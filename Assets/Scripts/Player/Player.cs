
[System.Serializable]
public class Player 
{
    public int playerMainStage;
    public int playerSubStage;
    public int playerGold;
    public int playerAtt;
    public int playerCri;
    public int playerCriDamaged;
    public float playerGoldBonus;

    //Equip 

    //Upgrade

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
