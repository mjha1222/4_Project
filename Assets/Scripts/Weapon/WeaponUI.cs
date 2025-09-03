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

        // ���� ���� ǥ��
        float critRate = weaponManager.GetCritRate();
        statsText.text = $"���ݷ�: {weaponManager.GetAttackPower()}\n" +
                         $"ġ��Ÿ: {critRate:F1}%\n" +
                         $"��� ���ʽ�: +{weaponManager.GetGoldBonus()}";
    }

    void TryUpgrade()
    {
        weaponManager.LevelUp();
        UpdateUI();
    }
}