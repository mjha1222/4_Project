using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public WeaponData weaponData;

    [Header("UI")]
    public Image icon;
    public Text nameText;
    public Text statText;
    public Button buyButton;
    public Button upgradeButton;

    [Header("Lock/Unlock")]
    public GameObject lockedUI;
    public GameObject unlockedUI;

    private bool isUnlocked = false;
    private int level = 0;

    public void Start()
    {
        // 버튼 이벤트 연결
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(Buy);
        }

        if (upgradeButton != null)
        {
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(Upgrade);
            upgradeButton.gameObject.SetActive(false); // 시작시 비활성화
        }

        // 기본 무기 처리
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
        if (lockedUI != null) lockedUI.SetActive(true);
        if (unlockedUI != null) unlockedUI.SetActive(false);

        if (buyButton != null) buyButton.gameObject.SetActive(true);
        if (upgradeButton != null) upgradeButton.gameObject.SetActive(false);
    }

    public void ShowUnlocked()
    {
        if (lockedUI != null) lockedUI.SetActive(false);
        if (unlockedUI != null) unlockedUI.SetActive(true);

        Refresh();

        if (buyButton != null) buyButton.gameObject.SetActive(false);
        if (upgradeButton != null) upgradeButton.gameObject.SetActive(true);
    }

    public void Refresh()
    {
        if (weaponData == null) return;

        if (icon != null) icon.sprite = weaponData.weaponIcon;
        if (nameText != null) nameText.text = $"{weaponData.weaponName} Lv.{level}";

        int atk = weaponData.baseDamage + (level * (weaponData.damagePerLevel.Length > 0 ? weaponData.damagePerLevel[0] : 1));
        float crit = weaponData.baseCritChance + (level * (weaponData.critPerLevel.Length > 0 ? weaponData.critPerLevel[0] : 1));

        if (statText != null)
            statText.text = $"공격력: {atk}\n치명타 확률: {crit}%";
    }

    public void Buy()
    {
        if (weaponData == null) return;

        if (GoldWallet.Get().TrySpend(weaponData.buyPrice))
        {
            isUnlocked = true;
            ShowUnlocked();

            if (WeaponManager.Instance != null)
            {
                WeaponManager.Instance.currentWeapon = weaponData;
                WeaponManager.Instance.level = level;
                WeaponManager.Instance.weaponUI.UpdateUI();
            }

            Debug.Log($"{weaponData.weaponName} 구매 완료!");
        }
        else
        {
            Debug.Log("골드 부족!");
        }
    }

    public void Upgrade()
    {
        if (!isUnlocked)
        {
            Debug.Log("구매하지 않은 무기는 강화 불가!");
            return;
        }

        int cost = 10 * (level + 1);
        if (GoldWallet.Get().TrySpend(cost))
        {
            level++;

            Refresh();

            // 무기 매니저랑 동기화
            if (WeaponManager.Instance != null)
            {
                // 현재 장착 무기 == 지금 강화 중인 무기일 때만 동기화
                if (WeaponManager.Instance.currentWeapon == weaponData)
                {
                    WeaponManager.Instance.level = level;
                    WeaponManager.Instance.weaponUI.UpdateUI(weaponData, level);
                }
            }

            Debug.Log($"{weaponData.weaponName} Lv.{level} 강화 성공!");
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

        if (weaponData != null)
        {
            // 무기 매니저에 무기 장착
            WeaponManager.Instance.EquipWeapon(weaponData, level);

            if (WeaponManager.Instance.weaponUI != null)
            {
                WeaponManager.Instance.weaponUI.UpdateUI(weaponData, level);
            }
        }
    }
}
