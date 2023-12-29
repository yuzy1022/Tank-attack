using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    public GameObject heatShell, heShell, canvasOb;
    public Transform firePos;
    private AudioClip fireSfx = null;
    public AudioClip shellEmptySound;
    private AudioSource sfx = null;
    private GameManager gameManager;
    bool ready = true;
    float fireRate = 2, lastFireTime;
    public int heShellRemain {get; private set;}
    public string useShell{get; private set;}
    string heat = "heatShell", he="heShell"; 
    UIManager uiManager;
    private void Awake()
    {
        heatShell = (GameObject)Resources.Load("Cannon"); //포탄 프리팹 리소스폴더에서 불러오기
        heShell = (GameObject)Resources.Load("HE Cannon"); //포탄 프리팹 리소스폴더에서 불러오기
        fireSfx = Resources.Load<AudioClip>("CannonFire"); // 사운드 파일 리소스 폴더에서 불러오기
        sfx = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        useShell = heat;
        uiManager = canvasOb.GetComponent<UIManager>();
        heShellRemain = 5;
    }
    
    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0) && ready && !gameManager.gameOver) //마우스 왼쪽 버튼을 누르면 포탄 발사
        {
            Fire();
            ready = false;
            lastFireTime = fireRate;
        }
        else if(Input.GetButtonDown(heat) && !gameManager.gameOver)
        {
            useShell = heat;
            uiManager.changeShell(heat);
            
        }
        else if(Input.GetButtonDown(he) && !gameManager.gameOver)
        {
            useShell = he;
            uiManager.changeShell(he);
        }

        if(!ready && lastFireTime > 0)
        {
            lastFireTime -= Time.deltaTime;
        }
        else if(!ready && lastFireTime <= 0)
        {
            ready = true;
        }

    }
    void Fire()
    {
        if(useShell == heat)
        {
            Instantiate(heatShell, firePos.position, firePos.rotation); //포탄 생성
            sfx.PlayOneShot(fireSfx, 1.0f); //발사 사운드 실행
        }
        else
        {
            if(heShellRemain > 0)
            {
                heShellRemain -= 1;
                Instantiate(heShell, firePos.position, firePos.rotation);
                sfx.PlayOneShot(fireSfx, 1.0f); //발사 사운드 실행
                uiManager.updateRemainShellText();
            }
            else
            {
                sfx.PlayOneShot(shellEmptySound);
            }
            
        }
    }

    public void getShell()
    {
        heShellRemain += 1;
        if(useShell == he)
        {
            uiManager.updateRemainShellText();
        }
    }
}
