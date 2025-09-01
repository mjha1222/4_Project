using UnityEngine;
using UnityEngine.UI;

public class UpgradeBtnUI : MonoBehaviour
{
    public UpgradeStats stats;
    public Gold wallet;
    public Player player;

    public Button buyButton;
    public Text titleText, levelText, descText, costText;

    void OnEnable()
    {
        if (buyButton) buyButton.onClick.AddListener(OnClickBuy);
        if (wallet) wallet.onGoldChanged += OnGoldChanged;
        Refresh();
    }

    void OnDisable()
    {
        if (buyButton) buyButton.onClick.RemoveListener(OnClickBuy);
        if (wallet) wallet.onGoldChanged -= OnGoldChanged;
    }

    void Update() => RefreshInteractableOnly();

    void OnClickBuy()
    {
        if (stats != null && stats.TryBuy(player)) Refresh();
    }

    void OnGoldChanged(double _) => Refresh();

    void Refresh()
    {
        if (stats == null || stats.dataTable == null) return;
        var data = stats.Current;
        if (data == null) return;

        if (titleText) titleText.text = "업그레이드";
        if (levelText) levelText.text = $"Lv.{stats.currentLevel}";
        if (descText) descText.text = $"치명타 x{data.critMultiplier:0.##} | Auto {data.autoDps:0.##}/s | 골드 x{data.goldMultiplier:0.##}";
        if (costText) costText.text = $"{data.cost:0}";
        RefreshInteractableOnly();
    }

    void RefreshInteractableOnly()
    {
        if (buyButton == null || wallet == null || stats == null || stats.dataTable == null) return;
        var data = stats.Current;
        buyButton.interactable = data != null && wallet.Current >= data.cost;
    }
}
