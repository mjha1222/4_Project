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

        // WeaponManager���� ���� ���� ��ġ �ҷ�����
        int atk = weaponManager.GetAttackPower();
        float crit = weaponManager.GetCritRate();

        if (statsText != null)
            statsText.text = $"���ݷ�: {atk}\nġ��Ÿ Ȯ��: {crit}%";
    }


    public void UpdateUI(WeaponData weaponData, int level,int atk, float crit)
    {
        if (weaponData == null) return;

        if (weaponImg != null) weaponImg.sprite = weaponData.weaponIcon;
        if (nameText != null) nameText.text = $"{weaponData.weaponName} Lv.{level}";

        if (weaponManager != null)

        if (statsText != null)
            statsText.text = $"���ݷ�: {atk}\nġ��Ÿ Ȯ��: {crit}%";
    }
}