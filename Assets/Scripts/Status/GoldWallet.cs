using System;
using UnityEngine;
using UnityEngine.UI;   // Legacy Text
using TMPro;           // TextMeshPro

public class GoldWallet : MonoBehaviour
{
    [Header("Wallet")]
    [SerializeField] double gold = 0;
    public event Action<double> onGoldChanged;

    [Header("UI (둘 중 하나만 연결)")]
    [SerializeField] Text legacyText;
    [SerializeField] TMP_Text tmpText;

    [Header("Label/Amount 색상 & 라벨")]
    [SerializeField] string label = "Gold";
    [SerializeField] Color labelColor = new Color(1f, 0.83f, 0f);
    [SerializeField] Color amountColor = Color.white;

    public double Current => gold;

    void OnEnable()
    {
        if (legacyText != null)
        {
            legacyText.supportRichText = true;
            if (legacyText.color != Color.white) legacyText.color = Color.white;
        }
        if (tmpText != null)
        {
            tmpText.richText = true;
            if (tmpText.color != Color.white) tmpText.color = Color.white;
        }

        RefreshUI();
        onGoldChanged?.Invoke(gold);
    }

    public void SetGold(double value)
    {
        double v = Math.Max(0, value);
        if (Math.Abs(v - gold) < 1e-9) return;
        gold = v;
        onGoldChanged?.Invoke(gold);
        RefreshUI();
    }

    public void AddGold(double amount)
    {
        if (amount <= 0) return;
        gold += amount;
        onGoldChanged?.Invoke(gold);
        RefreshUI();
    }

    public bool TrySpend(double amount)
    {
        if (amount < 0) return false;
        if (gold + 1e-9 < amount) return false;
        gold -= amount;
        onGoldChanged?.Invoke(gold);
        RefreshUI();
        return true;
    }

    public bool CanAfford(double amount) => gold + 1e-9 >= amount;

    void RefreshUI()
    {
        string labelHex = ColorUtility.ToHtmlStringRGB(labelColor);
        string amountHex = ColorUtility.ToHtmlStringRGB(amountColor);

        string s = $"<color=#{labelHex}>{label}</color> <color=#{amountHex}>{gold:0}</color>";

        if (legacyText != null) legacyText.text = s;
        if (tmpText != null) tmpText.text = s;
    }
}
