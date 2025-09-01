using UnityEngine;

public class Player
{
    public int playerMainStage { get; private set; }
    public int playerSubStage { get; private set; }
    public int playerGold { get; private set; }
    public int playerAtt { get; private set; }
    public int playerCri { get; private set; }
    public int playerCriDamaged { get; private set; }
    public float playerGoldBonus { get; private set; }

    // Equip

    // Upgrade
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

    public Player(int playermainstage, int playersubstage, int playergold, int playeratt, int playercri, int playercridamaged, float playergoldbouns)
    {
        playerMainStage = playermainstage;
        playerSubStage = playersubstage;
        playerGold = playergold;
        playerAtt = playeratt;
        playerCri = playercri;
        playerCriDamaged = playercridamaged;
        playerGoldBonus = playergoldbouns;
    }
}
