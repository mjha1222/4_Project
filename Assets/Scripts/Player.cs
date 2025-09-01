
public class Player 
{
    public int playerStageNum { get; private set; }
    public int playerGold { get; private set; }
    public int playerAtt { get; private set; }
    public int playerCri {  get; private set; }
    public int playerCriDamaged { get; private set; }
    public int playerGoldBonus { get; private set; }

    //Equip 

    //Upgrade

    public Player(int playerstageNum, int playergold, int playeratt, int playercri, int playercridamaged, int playergoldbouns)
    {
        playerStageNum = playerstageNum;
        playerGold = playergold;
        playerAtt = playeratt;
        playerCri = playercri;
        playerCriDamaged = playercridamaged;
        playerGoldBonus = playergoldbouns;  
    }


}
