using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public WeaponData currentWeapon;
    public List<WeaponSlot> weapons;
    public int level;

    [Header("UI")]
    public WeaponUI weaponUI;

    [Header("UI Panels")]
    public GameObject bagPanel; // 무기 가방
    public GameObject inGamePanel; // 인게임 화면

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        level = 0;
        UpdateCurrentWeaponUI();
    }




    public void EquipWeapon(WeaponData newWeapon, int newLevel = 0)
    {
        if (newWeapon == null) return;

        currentWeapon = newWeapon;
        level = newLevel;

        UpdateCurrentWeaponUI();
        
    }

    public void UpdateCurrentWeaponUI()
    {
        if (weaponUI != null && currentWeapon != null)
        {
            int atk = GetAttackPower();
            float crit = GetCritRate();
            weaponUI.UpdateUI(currentWeapon, level, atk, crit);
        }
    }

    public int GetAttackPower()
    {
        if (currentWeapon == null) return 0;
        return currentWeapon.baseDamage + (level * currentWeapon.damagePerUpgrade);
    }

    public float GetCritRate()
    {
        if (currentWeapon == null) return 0f;
        return currentWeapon.baseCritChance + (level * currentWeapon.critPerUpgrade);
    }

    public int GetGoldBonus()
    {
        if (currentWeapon == null) return 0;
        return currentWeapon.baseGoldBonus + (level * currentWeapon.goldPerUpgrade);
    }

    public void LevelUp()
    {
        if (currentWeapon == null) return;

        level++;
        UpdateCurrentWeaponUI();
    }

        public void OpenBag()
    {
        if (bagPanel != null)
            bagPanel.SetActive(true);

        Debug.Log("무기 가방 오픈");
    }



}
