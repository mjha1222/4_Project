using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("���� �⺻ ����")]
    public string weaponName;
    public Sprite weaponIcon;
    public int buyPrice;  // ���� ����

    [Header("�⺻ �ɷ�ġ")]
    public int baseDamage;
    public float baseCritChance;
    public int baseGoldBonus;  // �⺻ ��� ���ʽ�

    [Header("��ȭ ��ġ")]
    public int[] damagePerLevel;
    public float[] critPerLevel;
    public int[] goldPerLevel;     // ������ ��� ���ʽ� ����
    public int[] upgradeCosts;     // �� ���� ��ȭ ���

    [Header("ġ��Ÿ ����")]
    public float critMultiplier = 2.0f;  // ġ��Ÿ ����
}   