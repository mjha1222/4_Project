using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;   // 싱글톤

    [Header("현재 장착 무기")]
    public WeaponData currentWeapon;        // 현재 장착된 무기
    public int level = 0;                   // 현재 강화 단계

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

    // 무기 장착 (외부에서 무기를 선택해서 교체 가능)
    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        level = 0; // 새 무기 장착하면 강화 단계 초기화
        Debug.Log($"{currentWeapon.weaponName} 장착!");
    }

    // 공격력 계산 (누적 보너스 방식)
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

    // 치명타 확률 계산 (누적 보너스 방식)
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

    // 골드 보너스 (단계별 적용 가능)
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

    // 무기 강화
    public void LevelUp()
    {
        if (currentWeapon == null) return;

        if (level >= currentWeapon.damagePerLevel.Length - 1)
        {
            Debug.Log("더 이상 강화할 수 없습니다!");
            return;
        }

        level++;
        Debug.Log($"{currentWeapon.weaponName} +{level} 업그레이드 성공!");
    }
}
