using UnityEngine;

[System.Serializable]
public class UpgradeLevelData
{
    public int level;
    public float critMultiplier;
    public double autoDps;
    public float goldMultiplier;
    [HideInInspector] public double cost;
}
