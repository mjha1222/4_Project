using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public UpgradeStats stats;
    public float tickInterval = 1f;
    float timer;

    void Update()
    {
        if (stats == null || stats.Current == null) return;

        timer += Time.deltaTime;
        if (timer >= tickInterval)
        {
            timer = 0f;

            double dmg = stats.Current.autoDps;
            Debug.Log($"[AutoAttack] {dmg} damage applied");
        }
    }
}
