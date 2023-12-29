using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider slider;
    public int health, maxHealth;
    public bool dead {get; protected set;}
    Enemy enemy;
    // Start is called before the first frame update
    GameManager gameManager;
    void Start()
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        health = maxHealth;
        dead = false;
        enemy = GetComponent<Enemy>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    public void healthUpdate(int value)
    {
        if((health + value) > maxHealth) health = maxHealth;
        else health += value;
        slider.value = health;
        if(health <= 0) 
        {
            dead = true;
            if(enemy != null)  //enemy면 Die함수 실행
            {
                enemy.Die();
            }
            else gameManager.die();
        }
    }

    public void upgradeHealth()
    {
        maxHealth += 20;
        slider.maxValue = maxHealth;
        health = maxHealth;
        slider.value = health;
    }
}
