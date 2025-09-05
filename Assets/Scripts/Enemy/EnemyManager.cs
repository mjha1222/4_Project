using TMPro;
using UnityEngine;
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
    public Text StageNum;
    string enemyName = "";
    string DungeonName = "";
    public int Gold = 0;
    public TextMeshProUGUI goldText;

    private int DeathCount = 0;
    



    private void Awake()
    {
        Instance = this;
    }
    public void RefreshHPbar()
    {
        HPbar.fillAmount = nowEnemy.nowHP / (float)nowEnemy.Data.HP; 
    }

    public void Init()
    {
        SpawnEnemy();
        UpdateEnemyKillText();
        GoldViewText();

    }




    public void SpawnEnemy()  // 적을 죽이면 다음 스테이지
    {
        GameObject spawnedEnemy;

        if (DeathCount <= 10)
        {
            spawnedEnemy = Instantiate(CirclePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyName = "동그라미 몬스터";
            DungeonName = "동그라미 몬스터의 숲";
        }
        else if (DeathCount <= 20)
        {
            spawnedEnemy = Instantiate(TrianglePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyName = "삼각형 몬스터";
            DungeonName = "삼각형 몬스터의 숲";
        }
        else
        {
            spawnedEnemy = Instantiate(SquarePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyName = "사각형 몬스터";
            DungeonName = "사각형 몬스터의 숲";
        }


        nowEnemy = spawnedEnemy.GetComponent<Enemy>();

        EnemyNameText.text = $"<color=wheit>{enemyName}</color>";   //몬스터 이름
        DungeonNamText.text = $"<color=wheit>{DungeonName}</color>";  // 던전 이름

    }

     public void OnEnemyDead()
     {

        // 골드 획득 
        int goldReward = 0;

        if (DeathCount <= 10)
            goldReward = 10; // 동그라미
        else if (DeathCount <= 20)
            goldReward = 30; // 삼각형
        else
            goldReward = 50; // 사각형

        Gold += goldReward;
        GoldViewText();


        DeathCount++;
        UpdateEnemyKillText();
        Invoke(nameof(SpawnEnemy), 0.3f);  //적이 소환되는 시간
     }

    public void UpdateEnemyKillText()
    {
        StageNum.text = $"{(DeathCount) %11} / 10 Kill"; //킬 카운트
    }
    public void GoldViewText()
    {
        goldText.text = $"<color=yellow>Gold</color> <align=left>{Gold}";
    }
}


