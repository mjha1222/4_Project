using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData Data;
    private Animator anim;
    public int nowHP;
    private const string HitTrigger = "Hit";
    public int rewardGold;

    private bool isDead = false;


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


        if (nowHP <= 0)
        {
            Die();
            return;
        }

    }

    void Die()
    {

        if (isDead) return;
        isDead = true;
        Destroy(gameObject);   //ÆÄ±«
    }

}
