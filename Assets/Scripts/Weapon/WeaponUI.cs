using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public WeaponManager weaponManager;

    [Header("Weapon UI")]
    public Image weaponImg;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statsText;
    //public Button upgradeBtn;
    //public Text levelText;

    public void UpdateUI()
    {
        if (weaponManager == null || weaponManager.currentWeapon == null) return;

        WeaponData weaponData = weaponManager.currentWeapon;
        int level = weaponManager.level;

        if (weaponImg != null) weaponImg.sprite = weaponData.weaponIcon;
        if (nameText != null) nameText.text = $"{weaponData.weaponName} Lv.{level}";

        // WeaponManager에서 직접 최종 수치 불러오기
        int atk = weaponManager.GetAttackPower();
        float crit = weaponManager.GetCritRate();

        if (statsText != null)
            statsText.text = $"공격력: {atk}\n치명타 확률: {crit}%";
    }


    public void UpdateUI(WeaponData weaponData, int level,int atk, float crit)
    {
        if (weaponData == null) return;

        if (weaponImg != null) weaponImg.sprite = weaponData.weaponIcon;
        if (nameText != null) nameText.text = $"{weaponData.weaponName} Lv.{level}";

        if (weaponManager != null)

        if (statsText != null)
            statsText.text = $"공격력: {atk}\n치명타 확률: {crit}%";
    }
}