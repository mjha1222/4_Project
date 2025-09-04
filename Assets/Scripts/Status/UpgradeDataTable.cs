using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeDataTable", menuName = "Upgrades/Data Table")]
public class UpgradeDataTable : ScriptableObject
{
    public int baseCost = 5;
    public int costStep = 5;

    public float critBase = 0f;
    public float critPerLevel = 0.1f;

    public float autoBase = 0f;
    public float autoPerLevel = 0.1f;

    public int attackPerLevel = 2;

    public float goldBase = 0f;
    public float goldPerLevel = 0.5f;

    public float GetValue(UpgradeStatType stat, int level)
    {
        if (level < 0) level = 0;
        switch (stat)
        {
            case UpgradeStatType.CritDamage: return critBase + critPerLevel * level;
            case UpgradeStatType.AutoAttack: return autoBase + autoPerLevel * level;
            case UpgradeStatType.GoldBonus: return goldBase + goldPerLevel * level;
        }
        return 0f;
    }

    public int GetAttackBonusFromAutoLevel(int autoLevel)
    {
        if (autoLevel < 0) autoLevel = 0;
        return attackPerLevel * autoLevel;
    }

    public int GetMaxLevel(UpgradeStatType stat)
    {
        return -1;
    }

    public int GetCost(UpgradeStatType stat, int currentLevel)
    {
        if (currentLevel < 0) currentLevel = 0;
        return baseCost + costStep * currentLevel;
    }
}
