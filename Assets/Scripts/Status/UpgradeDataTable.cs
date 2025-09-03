using UnityEngine;

public enum UpgradeKind { Crit, Auto, Gold }

[CreateAssetMenu(fileName = "UpgradeDataTable", menuName = "Game/Upgrade Data Table")]
public class UpgradeDataTable : ScriptableObject
{
    public UpgradeKind kind = UpgradeKind.Crit;

    public int startCost = 10;
    public int costStep = 5;

    public float critStartPercent = 0f;
    public float critStepPercent = 1f;

    public float autoBaseInterval = 10f;
    public float autoIntervalStep = 0.1f;

    public float goldStartPercent = 0f;
    public float goldStepPercent = 0.5f;

    public UpgradeLevelData[] levels;

    public UpgradeLevelData GetLevelData(int level)
    {
        if (levels == null || levels.Length == 0) return null;

        int index = Mathf.Clamp(level - 1, 0, levels.Length - 1);
        var d = levels[index];

        d.cost = startCost + (level - 1) * costStep;

        switch (kind)
        {
            case UpgradeKind.Crit:
                {
                    float pct = critStartPercent + (level - 1) * critStepPercent;
                    d.critMultiplier = 1f + pct / 100f;
                }
                break;

            case UpgradeKind.Auto:
                {
                    float interval = Mathf.Max(0.05f, autoBaseInterval - (level - 1) * autoIntervalStep);
                    d.autoDps = 1.0 / interval;
                }
                break;

            case UpgradeKind.Gold:
                {
                    float pct = goldStartPercent + (level - 1) * goldStepPercent;
                    d.goldMultiplier = 1f + pct / 100f;
                }
                break;
        }
        return d;
    }
}
