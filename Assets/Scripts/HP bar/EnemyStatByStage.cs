using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

[System.Serializable]
public class EnemyStatByStage
{
    public int stage;  // �ش� �����Ͱ� ����� �������� ��ȣ
    public int health;  // �ش� �������������� ü�� ��
}

[CreateAssetMenu(menuName = "Enemies/Enemy Stats By Type")]
public class EnemyStatData : ScriptableObject
{
    public EnemyType enemyType;
    public EnemyStatByStage[] statsPerStage;

    public int GetHealthByStage(int stage)
    {
        foreach (var stat in statsPerStage)
        {
            if (stat.stage == stage)
                return stat.health;
        }

        Debug.LogWarning($"No health data for {enemyType} at stage {stage}");
        return 10; // �⺻��
    }
}

