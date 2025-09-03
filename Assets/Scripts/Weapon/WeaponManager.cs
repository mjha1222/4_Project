using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;   // 싱글턴 인스턴스

    public WeaponData currentWeapon; // 현재 장착 무기
    public int level = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지할거면
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        level = 0; // 새 무기는 초기 레벨로 시작
        Debug.Log($"장착 완료: {newWeapon.weaponName}");
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

        // 강화 가능한지 체크
        if (level >= currentWeapon.damagePerLevel.Length - 1)
        {
            Debug.Log("더 이상 강화할 수 없습니다");
            return;
        }

        level++;
        Debug.Log($"{currentWeapon.weaponName} +{level} 강화 성공!");
    }

    public int GetGoldBonus()
    {
        if (currentWeapon == null) return 0;

        if (currentWeapon.goldPerLevel == null || level >= currentWeapon.goldPerLevel.Length)
            return currentWeapon.baseGoldBonus;

        return currentWeapon.baseGoldBonus + currentWeapon.goldPerLevel[level];
    }
}
