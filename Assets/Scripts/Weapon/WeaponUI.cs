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

    void Start()
    {
        UpdateUI();

        //if (upgradeBtn != null)
        //    upgradeBtn.onClick.AddListener(TryUpgrade);
    }

    public void UpdateUI()
    {
        if (weaponManager.currentWeapon == null) return;

        WeaponData weapon = WeaponManager.Instance.currentWeapon;
        weaponImg.sprite = weapon.weaponIcon;
        nameText.text = weapon.weaponName;
        statsText.text = $"공격력: {weapon.baseDamage}\n치명타: {weapon.baseCritChance}%";
    }

    void TryUpgrade()
    {
        weaponManager.LevelUp();
        UpdateUI();
    }
}