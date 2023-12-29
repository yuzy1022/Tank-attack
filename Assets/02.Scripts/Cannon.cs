using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float speed;
    public GameObject expEffect; //���� ȿ�� ������ ���� ����
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
        //��ź�� ���� �������� 6000�� ����ŭ ����
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        //3�ʰ� ���� �� �ڵ����� �����ϴ� �ڷ�ƾ ���� (��ź�� ���鿡 �µ� �ȸµ�)
        Invoke("ExplosionCannon", 3);
    }
    private void OnTriggerEnter(Collider other)
    {
        //���� �Ǵ� �� ��ũ�� �浹�� ��� ��� �����ϵ��� �ڷ�ƾ ����
        if(other.tag == "Player" || other.tag == "Enemy" || other.tag == "Map") ExplosionCannon(); 
    }
    void ExplosionCannon()
    {
        if(!boom)
        {
            boom = true;
        _collider.enabled = false; //�ݶ��̴� ��Ȱ��ȭ
        _rigidbody.isKinematic = true; //���������� ������ ���� ����
        Collider[] hitCollidersCircle = Physics.OverlapSphere(transform.position, exRange);  //���߹���
        Health [] hitTarget = new Health[hitCollidersCircle.Length];
        for(int i = 0; i < hitCollidersCircle.Length; i++){  //circle�ȿ� ������Ʈ�� �ִٸ� ������ ����
                    Health target = hitCollidersCircle[i].GetComponent<Health>();
                    //������ updateHealth �Լ��� ������� ���濡 ����� �ֱ� 
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
        //���� ȿ�� ������Ʈ ����
        GameObject obj = (GameObject)Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(obj, 1.0f); //����ȿ�� ����� 1�ʰ� ��ٸ� �� �ı�
        Destroy(this.gameObject, 1.0f); // Trail Renderer�� �Ҹ�ɶ����� ��� �� �ı�
        }
    }
}
