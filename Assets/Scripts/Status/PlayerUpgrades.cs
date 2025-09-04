using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public UpgradeDataTable table;

    public int critLevel = 0;
    public int autoLevel = 0;
    public int goldLevel = 0;

    const string K_CRIT = "UP_critlevel";
    const string K_AUTO = "UP_autolevel";
    const string K_GOLD = "UP_goldlevel";

    Player P => GameManager.Instance != null ? GameManager.Instance.player : null;

    void OnEnable()
    {
        LoadLevels();
        ApplyAllToPlayer();
    }

    void Start()
    {
        LoadLevels();
        ApplyAllToPlayer();
    }

    void OnValidate()
    {
        if (critLevel < 0) critLevel = 0;
        if (autoLevel < 0) autoLevel = 0;
        if (goldLevel < 0) goldLevel = 0;
        ClampToTableMax();
    }

    void ClampToTableMax()
    {
        if (table == null) return;
        int cMax = table.GetMaxLevel(UpgradeStatType.CritDamage);
        int aMax = table.GetMaxLevel(UpgradeStatType.AutoAttack);
        int gMax = table.GetMaxLevel(UpgradeStatType.GoldBonus);
        if (cMax >= 0 && critLevel > cMax) critLevel = cMax;
        if (aMax >= 0 && autoLevel > aMax) autoLevel = aMax;
        if (gMax >= 0 && goldLevel > gMax) goldLevel = gMax;
    }

    public void ApplyAllToPlayer()
    {
        if (P == null || table == null) return;
        ClampToTableMax();
        P.playerCriDamaged = table.GetValue(UpgradeStatType.CritDamage, critLevel);
        P.playerAutoAtt = table.GetValue(UpgradeStatType.AutoAttack, autoLevel);
        P.playerAtt = table.GetAttackBonusFromAutoLevel(autoLevel);
        P.playerGoldBonus = table.GetValue(UpgradeStatType.GoldBonus, goldLevel);

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

    public void SetLevel(UpgradeStatType stat, int level)
    {
        if (level < 0) level = 0;
        switch (stat)
        {
            case UpgradeStatType.CritDamage: critLevel = level; break;
            case UpgradeStatType.AutoAttack: autoLevel = level; break;
            case UpgradeStatType.GoldBonus: goldLevel = level; break;
        }
        ClampToTableMax();
        SaveLevels();
        ApplyAllToPlayer();
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
        ClampToTableMax();
        SaveLevels();
        ApplyAllToPlayer();
    }

    public bool CanLevelUp(UpgradeStatType stat)
    {
        if (table == null) return false;
        int cur = GetLevel(stat);
        int max = table.GetMaxLevel(stat);
        return max < 0 || cur < max;
    }

    public void ResetLevels()
    {
        critLevel = 0;
        autoLevel = 0;
        goldLevel = 0;
        SaveLevels();
        ApplyAllToPlayer();
    }

    public void SaveLevels()
    {
        PlayerPrefs.SetInt(K_CRIT, critLevel);
        PlayerPrefs.SetInt(K_AUTO, autoLevel);
        PlayerPrefs.SetInt(K_GOLD, goldLevel);
        PlayerPrefs.Save();
    }

    public void LoadLevels()
    {
        if (PlayerPrefs.HasKey(K_CRIT)) critLevel = PlayerPrefs.GetInt(K_CRIT, 0);
        if (PlayerPrefs.HasKey(K_AUTO)) autoLevel = PlayerPrefs.GetInt(K_AUTO, 0);
        if (PlayerPrefs.HasKey(K_GOLD)) goldLevel = PlayerPrefs.GetInt(K_GOLD, 0);
        ClampToTableMax();
    }

    public void ClearSavedLevels()
    {
        PlayerPrefs.DeleteKey(K_CRIT);
        PlayerPrefs.DeleteKey(K_AUTO);
        PlayerPrefs.DeleteKey(K_GOLD);
        PlayerPrefs.Save();
    }
}
