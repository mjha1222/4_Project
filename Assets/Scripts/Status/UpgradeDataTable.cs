using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeDataTable", menuName = "Game/Upgrade Data Table")]
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

    public int GetCost(int currentLevel) => Mathf.Max(0, baseCost + currentLevel * costStep);

    public float GetValue(UpgradeStatType stat, int level)
    {
        switch (stat)
        {
            case UpgradeStatType.CritDamage: return critBase + critPerLevel * level;
            case UpgradeStatType.AutoAttack: return autoBase + autoPerLevel * level;
            case UpgradeStatType.GoldBonus: return goldBase + goldPerLevel * level;
        }
        return 0f;
    }

    public int GetAttackBonusFromAutoLevel(int level) => attackPerLevel * level;
}
