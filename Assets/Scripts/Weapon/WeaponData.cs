using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("무기 기본 정보")]
    public string weaponName;
    public Sprite weaponIcon;
    public int buyPrice;  // 구매 가격

    [Header("기본 능력치")]
    public int baseDamage;
    public float baseCritChance;
    public int baseGoldBonus;  // 기본 골드 보너스

    [Header("강화 수치")]
    public int[] damagePerLevel;
    public float[] critPerLevel;
    public int[] goldPerLevel;     // 레벨별 골드 보너스 증가
    public int[] upgradeCosts;     // 각 레벨 강화 비용

    [Header("치명타 배율")]
    public float critMultiplier = 2.0f;  // 치명타 배율
}   