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
    string enemyName = "";
    string DungeonName = "";

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

    

    public void SpawnEnemy()  // ���� ���̸� ���� ��������
    {
        GameObject spawnedEnemy;

        if (circleDeathCount <= 10)
        {
            spawnedEnemy = Instantiate(CirclePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyName = "���׶�� ����";
            DungeonName = "���׶�� ������ ��";
        }
        else if (circleDeathCount <= 20)
        {
            spawnedEnemy = Instantiate(TrianglePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyName = "�ﰢ�� ����";
            DungeonName = "�ﰢ�� ������ ��";
        }
        else
        {
            spawnedEnemy = Instantiate(SquarePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyName = "�簢�� ����";
            DungeonName = "�簢�� ������ ��";
        }
        

        nowEnemy = spawnedEnemy.GetComponent<Enemy>();

        EnemyNameText.text = $"<color=wheit>{enemyName}</color>";   //���� �̸�
        DungeonNamText.text = $"<color=wheit>{DungeonName}</color>";  // ���� �̸�

    }

     public void OnEnemyDead()
     {
        circleDeathCount++;
        UpdateEnemyKillText();
        Invoke(nameof(SpawnEnemy), 0.5f);  //���� ��ȯ�Ǵ� �ð�
     }

    public void UpdateEnemyKillText()
    {
      // StageNum.text = $"Enemy Kill{circleDeathCount}";
    }
}

