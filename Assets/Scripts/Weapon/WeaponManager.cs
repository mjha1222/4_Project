using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public WeaponData currentWeapon;
    //public WeaponData weapon;
    public int level = 0;

    [Header("UI")]
    public WeaponUI weaponUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        if (newWeapon == null)
        {
            //Debug.LogWarning("������ ���Ⱑ �����ϴ�!");
            return;
        }

        currentWeapon = newWeapon;
        Debug.Log($"{newWeapon.weaponName} ���� �Ϸ�!");

        if (weaponUI != null)
            weaponUI.UpdateUI();
        return;
            //Debug.LogWarning("WeaponUI ������ �ȵ�!");
    }

    public int GetAttackPower()
    {
        int bonus = 0;
        if (level < currentWeapon.damagePerLevel.Length)
            bonus = currentWeapon.damagePerLevel[level];

        return currentWeapon.baseDamage + bonus;
    }

    public float GetCritRate()
    {
        float bonusCrit = 0f;
        if (level < currentWeapon.critPerLevel.Length)
            bonusCrit = currentWeapon.critPerLevel[level];

        return currentWeapon.baseCritChance + bonusCrit;
    }

    public void LevelUp()
    {
        if (currentWeapon == null) return;

        // ��ȭ �������� üũ
        if (level >= currentWeapon.damagePerLevel.Length - 1)
        {
            //Debug.Log("�� �̻� ��ȭ�� �� �����ϴ�");
            return;
        }

        level++;
        //Debug.Log($"{currentWeapon.weaponName} +{level} ��ȭ ����!");
    }

    public int GetGoldBonus()
    {
        if (currentWeapon == null) return 0;

        if (currentWeapon.goldPerLevel == null || level >= currentWeapon.goldPerLevel.Length)
            return currentWeapon.baseGoldBonus;

        return currentWeapon.baseGoldBonus + currentWeapon.goldPerLevel[level];
    }
    void Start()
    {
        //if (weapon == null)
        //{
        //    // �⺻ ���� ����
        //    weapon = Resources.Load<WeaponData>("WeaponScriptableObject/WSword");
        //}
        //EquipWeapon(weapon);
    }
}
