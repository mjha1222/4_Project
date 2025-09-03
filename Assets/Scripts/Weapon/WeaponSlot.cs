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

    private Button button;  // ���� ��ü ��ư

    void Awake()
    {
        button = GetComponent<Button>(); // �ڱ� �ڽſ��� ��ư ã��
    }

    void Start()
    {
        if (weaponData != null)
        {
            icon.sprite = weaponData.weaponIcon;
            nameText.text = weaponData.weaponName;
            statText.text = $"���ݷ�: {weaponData.baseDamage}\nġ��Ÿ Ȯ��: {weaponData.baseCritChance}%";

            // ��ư ����
            if (equipButton != null)
                equipButton.onClick.AddListener(Equip);
        }
    }
    public void Equip()
    {
        WeaponManager.instance.EquipWeapon(weaponData);
    }
}
