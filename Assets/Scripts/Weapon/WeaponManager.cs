using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;   // �̱���

    [Header("���� ���� ����")]
    public WeaponData currentWeapon;        // ���� ������ ����
    public int level = 0;                   // ���� ��ȭ �ܰ�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� ���� (�ܺο��� ���⸦ �����ؼ� ��ü ����)
    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        level = 0; // �� ���� �����ϸ� ��ȭ �ܰ� �ʱ�ȭ
        Debug.Log($"{currentWeapon.weaponName} ����!");
    }

    // ���ݷ� ��� (���� ���ʽ� ���)
    public int GetAttackPower()
    {
        if (currentWeapon == null) return 0;

        int totalBonus = 0;
        for (int i = 0; i <= level && i < currentWeapon.damagePerLevel.Length; i++)
        {
            totalBonus += currentWeapon.damagePerLevel[i];
        }

        return currentWeapon.baseDamage + totalBonus;
    }

    // ġ��Ÿ Ȯ�� ��� (���� ���ʽ� ���)
    public float GetCritRate()
    {
        if (currentWeapon == null) return 0f;

        float totalCrit = 0f;
        for (int i = 0; i <= level && i < currentWeapon.critPerLevel.Length; i++)
        {
            totalCrit += currentWeapon.critPerLevel[i];
        }

        return currentWeapon.baseCritChance + totalCrit;
    }

    // ��� ���ʽ� (�ܰ躰 ���� ����)
    public int GetGoldBonus()
    {
        if (currentWeapon == null) return 0;

        int totalGoldBonus = currentWeapon.baseGoldBonus;

        for (int i = 0; i <= level && i < currentWeapon.goldPerLevel.Length; i++)
        {
            totalGoldBonus += currentWeapon.goldPerLevel[i];
        }

        return totalGoldBonus;
    }

    // ���� ��ȭ
    public void LevelUp()
    {
        if (currentWeapon == null) return;

        if (level >= currentWeapon.damagePerLevel.Length - 1)
        {
            Debug.Log("�� �̻� ��ȭ�� �� �����ϴ�!");
            return;
        }

        level++;
        Debug.Log($"{currentWeapon.weaponName} +{level} ���׷��̵� ����!");
    }
}
