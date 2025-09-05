using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public WeaponData weaponData;

    [Header("UI")]
    public Image icon;
    public Text nameText;
    public Text statText;
    public Text costText;
    public Button buyButton;
    public Button upgradeButton;


    [Header("Lock/Unlock")]
    public GameObject lockedUI;
    public GameObject unlockedUI;

    private bool isUnlocked = false;
    private int level = 0;

    void Start()
    {
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(Buy);
        }

        if (upgradeButton != null)
        {
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(Upgrade);
            upgradeButton.gameObject.SetActive(false);
        }

        if (weaponData != null && weaponData.isDefaultWeapon)
        {
            isUnlocked = true;
            ShowUnlocked();
        }
        else
        {
            ShowLocked();
        }
    }

    public void ShowLocked()
    {
        lockedUI?.SetActive(true);
        unlockedUI?.SetActive(false);

        buyButton?.gameObject.SetActive(true);
        upgradeButton?.gameObject.SetActive(false);
    }

    public void ShowUnlocked()
    {
        if (lockedUI != null) lockedUI.SetActive(false);
        if (unlockedUI != null) unlockedUI.SetActive(true);

        Refresh();

        // 기본 무기는 구매 버튼 필요 없음
        if (weaponData != null && weaponData.isDefaultWeapon)
        {
            if (buyButton != null) buyButton.gameObject.SetActive(false);
            if (upgradeButton != null) upgradeButton.gameObject.SetActive(true);
        }
        else
        {
            if (buyButton != null) buyButton.gameObject.SetActive(false);
            if (upgradeButton != null) upgradeButton.gameObject.SetActive(true);
        }
    }

    public void Refresh()
    {
        if (weaponData == null) return;

        if (icon != null) icon.sprite = weaponData.weaponIcon;
        if (nameText != null) nameText.text = $"{weaponData.weaponName} Lv.{level}";

        int atk = weaponData.baseDamage + (level * weaponData.damagePerUpgrade);
        float crit = weaponData.baseCritChance + (level * weaponData.critPerUpgrade);

        if (statText != null)
            statText.text = $"공격력: {atk}\n치명타 확률: {crit}%";

        if (costText != null)
        { 
            int nextCost = weaponData.upgradeCost * (level + 1); // 무기별 비용 반영
            costText.text = nextCost.ToString("N0");
        }
    }

    public void Buy()
    {
        if (weaponData == null) return;

        if (GoldWallet.Get().TrySpend(weaponData.buyPrice))
        {
            isUnlocked = true;
            ShowUnlocked();

            WeaponManager.Instance?.EquipWeapon(weaponData, level);

            Debug.Log($"{weaponData.weaponName} 구매 완료!");
        }
        else
        {
            Debug.Log("골드 부족!");
        }
    }

    public void Upgrade()
    {
        if (!isUnlocked) return;

        int cost = weaponData.upgradeCost * (level + 1);  // 무기별 비용 반영
        if (GoldWallet.Get().TrySpend(cost))
        {
            level++;
            Refresh();

            if (WeaponManager.Instance != null && WeaponManager.Instance.currentWeapon == weaponData)
            {
                WeaponManager.Instance.EquipWeapon(weaponData, level);
            }

            Debug.Log($"{weaponData.weaponName} Lv.{level} 강화 성공! (비용 {cost})");
        }
        else
        {
            Debug.Log("골드 부족!");
        }
    }

    public void Equip()
    {
        if (!isUnlocked)
        {
            Debug.Log("구매하지 않은 무기는 장착 불가!");
            return;
        }

        WeaponManager.Instance?.EquipWeapon(weaponData, level);
    }
}
