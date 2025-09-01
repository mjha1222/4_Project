using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    public Gold wallet;
    public UpgradeDataTable dataTable;
    public int currentLevel = 1;

    public UpgradeLevelData Current => dataTable != null ? dataTable.GetLevelData(currentLevel) : null;

    public bool TryBuy(Player player)
    {
        if (dataTable == null) return false;
        var data = Current;
        if (data == null) return false;
        if (!wallet.TrySpend(data.cost)) return false;

        currentLevel++;

        if (player != null)
        {
            player.UpgradeCritDamage(data.critMultiplier);
            player.UpgradeAutoAttack(Mathf.RoundToInt((float)data.autoDps));
            player.UpgradeGoldBonus(data.goldMultiplier);
        }

        return true;
    }
}
