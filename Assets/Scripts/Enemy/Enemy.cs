using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData Data;
    private Animator anim;
    public int nowHP;
    private const string HitTrigger = "Hit";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Start()
    {
        nowHP = Data.HP;
        EnemyManager.Instance.nowEnemy = this;
    }

    public void TakeDamage(int damage)
    {
        nowHP -= damage;
        anim.ResetTrigger(HitTrigger);
        anim.SetTrigger(HitTrigger);
        Debug.Log("Hit");
        EnemyManager.Instance.RefreshHPbar();
    }
    
}
