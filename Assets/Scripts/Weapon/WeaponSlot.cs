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
    public Button buyButton;


    [Header("Lock/Unlock")]
    public GameObject lockedUI; // ??? ���� UI
    public GameObject unlockedUI; // ���� ���� UI

    private bool isUnlocked = false; // ���� ����

    void Start()
    {
        // ��ư �̺�Ʈ ����
        if (equipButton != null)
        {
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(Equip);
        }

        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(Buy);
        }

        // �⺻ ����� ������ �ر�
        if (weaponData != null && weaponData.isDefaultWeapon)
        {
            isUnlocked = true;
            ShowUnlocked();
        }
        else
        {
            if (isUnlocked)
                ShowUnlocked();
            else
                ShowLocked();
        }
    }
    // ���� �� ���� UI
    void ShowLocked()
    {
        if (lockedUI != null) lockedUI.SetActive(true);
        if (unlockedUI != null) unlockedUI.SetActive(false);
    }

    // ���� �� ���� UI
    void ShowUnlocked()
    {
        if (lockedUI != null) lockedUI.SetActive(false);
        if (unlockedUI != null) unlockedUI.SetActive(true);

        if (weaponData != null)
        {
            if (icon != null) icon.sprite = weaponData.weaponIcon;
            if (nameText != null) nameText.text = $"{weaponData.weaponName} Lv.0";
            if (statText != null) statText.text =
                $"���ݷ�: {weaponData.baseDamage}\nġ��Ÿ Ȯ��: {weaponData.baseCritChance}%";
        }
    }
    // ���� ����
    void Buy()
    {
        int gold = GameManager.instance.player.playerGold;

        if (gold >= weaponData.buyPrice)
        {
            GameManager.instance.player.playerGold -= weaponData.buyPrice;
            isUnlocked = true;
            ShowUnlocked();

            // ��� UI ����
            UIManager.Instance.GoldViewText();

            Debug.Log($"{weaponData.weaponName} ���� �Ϸ�!");
        }
        else
        {
            Debug.Log("��� ����!");
        }
    }

    // ���� ����
    public void Equip()
    {
        if (!isUnlocked)
        {
            Debug.Log("�������� ���� ����� ���� �Ұ�!");
            return;
        }

        if (weaponData != null)
        {
            WeaponManager.Instance.EquipWeapon(weaponData);
        }
    }
}


