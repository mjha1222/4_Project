using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeBtnUI : MonoBehaviour
{
    public UpgradeStats stats;
    public GoldWallet wallet;
    public Player player;

    public Button buyButton;
    public Text titleText;
    public Text levelText;
    public Text descText;
    public Text costText;

    public TMP_Text titleTMP;
    public TMP_Text levelTMP;
    public TMP_Text descTMP;
    public TMP_Text costTMPTxt;

    public Color affordableColor = Color.black;
    public Color unaffordableColor = new Color(0.9f, 0.2f, 0.2f);

    void OnEnable()
    {
        if (buyButton) buyButton.onClick.AddListener(OnClickBuy);
        if (wallet) wallet.onGoldChanged += OnGoldChanged;
        RefreshAll();
    }
    void OnDisable()
    {
        if (buyButton) buyButton.onClick.RemoveListener(OnClickBuy);
        if (wallet) wallet.onGoldChanged -= OnGoldChanged;
    }

    void Update() => RefreshInteractableOnly();

    void OnClickBuy()
    {
        if (stats != null && stats.TryBuy(player)) RefreshAll();
    }
    void OnGoldChanged(double _) => RefreshAll();

    void RefreshAll()
    {
        if (stats == null || stats.dataTable == null) { RefreshInteractableOnly(); return; }
        var data = stats.Current;
        if (data == null)
        {
            SetText(descText, descTMP, "");
            SetText(costText, costTMPTxt, "—");
            if (buyButton) buyButton.interactable = false;
            return;
        }

        SetText(titleText, titleTMP, BuildTitleString());
        SetText(descText, descTMP, BuildDescString(data));
        SetText(costText, costTMPTxt, $"{data.cost:0}");
        RefreshInteractableOnly();
    }

    string BuildTitleString()
    {
        string current = titleText ? titleText.text : (titleTMP ? titleTMP.text : "업그레이드 0");
        int i = current.Length - 1; while (i >= 0 && char.IsDigit(current[i])) i--;
        string prefix = current.Substring(0, i + 1).TrimEnd();
        if (string.IsNullOrEmpty(prefix)) prefix = "업그레이드";
        return $"{prefix} {stats.currentLevel}";
    }

    string BuildDescString(UpgradeLevelData d)
    {
        var kind = stats.dataTable.kind;
        switch (kind)
        {
            case UpgradeKind.Auto: return $"{d.autoDps:0.00} 회/초";
            case UpgradeKind.Crit: return $"치명타 데미지 +{(d.critMultiplier - 1f) * 100f:0.0}%";
            case UpgradeKind.Gold: return $"골드 획득량 +{(d.goldMultiplier - 1f) * 100f:0.0}%";
        }
        return "";
    }

    void RefreshInteractableOnly()
    {
        if (buyButton == null || wallet == null || stats == null || stats.dataTable == null) return;
        var data = stats.Current;
        bool canBuy = data != null && wallet.CanAfford(data.cost);
        buyButton.interactable = canBuy;
        SetColor(costText, costTMPTxt, canBuy ? affordableColor : unaffordableColor);
    }

    static void SetText(Text legacy, TMP_Text tmp, string v) { if (legacy) legacy.text = v; if (tmp) tmp.text = v; }
    static void SetColor(Text legacy, TMP_Text tmp, Color c) { if (legacy) legacy.color = c; if (tmp) tmp.color = c; }
}
