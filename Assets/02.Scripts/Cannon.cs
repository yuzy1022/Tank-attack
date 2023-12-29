using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float speed;
    public GameObject expEffect; //폭발 효과 프리팹 연결 변수
    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;
    int damage = 10;
    float exRange = 5;
    bool boom = false;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        //포탄이 앞쪽 방향으로 6000의 힘만큼 날라감
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        //3초가 지난 후 자동으로 폭발하는 코루틴 실행 (포탄이 지면에 맞든 안맞든)
        Invoke("ExplosionCannon", 3);
    }
    private void OnTriggerEnter(Collider other)
    {
        //지면 또는 적 탱크에 충돌한 경우 즉시 폭발하도록 코루틴 실행
        if(other.tag == "Player" || other.tag == "Enemy" || other.tag == "Map") ExplosionCannon(); 
    }
    void ExplosionCannon()
    {
        if(!boom)
        {
            boom = true;
        _collider.enabled = false; //콜라이더 비활성화
        _rigidbody.isKinematic = true; //물리엔진의 영향을 받지 않음
        Collider[] hitCollidersCircle = Physics.OverlapSphere(transform.position, exRange);  //폭발범위
        Health [] hitTarget = new Health[hitCollidersCircle.Length];
        for(int i = 0; i < hitCollidersCircle.Length; i++){  //circle안에 오브젝트가 있다면 데미지 적용
                    Health target = hitCollidersCircle[i].GetComponent<Health>();
                    //상대방의 updateHealth 함수를 실행시켜 상대방에 대미지 주기 
                    if(target != null)
                    {
                        bool attacked = false;
                        for(int u=0; u < hitTarget.Length; u++)
                        {
                            if(hitTarget[u] == target) attacked = true;
                        }
                        if(!attacked) 
                        {
                            target.healthUpdate(-damage);
                            hitTarget[i] = target;
                        }
                    }
                    
                } 
        //폭발 효과 오브젝트 생성
        GameObject obj = (GameObject)Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(obj, 1.0f); //폭발효과 재생을 1초간 기다린 뒤 파괴
        Destroy(this.gameObject, 1.0f); // Trail Renderer가 소멸될때까지 대기 후 파괴
        }
    }
}
