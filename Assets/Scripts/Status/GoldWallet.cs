using UnityEngine;
using TMPro;

public class GoldWallet : MonoBehaviour
{
    static GoldWallet _inst;
    public static GoldWallet Get() => _inst;

    public Player player;
    public TMP_Text goldText;
    public bool useThousandsSeparator = true;
    public int debugGold = 0;

    string goldHex = "#FFD900";
    string valueHex = "#FFFFFF";

    void Awake()
    {
        if (_inst != null && _inst != this) { Destroy(gameObject); return; }
        _inst = this;
    }

    void Start()
    {
        if (player != null) player.playerGold = debugGold;
        Broadcast();
    }

    void Update()
    {
        if (player != null && player.playerGold != debugGold)
        {
            player.playerGold = debugGold;
            Broadcast();
        }
    }

    void Broadcast()
    {
        if (!goldText) return;
        int g = GetGold();
        string s = useThousandsSeparator ? g.ToString("N0") : g.ToString();
        goldText.text = $"<color={goldHex}>Gold</color> <color={valueHex}>{s}</color>";
    }

    public int GetGold() => player != null ? player.playerGold : 0;

    public void SetGold(int amount)
    {
        if (player == null) return;
        player.playerGold = Mathf.Max(0, amount);
        debugGold = player.playerGold;
        Broadcast();
    }

    public void AddGold(int amount) => SetGold(GetGold() + amount);

    public bool TrySpend(int amount)
    {
        if (GetGold() < amount)
        {
            UIManager.instance.GoldWarringMessage();
            return false;
        }
        SetGold(GetGold() - amount);
        return true;
    }

    void OnValidate()
    {
        if (player != null)
        {
            player.playerGold = debugGold;
            Broadcast();
        }
    }
}
