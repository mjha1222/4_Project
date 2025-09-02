using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData weapon;
    public int level = 0;

    public int GetAttackPower()
    {
        int bonus = 0;
        if (level < weapon.damagePerLevel.Length)
        {
            bonus = weapon.damagePerLevel[level];
        }

        return weapon.baseDamage + bonus;
    }

    public float GetCritRate()
    {
        float bonusCrit = 0f;
        if (level < weapon.critPerLevel.Length)
        {
            bonusCrit = weapon.critPerLevel[level];
        }

        return weapon.baseCritChance + bonusCrit;
    }

    public void LevelUp()
    {
        // ��ȭ �������� üũ
        if (level >= weapon.damagePerLevel.Length - 1)
        {
            Debug.Log("�� �̻� ��ȭ�� �� �����ϴ�");
            return;
        }

        level++;
        Debug.Log($"{weapon.weaponName} +{level} ��ȭ ����!");
    }

    // ��� ���ʽ��� �߰�
    public int GetGoldBonus()
    {
        if (weapon.goldPerLevel == null || level >= weapon.goldPerLevel.Length)
            return weapon.baseGoldBonus;

        return weapon.baseGoldBonus + weapon.goldPerLevel[level];
    }
}