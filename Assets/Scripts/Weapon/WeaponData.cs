using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("무기 기본 정보")]
    public string weaponName;
    public Sprite weaponIcon;
    public bool isDefaultWeapon; // 기본 무기 여부

    [Header("기본 능력치")]
    public int baseDamage;
    public float baseCritChance;
    public int baseGoldBonus;  // 기본 골드 보너스

    [Header("강화 증가치 (고정값)")]
    public int damagePerUpgrade = 1;    // 공격력 고정 증가치
    public float critPerUpgrade = 0.1f; // 치명타 확률 고정 증가치
    public int goldPerUpgrade = 1;      // 골드 보너스 고정 증가치

    [Header("구매 & 강화 비용")]
    public int buyPrice;         // 무기 최초 구매 비용
    public int upgradeCost = 10; // 무기별 강화 비용
}   