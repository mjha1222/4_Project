using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("���� �⺻ ����")]
    public string weaponName;
    public Sprite weaponIcon;
    public bool isDefaultWeapon; // �⺻ ���� ����

    [Header("�⺻ �ɷ�ġ")]
    public int baseDamage;
    public float baseCritChance;
    public int baseGoldBonus;  // �⺻ ��� ���ʽ�

    [Header("��ȭ ����ġ (������)")]
    public int damagePerUpgrade = 1;    // ���ݷ� ���� ����ġ
    public float critPerUpgrade = 0.1f; // ġ��Ÿ Ȯ�� ���� ����ġ
    public int goldPerUpgrade = 1;      // ��� ���ʽ� ���� ����ġ

    [Header("���� & ��ȭ ���")]
    public int buyPrice;         // ���� ���� ���� ���
    public int upgradeCost = 10; // ���⺰ ��ȭ ���
}   