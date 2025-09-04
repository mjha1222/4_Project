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

    public int levelCri;
    public int levelAuto;
    public int levelGold;

    //Equip 
    //public Equip playerEquip;

    //Upgrade
    //public List<Upgrade> playerUpgrade;
    //자동공격, 치명타데미지, 골드획득량

    private int plTotalAtt;
    private int plTotalCri;
    private float plTotalCriDamaged;
    private float plTotalGoldBonus;

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

        //업그레이드 관련
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

    public void AddUpgrade(int cri, int auto, int gold)
    {
        levelCri = cri;
        levelAuto = auto;
        levelGold = gold;

    }

    //장비 장착 생성되고 생각
    /*
    public void PlayerEquipItem(Equip equipItem)
    {
        plTotalAtt = playerAtt;
        plTotalCri = playerCri;

        if (playerEquip != null)
            UnEquipItem();

        plTotalAtt += playerEquip.att;
        plTotalCri += playerEquip.cri;
        playerEquip = equipItem;
    }

    public void UnEquipItem()
    {
        plTotalAtt -= playerEquip.att;
        plTotalCri -= playerEquip.cri;
    }
    */

    //업그레이드 되고 생각
    /*
    public void PlayerUpgrade(List<Upgrade> upgrades)
    {
        playerCriDamaged += upgrades[0].criticalDamage;
        playerGoldBonus += upgrades[1].goldBonus;
        playerAutoAtt += upgrades[2].autoAttack;
    }
    */
}
