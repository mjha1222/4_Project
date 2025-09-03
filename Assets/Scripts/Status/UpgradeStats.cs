using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    public GoldWallet wallet;
    public UpgradeDataTable dataTable;
    public int currentLevel = 1;

    public UpgradeLevelData Current => dataTable != null ? dataTable.GetLevelData(currentLevel) : null;

    public bool TryBuy(Player player)
    {
        if (dataTable == null) return false;
        var cur = Current;
        if (cur == null) return false;
        if (!wallet.TrySpend(cur.cost)) return false;

        currentLevel++;

        var next = Current;
        if (player != null && next != null)
        {
            player.UpgradeCritDamage(next.critMultiplier);
            player.UpgradeAutoAttack(Mathf.RoundToInt((float)next.autoDps));
            player.UpgradeGoldBonus(next.goldMultiplier);
        }
        return true;
    }
}
