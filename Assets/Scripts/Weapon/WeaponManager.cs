using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public WeaponData currentWeapon;
    public WeaponData weapon;
    public int level = 0;

    [Header("UI")]
    public WeaponUI weaponUI;

    [Header("UI Panels")]
    public GameObject bagPanel; // 무기 가방
    public GameObject inGamePanel; // 인게임 화면

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void OpenBag()
    {
        if (bagPanel != null)
            bagPanel.SetActive(true);

        Debug.Log("무기 가방 오픈");
    }

    public void CloseBag()
    {
        if (bagPanel != null)
            bagPanel.SetActive(false);
        Debug.Log("무기 가방 닫기");
    }

    public void EquipWeapon(WeaponData newWeapon, int newLevel = -1)
    {
        if (newWeapon == null) return;

        currentWeapon = newWeapon;

        if (newLevel >= 0)
            level = newLevel;

        int atk = GetAttackPower();
        float crit = GetCritRate();

        Debug.Log($"{newWeapon.weaponName} 장착 완료! (Lv.{level}, 공격력 {atk}, 치명타 {crit}%)");

        if (weaponUI != null)
        {
            weaponUI.UpdateUI(currentWeapon, level, atk, crit);
        }
    }

    public int GetAttackPower()
    {
        if (currentWeapon == null) return 0;

        int bonus = 0;
        if (currentWeapon.damagePerLevel != null && level < currentWeapon.damagePerLevel.Length)
            bonus = currentWeapon.damagePerLevel[level];

        return currentWeapon.baseDamage + bonus;
    }

    public float GetCritRate()
    {
        if (currentWeapon == null) return 0f;

        float bonus = 0f;
        if (currentWeapon.critPerLevel != null && level < currentWeapon.critPerLevel.Length)
            bonus = currentWeapon.critPerLevel[level];

        return currentWeapon.baseCritChance + bonus;
    }

    public void LevelUp()
    {
        if (currentWeapon == null) return;

        // 강화 가능한지 체크
        if (level >= currentWeapon.damagePerLevel.Length - 1)
        {
            //Debug.Log("더 이상 강화할 수 없습니다");
            return;
        }

        level++;
        //Debug.Log($"{currentWeapon.weaponName} +{level} 강화 성공!");
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
    
    }
}
