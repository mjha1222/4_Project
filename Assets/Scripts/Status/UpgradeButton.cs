using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public UpgradeStatType stat;
    public PlayerUpgrades upgrades;
    public UpgradeDataTable table;

    public Text titleText;
    public Text descText;
    public Text costText;
    public Button levelUpButton;

    public Color costEnoughColor = Color.black;
    public Color costLackColor = Color.red;
    public float holdInterval = 0.2f;

    Coroutine holdCo;
    GoldWallet Wallet => GoldWallet.Get();

    void Awake()
    {
        if (levelUpButton) levelUpButton.onClick.AddListener(TryLevelUpOnce);
    }

    void Start()
    {
        RefreshUI();
    }

    void Update()
    {
        RefreshInteractableAndCostColor();
    }

    void TryLevelUpOnce()
    {
        if (upgrades == null || table == null || Wallet == null) return;
        int curLv = upgrades.GetLevel(stat);
        int cost = table.GetCost(curLv);
        if (!Wallet.TrySpend(cost)) return;
        upgrades.AddLevel(stat, 1);
        RefreshUI();
    }

    void RefreshUI()
    {
        if (upgrades == null || table == null) return;
        int lv = upgrades.GetLevel(stat);

        if (titleText)
        {
            switch (stat)
            {
                case UpgradeStatType.CritDamage: titleText.text = "Ä¡¸íÅ¸ " + lv; break;
                case UpgradeStatType.AutoAttack: titleText.text = "ÀÚµ¿ °ø°Ý " + lv; break;
                case UpgradeStatType.GoldBonus: titleText.text = "°ñµå È¹µæ " + lv; break;
            }
        }

        if (descText)
        {
            switch (stat)
            {
                case UpgradeStatType.CritDamage:
                    descText.text = "Ä¡¸íÅ¸ µ¥¹ÌÁö +" + table.GetValue(UpgradeStatType.CritDamage, lv).ToString("0.0") + "%";
                    break;
                case UpgradeStatType.AutoAttack:
                    float rate = table.GetValue(UpgradeStatType.AutoAttack, lv);
                    int atk = table.GetAttackBonusFromAutoLevel(lv);
                    descText.text = rate.ToString("0.0") + " È¸/ÃÊ, °ø°Ý·Â " + atk;
                    break;
                case UpgradeStatType.GoldBonus:
                    descText.text = "°ñµå È¹µæ·® +" + table.GetValue(UpgradeStatType.GoldBonus, lv).ToString("0.0") + "%";
                    break;
            }
        }

        if (costText)
        {
            int cost = table.GetCost(lv);
            costText.text = cost.ToString("N0");
        }

        RefreshInteractableAndCostColor();
    }

    void RefreshInteractableAndCostColor()
    {
        if (levelUpButton == null || Wallet == null || table == null || upgrades == null) return;
        int lv = upgrades.GetLevel(stat);
        int cost = table.GetCost(lv);
        bool enough = Wallet.GetGold() >= cost;
        levelUpButton.interactable = enough;
        if (costText) costText.color = enough ? costEnoughColor : costLackColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (holdCo == null) holdCo = StartCoroutine(HoldRoutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopHold();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopHold();
    }

    void StopHold()
    {
        if (holdCo != null)
        {
            StopCoroutine(holdCo);
            holdCo = null;
        }
    }

    IEnumerator HoldRoutine()
    {
        TryLevelUpOnce();
        yield return new WaitForSeconds(holdInterval);
        while (true)
        {
            int lv = upgrades.GetLevel(stat);
            int cost = table.GetCost(lv);
            if (Wallet == null || !levelUpButton.interactable || Wallet.GetGold() < cost)
            {
                holdCo = null;
                yield break;
            }
            TryLevelUpOnce();
            yield return new WaitForSeconds(holdInterval);
        }
    }
}
