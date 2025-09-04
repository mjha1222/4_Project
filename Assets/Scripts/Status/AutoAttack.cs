using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    float acc;
    Player P => GoldWallet.Get() ? GoldWallet.Get().player : null;

    void Update()
    {
        var em = EnemyManager.Instance;
        var e = em ? em.nowEnemy : null;
        var p = P;
        if (e == null || p == null) return;

        float rate = Mathf.Max(0f, p.playerAutoAtt);
        if (rate <= 0f) return;

        float interval = 1f / rate;
        acc += Time.deltaTime;

        while (acc >= interval)
        {
            acc -= interval;
            e.TakeDamage(Mathf.Max(1, p.playerAtt));
            if (e.nowHP <= 0)
            {
                e.nowHP = 0;
                em.RefreshHPbar();
                return;
            }
        }
    }
}
