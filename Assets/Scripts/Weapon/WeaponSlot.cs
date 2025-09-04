using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSlot : MonoBehaviour
{
    public WeaponData weaponData;

    [Header("UI")]
    public Image icon;
    public Text nameText;
    public Text statText;
    public Button equipButton;
    public Button buyButton;


    [Header("Lock/Unlock")]
    public GameObject lockedUI; // ??? 상태 UI
    public GameObject unlockedUI; // 무기 정보 UI

    private bool isUnlocked = false; // 구매 여부

    void Start()
    {
        // 버튼 이벤트 연결
        if (equipButton != null)
        {
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(Equip);
        }

        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(Buy);
        }

        // 기본 무기는 무조건 해금
        if (weaponData != null && weaponData.isDefaultWeapon)
        {
            isUnlocked = true;
            ShowUnlocked();
        }
        else
        {
            if (isUnlocked)
                ShowUnlocked();
            else
                ShowLocked();
        }
    }
    // 구매 전 상태 UI
    void ShowLocked()
    {
        if (lockedUI != null) lockedUI.SetActive(true);
        if (unlockedUI != null) unlockedUI.SetActive(false);
    }

    // 구매 후 상태 UI
    void ShowUnlocked()
    {
        if (lockedUI != null) lockedUI.SetActive(false);
        if (unlockedUI != null) unlockedUI.SetActive(true);

        if (weaponData != null)
        {
            if (icon != null) icon.sprite = weaponData.weaponIcon;
            if (nameText != null) nameText.text = $"{weaponData.weaponName} Lv.0";
            if (statText != null) statText.text =
                $"공격력: {weaponData.baseDamage}\n치명타 확률: {weaponData.baseCritChance}%";
        }
    }
    // 구매 로직
    void Buy()
    {
        int gold = GameManager.instance.player.playerGold;

        if (gold >= weaponData.buyPrice)
        {
            GameManager.instance.player.playerGold -= weaponData.buyPrice;
            isUnlocked = true;
            ShowUnlocked();

            // 골드 UI 갱신
            UIManager.Instance.GoldViewText();

            Debug.Log($"{weaponData.weaponName} 구매 완료!");
        }
        else
        {
            Debug.Log("골드 부족!");
        }
    }

    // 장착 로직
    public void Equip()
    {
        if (!isUnlocked)
        {
            Debug.Log("구매하지 않은 무기는 장착 불가!");
            return;
        }

        if (weaponData != null)
        {
            WeaponManager.Instance.EquipWeapon(weaponData);
        }
    }
}


