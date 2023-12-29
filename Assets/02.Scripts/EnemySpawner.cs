using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // 적 AI를 소환할 위치들
    private List<GameObject> enemies = new List<GameObject>(); // 생성된 적들을 담는 리스트
    public GameObject[] enemyPrefebs;
    public GameObject uiManager;
    GameManager gameManager;
    int deadEnemys=0;
    private int wave=0; // 현재 웨이브
    bool isWaveEnd = false;

    // Start is called before the first frame update
    private void Awake() 
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SpawnWave();
    }

    public void SpawnWave() {
        deadEnemys = 0;
        isWaveEnd=false;
        wave++;
        uiManager.GetComponent<UIManager>().WaveNotify(wave);
        for(int i=0; i<wave; i++)
        {
            CreateEnemy();
        }
        uiManager.GetComponent<UIManager>().updateEnemyWaveText(wave-deadEnemys, wave);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isWaveEnd && deadEnemys == wave) isWaveEnd = true;
        else if(isWaveEnd && deadEnemys == wave && wave != 10) SpawnWave();
        if(isWaveEnd && wave == 10) gameManager.endGame();
    }

    private void CreateEnemy() 
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; //생성할 위치 랜덤 설정
        GameObject enemy = Instantiate(enemyPrefebs[Random.Range(0, 2)], spawnPoint.position, spawnPoint.rotation);
        enemies.Add(enemy); //생성된 적을 리스트에 추가
    }

    public void deadEnemy()
    {
        deadEnemys += 1;
        uiManager.GetComponent<UIManager>().updateEnemyWaveText(wave-deadEnemys, wave);
    }
}
