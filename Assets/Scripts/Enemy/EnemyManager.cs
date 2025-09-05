using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Enemy nowEnemy;
    public Image HPbar;

    public GameObject SquarePrefab;
    public GameObject TrianglePrefab;
    public GameObject CirclePrefab;
    public Transform spawnPoint;
    public Text EnemyNameText;
    public Text DungeonNamText;
    string enemyDisplayName = "";
    string DungeonDisplayName = "";

    private int circleDeathCount = 0;
    private object StageNum;




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
        UpdateEnemyKillText();
    }

    

    public void SpawnEnemy()  // 적을 죽이면 다음 스테이지
    {
        GameObject spawnedEnemy;

        if (circleDeathCount <= 10)
        {
            spawnedEnemy = Instantiate(CirclePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyDisplayName = "동그라미 몬스터";
            DungeonDisplayName = "동그라미 몬스터의 숲";
        }
        else if (circleDeathCount <= 20)
        {
            spawnedEnemy = Instantiate(TrianglePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyDisplayName = "삼각형 몬스터";
            DungeonDisplayName = "삼각형 몬스터의 숲";
        }
        else
        {
            spawnedEnemy = Instantiate(SquarePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyDisplayName = "사각형 몬스터";
            DungeonDisplayName = "사각형 몬스터의 숲";
        }

        nowEnemy = spawnedEnemy.GetComponent<Enemy>();

        EnemyNameText.text = $"<color=wheit>{enemyDisplayName}</color>";   //몬스터 이름
        DungeonNamText.text = $"<color=wheit>{DungeonDisplayName}</color>";  // 던전 이름

    }

     public void OnEnemyDead()
     {
        circleDeathCount++;
        UpdateEnemyKillText();
        Invoke(nameof(SpawnEnemy), 0.5f);  //적이 소환되는 시간
     }

    public void UpdateEnemyKillText()
    {
      // StageNum.text = $"Enemy Kill{circleDeathCount}";
    }
}

