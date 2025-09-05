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

    public GameObject squarePrefab;
    public GameObject TrianglePrefab;
    public GameObject enemyPrefab;
    public Transform spawnPoint;


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
        if(true)
        {

        }
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        nowEnemy=spawnedEnemy.GetComponent<Enemy>();

    }

     public void OnEnemyDead()
     {
        Invoke(nameof(SpawnEnemy), 0.5f);
     }
}
