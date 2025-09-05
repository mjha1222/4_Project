using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public UpgradeStatType stat;
    public UpgradeDataTable table;
    public Text titleText;
    public Text descText;
    public Text costText;
    public Button levelUpButton;
    public Color costEnoughColor = Color.black;
    public Color costLackColor = Color.red;
    public float holdInterval = 0.2f;
    public ClickController clickController;

    PlayerUpgrades upgrades;
    GoldWallet W => GoldWallet.Get();
    Player P => GameManager.Instance != null ? GameManager.Instance.player
                                             : (W != null ? GameManager.instance.player : null);

    void Awake()
    {
        if (levelUpButton) levelUpButton.onClick.AddListener(TryLevelUpOnce);
        if (clickController == null) clickController = FindAnyObjectByType<ClickController>();
        if (upgrades == null) upgrades = FindAnyObjectByType<PlayerUpgrades>(FindObjectsInactive.Include);
    }

    void OnEnable()
    {
        RefreshUI();
        SyncClickController();
    }

    void Update()
    {
        RefreshInteractableAndCostColor();
    }

    int GetLevel() => upgrades != null ? upgrades.GetLevel(stat) : 0;

    int CurrentCost()
    {
        if (table == null) return 0;
        int level = GetLevel();
        return Mathf.Max(0, table.baseCost + level * table.costStep);
    }

    void TryLevelUpOnce()
    {
        if (table == null || W == null) return;
        if (upgrades == null) upgrades = FindAnyObjectByType<PlayerUpgrades>(FindObjectsInactive.Include);
        if (upgrades == null) return;

        int cost = CurrentCost();
        if (!W.TrySpend(cost)) return;
        upgrades.AddLevel(stat, 1);
        GameManager.instance.player.FinalStatusSet();
        RefreshUI();
        SyncClickController();
        
    }

    void SyncClickController()
    {
        if (clickController == null || P == null) return;
        if (stat != UpgradeStatType.AutoAttack) return;
        bool enable = P.playerAutoAtt > 0f;
        clickController.SetAutoAttackEnabled(enable);
        clickController.SetAutoAttackRate(enable ? P.playerAutoAtt : 0f);
    }

    public void RefreshUI()
    {
        if (table == null) return;
        int level = GetLevel();

        if (titleText)
        {
            switch (stat)
            {
                case UpgradeStatType.CritDamage: titleText.text = "Ä¡¸íÅ¸ " + level; break;
                case UpgradeStatType.AutoAttack: titleText.text = "ÀÚµ¿ °ø°Ý " + level; break;
                case UpgradeStatType.GoldBonus: titleText.text = "°ñµå È¹µæ " + level; break;
            }
        }

        if (descText && P != null)
        {
            switch (stat)
            {
                case UpgradeStatType.CritDamage:
                    descText.text = "Ä¡¸íÅ¸ µ¥¹ÌÁö +" + P.playerCriDamaged.ToString("0.0") + "%";
                    break;
                case UpgradeStatType.AutoAttack:
                    descText.text = P.playerAutoAtt.ToString("0.0") + " È¸/ÃÊ, °ø°Ý·Â " + P.plTotalAtt;
                    break;
                case UpgradeStatType.GoldBonus:
                    descText.text = "°ñµå È¹µæ·® +" + P.playerGoldBonus.ToString("0.0") + "%";
                    break;
            }
        }

        if (costText)
        {
            int cost = CurrentCost();
            costText.text = cost.ToString("N0");
        }

        RefreshInteractableAndCostColor();
    }

    void RefreshInteractableAndCostColor()
    {
        if (levelUpButton == null || W == null || table == null) return;
        int cost = CurrentCost();
        bool enough = W.GetGold() >= cost;
        levelUpButton.interactable = enough;
        if (costText) costText.color = enough ? costEnoughColor : costLackColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (holdCo == null) holdCo = StartCoroutine(HoldRoutine());
    }

    public void OnPointerUp(PointerEventData eventData) { StopHold(); }
    public void OnPointerExit(PointerEventData eventData) { StopHold(); }

    void StopHold()
    {
        if (holdCo != null)
        {
            StopCoroutine(holdCo);
            holdCo = null;
        }
    }

    Coroutine holdCo;
    IEnumerator HoldRoutine()
    {
        TryLevelUpOnce();
        yield return new WaitForSeconds(holdInterval);
        while (true)
        {
            if (W == null || !levelUpButton || !levelUpButton.interactable || W.GetGold() < CurrentCost())
            {
                StopHold();
                yield break;
            }
            TryLevelUpOnce();
            yield return new WaitForSeconds(holdInterval);
        }
    }
}
