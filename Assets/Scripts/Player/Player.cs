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

    //업그레이드
    public int levelCri;
    public int levelAuto;
    public int levelGold;

    //Equip 

    public List<SaveWeaponData> saveWeaponData = new List<SaveWeaponData>();


    public int plTotalAtt { get; private set; }
    public float plTotalCri { get; private set; }
    public float plTotalCriDamaged { get; private set; }
    public float plTotalGoldBonus { get; private set; }

    public Player(int playermainstage, int playersubstage, int playergold, int playeratt, int playercri)
    {
        //스테이지
        playerMainStage = playermainstage;
        playerSubStage = playersubstage;

        //현재 골드
        playerGold = playergold;

        //장비장착 관련
        playerAtt = playeratt;
        playerCri = playercri;
    }

    public void FinalStatusSet()
    {
        plTotalAtt = playerAtt + WeaponManager.Instance.GetAttackPower();
        plTotalCri = playerCri + WeaponManager.Instance.GetCritRate();
        plTotalCriDamaged = 100 + playerCriDamaged;
        plTotalGoldBonus = 100 + playerGoldBonus;

        Debug.Log("데미지 : " + plTotalAtt);
        Debug.Log("크리율 : " + plTotalCri);
        Debug.Log("크리티컬 : " + plTotalCriDamaged);
        Debug.Log("골드보너스 : " + plTotalGoldBonus);
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

    public void GetWeaponData(List<WeaponSlot> weapons)
    {
        if (weapons.Count == 0) return;

        saveWeaponData = new List<SaveWeaponData>();

        foreach (WeaponSlot data in weapons)
        {
            WeaponData weapondata = data.weaponData;

            SaveWeaponData saveData = new SaveWeaponData
            {
                weaponName = weapondata.weaponName,
                weaponLevel = data.level
            };
            saveWeaponData.Add(saveData);
        }
    }

    public void SetWeaponData(List<WeaponSlot> weapons)
    {
        if (saveWeaponData.Count == 0) return;

        string weaponName = WeaponManager.Instance.currentWeapon.name;

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].weaponData.weaponName = saveWeaponData[i].weaponName;
            weapons[i].level = saveWeaponData[i].weaponLevel;

            if (weaponName == weapons[i].weaponData.name)
            {
                WeaponManager.Instance.level = saveWeaponData[i].weaponLevel;
                Debug.Log(saveWeaponData[i].weaponLevel);
            }
            Debug.Log($"이름 : {weapons[i].weaponData.name}, 레벨 : {saveWeaponData[i].weaponLevel}");
        }

    }

}
  

[System.Serializable]
public class SaveWeaponData
{
    public string weaponName;
    public int weaponLevel;
}