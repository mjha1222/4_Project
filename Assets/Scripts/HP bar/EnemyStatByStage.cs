using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

[System.Serializable]
public class EnemyStatByStage
{
    public int stage;  // 해당 데이터가 적용될 스테이지 번호
    public int health;  // 해당 스테이지에서의 체력 값
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
        return 10; // 기본값
    }
}

