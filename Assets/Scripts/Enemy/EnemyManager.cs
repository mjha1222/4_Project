using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Enemy nowEnemy;
    public Image HPbar;


    private void Awake()
    {
        Instance = this;
    }
    public void RefreshHPbar()
    {
        HPbar.fillAmount = nowEnemy.nowHP / (float)nowEnemy.Data.HP; 
    }

    public void Start()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        // GameObject spawnedEnemy = Instantiate(
    }

     public void OnEnemyDead()
     {
       
     }
}
