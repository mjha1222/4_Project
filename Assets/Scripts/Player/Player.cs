using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int playerMainStage;
    public int playerSubStage;
    public int playerGold;
    public int playerAtt;
    public int playerCri;
    public float playerCriDamaged;
    public float playerGoldBonus;
    public float playerAutoAtt;

    //���׷��̵�
    public int levelCri;
    public int levelAuto;
    public int levelGold;

    //Equip 
    public WeaponData weaponData;
    public List<SaveWeaponData> saveWeaponData = new List<SaveWeaponData>();


    private int plTotalAtt;
    private int plTotalCri;
    private float plTotalCriDamaged;
    private float plTotalGoldBonus;

    public Player(int playermainstage, int playersubstage, int playergold, int playeratt, int playercri)
    {
        //��������
        playerMainStage = playermainstage;
        playerSubStage = playersubstage;

        //���� ���
        playerGold = playergold;

        //������� ����
        playerAtt = playeratt;
        playerCri = playercri;

        //���׷��̵� ����
        playerCriDamaged = 1.0f;
        playerGoldBonus = 1.0f;
        playerAutoAtt = 0f;
    }

    public void UpgradeCritDamage(float newMultiplier)
    {
        playerCriDamaged = Mathf.RoundToInt(newMultiplier * 100f);
    }

    public void UpgradeAutoAttack(int newAttack)
    {
        playerAtt = newAttack;
    }

    public void UpgradeGoldBonus(float newBonus)
    {
        playerGoldBonus = newBonus;
    }

    public void SpendGold(int amount)
    {
        playerGold = Mathf.Max(0, playerGold - amount);
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
    }

    public void SetUpgrade(int cri, int auto, int gold)
    {
        levelCri = cri;
        levelAuto = auto;
        levelGold = gold;

    }

    public void SetWeaponData(List<WeaponData> weapons)
    {
        if (weapons.Count == 0 ) return;

        foreach(WeaponData data in weapons)
        {
            SaveWeaponData saveData = new SaveWeaponData
            {
                weaponName = data.weaponName,
                baseDamage = data.baseDamage,
                baseCritChance = data.baseCritChance,
                baseGoldBonus = data.baseGoldBonus,
                damagePerUpgrade = data.damagePerUpgrade,
                critPerUpgrade = data.critPerUpgrade,
                goldPerUpgrade = data.goldPerUpgrade,
                buyPrice = data.buyPrice,
                upgradeCost = data.upgradeCost
            };
            saveWeaponData.Add(saveData);
        }
       
    }

    public enum SwordItem
    {
        ������,
        ����,
        ö��,
        Ȳ�ݰ�
    }

    public void LoadWeaponData(SwordItem item)
    {
        WeaponData weapon;

        switch (item)
        {
            case SwordItem.������:
                weapon = Resources.Load<WeaponData>("WeaponScriptableObject\\WSword");
                break;
            case SwordItem.����:
                weapon = Resources.Load<WeaponData>("WeaponScriptableObject\\SSword");
                break;
            case SwordItem.ö��:
                weapon = Resources.Load<WeaponData>("WeaponScriptableObject\\ISword");
                break;
            case SwordItem.Ȳ�ݰ�:
                weapon = Resources.Load<WeaponData>("WeaponScriptableObject\\GSword");
                break;
        }
        

    }
}

[System.Serializable]
public class SaveWeaponData
{
    public string weaponName;
    public int baseDamage;
    public float baseCritChance;
    public int baseGoldBonus;

    public int damagePerUpgrade;
    public float critPerUpgrade;
    public int goldPerUpgrade;

    public int buyPrice;
    public int upgradeCost;
}