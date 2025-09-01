using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    public GoldWallet wallet;
    public UpgradeDataTable dataTable;

    public int currentLevel = 1;

    public UpgradeLevelData Current => dataTable != null ? dataTable.GetLevelData(currentLevel) : null;

    public bool TryBuy()
    {
        if (dataTable == null) return false;
        var data = Current;
        if (data == null) return false;
        if (!wallet.TrySpend(data.cost)) return false;

        currentLevel++;
        return true;
    }
}
