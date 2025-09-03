using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public WeaponManager weaponManager;

    [Header("Weapon UI")]
    public Image weaponImg;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI statsText;
    public Button upgradeBtn;

    void Start()
    {
        UpdateUI();

        if (upgradeBtn != null)
            upgradeBtn.onClick.AddListener(TryUpgrade);
    }

    public void UpdateUI()
    {
        if (weaponManager.currentWeapon == null) return;

        weaponImg.sprite = weaponManager.currentWeapon.weaponIcon;
        nameText.text = weaponManager.currentWeapon.weaponName;
        levelText.text = $"+{weaponManager.level}";

        // 스탯 정보 표시
        float critRate = weaponManager.GetCritRate();
        statsText.text = $"공격력: {weaponManager.GetAttackPower()}\n" +
                         $"치명타: {critRate:F1}%\n" +
                         $"골드 보너스: +{weaponManager.GetGoldBonus()}";
    }

    void TryUpgrade()
    {
        weaponManager.LevelUp();
        UpdateUI();
    }
}