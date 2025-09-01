using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeDataTable", menuName = "Game/Upgrade Data Table")]
public class UpgradeDataTable : ScriptableObject
{
    public UpgradeLevelData[] levels;

    public UpgradeLevelData GetLevelData(int level)
    {
        if (levels == null || levels.Length == 0) return null;
        int index = Mathf.Clamp(level - 1, 0, levels.Length - 1);
        return levels[index];
    }
}
