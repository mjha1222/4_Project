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

    void Start()
    {
        if (weaponData != null)
        {
            icon.sprite = weaponData.weaponIcon;
            nameText.text = weaponData.weaponName;
            statText.text = $"공격력: {weaponData.baseDamage}\n치명타 확률: {weaponData.baseCritChance}%";

            if (equipButton != null)
            {
                equipButton.onClick.RemoveAllListeners();
                equipButton.onClick.AddListener(Equip);
            }
        }
    }
    public void Equip()
    {
        if (weaponData != null)
        {
            WeaponManager.Instance.EquipWeapon(weaponData);
        }
    }
}
