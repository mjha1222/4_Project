
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

    //Equip 
    //public Equip playerEquip;

    //Upgrade
    //public List<Upgrade> playerUpgrade;
    //�ڵ�����, ġ��Ÿ������, ���ȹ�淮

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

    //��� ���� �����ǰ� ����
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

    //���׷��̵� �ǰ� ����
    /*
    public void PlayerUpgrade(List<Upgrade> upgrades)
    {
        playerCriDamaged += upgrades[0].criticalDamage;
        playerGoldBonus += upgrades[1].goldBonus;
        playerAutoAtt += upgrades[2].autoAttack;
    }
    */
}
