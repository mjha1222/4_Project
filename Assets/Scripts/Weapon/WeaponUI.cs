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
        if (weaponManager != null)
        {
            UpdateUI(weaponManager.currentWeapon, weaponManager.level);
        }
    }


    public void UpdateUI(WeaponData weaponData, int level)
    {
        if (weaponData == null) return;

        if (weaponImg != null) weaponImg.sprite = weaponData.weaponIcon;
        if (nameText != null) nameText.text = $"{weaponData.weaponName} Lv.{level}";

        int atk = 0;
        float crit = 0f;

        if (weaponManager != null && weaponManager.currentWeapon == weaponData)
        {
            atk = weaponManager.GetAttackPower();
            crit = weaponManager.GetCritRate();
        }
        else
        {
            // ���� ���� �� �̸����� ��
            atk = weaponData.baseDamage;
            if (weaponData.damagePerLevel != null && level < weaponData.damagePerLevel.Length)
                atk += weaponData.damagePerLevel[level];

            crit = weaponData.baseCritChance;
            if (weaponData.critPerLevel != null && level < weaponData.critPerLevel.Length)
                crit += weaponData.critPerLevel[level];
        }

        if (statsText != null)
            statsText.text = $"���ݷ�: {atk}\nġ��Ÿ Ȯ��: {crit}%";
    }
}