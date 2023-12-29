using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCtrl : MonoBehaviour
{
    private Transform tr;
    private GameManager gameManager;
    public float rotSpeed = 100.0f;
    // Start is called before the first frame update

    private void Awake() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void Start()
    {
        tr = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if(!gameManager.gameOver)
        {
            //마우스 스크롤 휠을 이용하여 포신의 각도를 조절
            float n = tr.rotation.eulerAngles.x;
            float angle = -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * rotSpeed;
            if(angle > 0 && (n < 10 || n >= 336)){
                tr.Rotate(angle, 0, 0);
            }
            if(angle < 0 && (n > 336 || n <= 10)){
                tr.Rotate(angle, 0, 0);
            }

            if(n < 336 && n > 180) tr.transform.localEulerAngles = new Vector3((float)336, 0, 0);
            else if(n > 10 && n < 180) tr.transform.localEulerAngles = new Vector3((float)9.99, 0, 0);
        }
        
    }
}
