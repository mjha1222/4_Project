using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;   // �̱��� �ν��Ͻ�

    public WeaponData currentWeapon; // ���� ���� ����
    public int level = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �����ҰŸ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        level = 0; // �� ����� �ʱ� ������ ����
        Debug.Log($"���� �Ϸ�: {newWeapon.weaponName}");
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
            Debug.Log("�� �̻� ��ȭ�� �� �����ϴ�");
            return;
        }

        level++;
        Debug.Log($"{currentWeapon.weaponName} +{level} ��ȭ ����!");
    }

    public int GetGoldBonus()
    {
        if (currentWeapon == null) return 0;

        if (currentWeapon.goldPerLevel == null || level >= currentWeapon.goldPerLevel.Length)
            return currentWeapon.baseGoldBonus;

        return currentWeapon.baseGoldBonus + currentWeapon.goldPerLevel[level];
    }
}
