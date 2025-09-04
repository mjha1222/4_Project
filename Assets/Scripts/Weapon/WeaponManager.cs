using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public WeaponData currentWeapon;
    //public WeaponData weapon;
    public int level = 0;

    [Header("UI")]
    public WeaponUI weaponUI;

    [Header("UI Panels")]
    public GameObject bagPanel; // 무기 가방
    public GameObject inGamePanel; // 인게임 화면

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


    public void EquipWeapon(WeaponData newWeapon)
    {
        if (newWeapon == null)
        {
            //Debug.LogWarning("장착할 무기가 없습니다!");
            return;
        }

        currentWeapon = newWeapon;
        Debug.Log($"{newWeapon.weaponName} 장착 완료!");

        if (weaponUI != null)
            weaponUI.UpdateUI();
        return;
            //Debug.LogWarning("WeaponUI 연결이 안됨!");
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
        if (bagPanel == null)
            bagPanel = GameObject.Find("BagPanel");
    }
}
