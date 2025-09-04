using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public Player player;
    public UpgradeDataTable table;

    public int critLevel = 0;
    public int autoLevel = 0;
    public int goldLevel = 0;

    public void ApplyAllToPlayer()
    {
        if (player == null || table == null) return;
        player.playerCriDamaged = table.GetValue(UpgradeStatType.CritDamage, critLevel);
        player.playerAutoAtt = table.GetValue(UpgradeStatType.AutoAttack, autoLevel);
        player.playerAtt = table.GetAttackBonusFromAutoLevel(autoLevel);
        player.playerGoldBonus = table.GetValue(UpgradeStatType.GoldBonus, goldLevel);
    }

    public int GetLevel(UpgradeStatType stat)
    {
        switch (stat)
        {
            case UpgradeStatType.CritDamage: return critLevel;
            case UpgradeStatType.AutoAttack: return autoLevel;
            case UpgradeStatType.GoldBonus: return goldLevel;
        }
        return 0;
    }

    public void AddLevel(UpgradeStatType stat, int delta = 1)
    {
        switch (stat)
        {
            case UpgradeStatType.CritDamage: critLevel += delta; break;
            case UpgradeStatType.AutoAttack: autoLevel += delta; break;
            case UpgradeStatType.GoldBonus: goldLevel += delta; break;
        }
        if (critLevel < 0) critLevel = 0;
        if (autoLevel < 0) autoLevel = 0;
        if (goldLevel < 0) goldLevel = 0;
        ApplyAllToPlayer();
    }
}
