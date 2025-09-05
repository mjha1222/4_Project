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




    public void SpawnEnemy()  // ���� ���̸� ���� ��������
    {
        GameObject spawnedEnemy;

        if (DeathCount <= 10)
        {
            spawnedEnemy = Instantiate(CirclePrefab, spawnPoint.position, spawnPoint.rotation);
            enemyName = "���׶�� ����";
            DungeonName = "���׶�� ������ ��";
        }
        else if (DeathCount <= 20)
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

        // ��� ȹ�� 
        int goldReward = 0;

        if (DeathCount <= 10)
            goldReward = 10; // ���׶��
        else if (DeathCount <= 20)
            goldReward = 30; // �ﰢ��
        else
            goldReward = 50; // �簢��

        Gold += goldReward;
        GoldViewText();


        DeathCount++;
        UpdateEnemyKillText();
        Invoke(nameof(SpawnEnemy), 0.3f);  //���� ��ȯ�Ǵ� �ð�
     }

    public void UpdateEnemyKillText()
    {
        StageNum.text = $"{(DeathCount) %11} / 10 Kill"; //ų ī��Ʈ
    }
    public void GoldViewText()
    {
        goldText.text = $"<color=yellow>Gold</color> <align=left>{Gold}";
    }
}


