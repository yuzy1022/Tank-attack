using System.Collections;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드를 가져오기
using System;

// 적 AI를 구현한다
public class Enemy : MonoBehaviour {
    public LayerMask whatIsTarget; // 추적 대상 레이어

    private Health targetEntity; // 추적할 대상
    private NavMeshAgent pathFinder; // 경로계산 AI 에이전트

    private AudioSource enemyAudioPlayer; // 오디오 소스 컴포넌트
    private Renderer enemyRenderer; // 렌더러 컴포넌트
    public Health health;
    public EnemyFire enemyFire;
    public int speed;
    public int targetRange;
    public int fireRange;
    int minRange = 10;
    bool targetInRange = false;
    private EnemySpawner enemySpawner;
    public GameObject destroyEffect;
    public AudioClip destroyEffectSound = null;
    private AudioSource audioSource = null;
    

    // 추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (targetEntity != null && !targetEntity.dead)  //프로퍼티에 조건추가
            {
                return true;
            }

            // 그렇지 않다면 false
            return false;
        }
    }

    private void Awake() {
        // 초기화
        pathFinder = GetComponent<NavMeshAgent>();
        pathFinder.speed = speed;
        enemyAudioPlayer = GetComponent<AudioSource>();
        enemyRenderer = GetComponentInChildren<Renderer>();
        health = GetComponent<Health>();
        enemyFire = GetComponent<EnemyFire>();
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        // 게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(UpdatePath());
    }

    private void Update() {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    // 주기적으로 추적할 대상의 위치를 찾아 경로를 갱신
    private IEnumerator UpdatePath() {
        
        while (!health.dead)// 좀비가 살아있는 동안 무한 루프
        {
            if(hasTarget){//추적대상 존재 : 경로를 갱신하고 AI이동을 계속 진행
                float distance = Vector3.Distance(gameObject.transform.position, targetEntity.transform.position);
                //Collider[] colliders = Physics.OverlapSphere(transform.position, fireRange, whatIsTarget);
                //if(colliders.Length > 0 && enemyFire.isTargetStraight(fireRange, whatIsTarget))
                if(distance > fireRange)  //공격범위 밖이면 이동, 공격x
                {
                    pathFinder.enabled = true;
                    pathFinder.isStopped = false;
                    pathFinder.SetDestination(targetEntity.transform.position);
                }
                else if(distance <= minRange)  //공격범위o, 최소범위o 이면
                {
                    pathFinder.enabled = false; //정지
                    if(enemyFire.isTargetStraight(fireRange, whatIsTarget)) enemyFire.Fire();  //직선이면 공격
                }
                else //공격범위o, 최소범위x 이면
                {  
                    if(enemyFire.isTargetStraight(fireRange, whatIsTarget))  //직선이면
                    {
                        enemyFire.Fire(); //공격
                        if(distance <= targetRange) pathFinder.enabled = false; //추적범위o 이면 정지
                        else //추적범위x면 이동
                        {
                            pathFinder.enabled = true;
                            pathFinder.isStopped = false;
                            pathFinder.SetDestination(targetEntity.transform.position);
                        }
                    }
                    else //직선이 아니면 이동
                    {
                        pathFinder.enabled = true;
                        pathFinder.isStopped = false;
                        pathFinder.SetDestination(targetEntity.transform.position);
                    } 
                }
            }

            else {//추적 대상 없음 : AI이동 중지
                if(pathFinder.enabled) pathFinder.isStopped = true;
                //좀비의 포지션을 기준으로 20유닛의 반지름을 가진 가상의 구를 그렸을때 구와 겹치는 모든 콜라이더를 가져옴. 단, WhatIsTarget 레이어를 가진 콜라이더만 가져오도록 필터링
                Collider[] colliders = Physics.OverlapSphere(transform.position, 400f, whatIsTarget); 
                //살아있는 LivingEntity찾기
                for (int i = 0; i<colliders.Length; i++)
                {
                    Health livingEntity = colliders[i].GetComponent<Health>();

                    if(livingEntity != null && !livingEntity.dead){ //LivingEntity 컴포넌트가 존재하며, 해당 LivingEntity가 살아있다면
                        targetEntity = livingEntity; //추적대상을 해당 LivingEntity로 변경
                        float distance = Vector3.Distance(gameObject.transform.position, colliders[i].transform.position);
                        if(distance <= targetRange) targetInRange = true;
                        break;
                    }
                }
                
            }
            // 0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);
        }
    }

    // 사망 처리
    public void Die() {
        enemySpawner.deadEnemy();
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        Collider[] enemyColliders = GetComponents<Collider>();  //다른 AI를 방해하지 않도록 자신의 모든 콜라이더를 비활성화
        for(int i = 0; i<enemyColliders.Length; i++){
            enemyColliders[i].enabled = false;
        }
        pathFinder.enabled = true;
        pathFinder.isStopped = true;  //Ai추적을 중지하고 내비메쉬 컴포넌트 비활성화
        pathFinder.enabled = false;
        GameObject obj = (GameObject)Instantiate(destroyEffect, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(destroyEffectSound, 2f);
        Destroy(obj, 2f);
        Destroy(gameObject);
    }

    /*private void OnTriggerStay(Collider other) {
        // 트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행   
        if(!health.dead && Time.time >= lastAttackTime + timeBetAttack){
            Health attackTarget = other.GetComponent<Health>(); //상대방의 LivingEntity타입 가져오기 시도
            
            if(attackTarget != null && attackTarget == targetEntity){ //상대방의 LivingEntity가 자신의 추적 대상이라면 공격 실행
                lastAttackTime = Time.time; //최근 공격시간 갱신
                //Vector3 hitPoint = other.ClosestPoint(transform.position); //상대방의 피격 위치와 피격 방향을 근삿값으로 계산
                //Vector3 hitNormal = transform.position - other.transform.position;
                //attackTarget.OnDamage(damage, hitPoint, hitNormal); //공격실행
                enemyFire.Fire();
            }
        }
    }*/

    
}