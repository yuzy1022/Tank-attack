using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float moveSpeed = 20.0f; //이동 속도
    public float rotSpeed = 50.0f; // 회전 속도
    // 참조할 컴포넌트 변수들
    private Rigidbody rbody; 
    private Transform tr;
    //키보드 입력 값 변수
    private float h, v;
    private GameManager gameManager;

    private void Awake() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        //탱크의 리지드바디 무게중심을 낮게 설정
        rbody.centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);   
    }
    // Update is called once per frame
    void Update()
    {
        if(!gameManager.gameOver)
        {
            h = Input.GetAxis("Horizontal"); //좌우 방향키 (회전)
            v = Input.GetAxis("Vertical"); // 위아래 방향키(이동)

            //회전과 이동처리
            tr.Rotate(Vector3.up * rotSpeed * h * Time.deltaTime);
            tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        }
    }
}
