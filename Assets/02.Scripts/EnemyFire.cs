using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject cannon, turret, target;
    public Transform firePos;
    private AudioClip fireSfx = null;
    private AudioSource sfx = null;
    bool ready = true;
    public float fireRate;
    float lastFireTime=0;
    // Start is called before the first frame update
    private void Awake()
    {
        fireSfx = Resources.Load<AudioClip>("CannonFire"); // 사운드 파일 리소스 폴더에서 불러오기
        sfx = GetComponent<AudioSource>();
        target = GameObject.Find("Tank");
    }

    private void Update() {
        Vector3 targetPosition = new Vector3 (target.transform.position.x, turret.transform.position.y, target.transform.position.z);
        turret.transform.LookAt (targetPosition);
        if(!ready && lastFireTime > 0)
        {
            lastFireTime -= Time.deltaTime;
        }
        else if(!ready && lastFireTime <= 0)
        {
            ready = true;
        }
    }
    public void Fire()
    {
        if(ready)
        {
            ready = false;
            lastFireTime = fireRate;
            sfx.PlayOneShot(fireSfx, 1.0f); //발사 사운드 실행
            Instantiate(cannon, firePos.position, firePos.rotation); //포탄 생성
        }
    }

    public bool isTargetStraight(int fireDistance, LayerMask targetLayer)
    {
        RaycastHit hit;
        if(Physics.SphereCast(firePos.position, (float)10, firePos.forward, out hit, fireDistance, targetLayer))
        {
            if(hit.collider.tag == "Player")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        else return false;
    }
}
