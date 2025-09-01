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
        // 강화 가능한지 체크
        if (level >= weapon.damagePerLevel.Length - 1)
        {
            Debug.Log("더 이상 강화할 수 없습니다");
            return;
        }

        level++;
        Debug.Log($"{weapon.weaponName} +{level} 강화 성공!");
    }

    // 골드 보너스도 추가
    public int GetGoldBonus()
    {
        if (weapon.goldPerLevel == null || level >= weapon.goldPerLevel.Length)
            return weapon.baseGoldBonus;

        return weapon.baseGoldBonus + weapon.goldPerLevel[level];
    }
}