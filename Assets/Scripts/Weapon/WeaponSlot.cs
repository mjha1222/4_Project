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

    private Button button;  // 슬롯 전체 버튼

    void Awake()
    {
        button = GetComponent<Button>(); // 자기 자신에서 버튼 찾음
    }

    void Start()
    {
        if (weaponData != null)
        {
            icon.sprite = weaponData.weaponIcon;
            nameText.text = weaponData.weaponName;
            statText.text = $"공격력: {weaponData.baseDamage}\n치명타 확률: {weaponData.baseCritChance}%";

            // 버튼 연결
            if (equipButton != null)
                equipButton.onClick.AddListener(Equip);
        }
    }
    public void Equip()
    {
        WeaponManager.instance.EquipWeapon(weaponData);
    }
}
