using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver{get; private set;}
    bool gameClear = false;
    public static float time=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        gameOver = false;
    }

    // Update is called once per frame
private void Update() {
        if(!gameOver && !gameClear) time+=Time.deltaTime;
    }

    public void die() //죽었을 때 엔딩씬 추가하기
    {
        gameOver = true;
        SceneManager.LoadScene("GameOver");
    }

    public void endGame() //클리어했을 때 엔딩씬 추가하기
    {
        gameClear = true;
        SceneManager.LoadScene("GameClear");
    }
}
