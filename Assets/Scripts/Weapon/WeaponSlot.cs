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

        // 강화 수치 합산 계산으로 변경
        int atk = weaponData.baseDamage;
        float crit = weaponData.baseCritChance;

        if (weaponData.damagePerLevel != null && weaponData.damagePerLevel.Length > 0)
        {
            for (int i = 0; i < level && i < weaponData.damagePerLevel.Length; i++)
                atk += weaponData.damagePerLevel[i];
        }

        if (weaponData.critPerLevel != null && weaponData.critPerLevel.Length > 0)
        {
            for (int i = 0; i < level && i < weaponData.critPerLevel.Length; i++)
                crit += weaponData.critPerLevel[i];
        }

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
                WeaponManager.Instance.EquipWeapon(weaponData, level);
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
        if (!isUnlocked) return;

        int cost = 10 * (level + 1);
        if (GoldWallet.Get().TrySpend(cost))
        {
            level++;
            Refresh();

            if (WeaponManager.Instance != null && WeaponManager.Instance.currentWeapon == weaponData)
            {
                WeaponManager.Instance.level = level;
                WeaponManager.Instance.EquipWeapon(weaponData, level); //  강화 후 매니저에도 반영
                Debug.Log($"{weaponData.weaponName} Lv.{level} 강화 성공!");
            }
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
            WeaponManager.Instance.EquipWeapon(weaponData, level);
        }
    }
}
