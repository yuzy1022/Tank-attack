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
        heatShell = (GameObject)Resources.Load("Cannon"); //��ź ������ ���ҽ��������� �ҷ�����
        heShell = (GameObject)Resources.Load("HE Cannon"); //��ź ������ ���ҽ��������� �ҷ�����
        fireSfx = Resources.Load<AudioClip>("CannonFire"); // ���� ���� ���ҽ� �������� �ҷ�����
        sfx = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        useShell = heat;
        uiManager = canvasOb.GetComponent<UIManager>();
        heShellRemain = 5;
    }
    
    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0) && ready && !gameManager.gameOver) //���콺 ���� ��ư�� ������ ��ź �߻�
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
            Instantiate(heatShell, firePos.position, firePos.rotation); //��ź ����
            sfx.PlayOneShot(fireSfx, 1.0f); //�߻� ���� ����
        }
        else
        {
            if(heShellRemain > 0)
            {
                heShellRemain -= 1;
                Instantiate(heShell, firePos.position, firePos.rotation);
                sfx.PlayOneShot(fireSfx, 1.0f); //�߻� ���� ����
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
