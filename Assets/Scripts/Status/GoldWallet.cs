using System;
using UnityEngine;

public class GoldWallet : MonoBehaviour
{
    [SerializeField] double gold = 0;
    public event Action<double> onGoldChanged;

    public double Current => gold;

    public void SetGold(double value)
    {
        double v = Math.Max(0, value);
        if (Math.Abs(v - gold) < 1e-9) return;
        gold = v;
        onGoldChanged?.Invoke(gold);
    }

    public void AddGold(double amount)
    {
        if (amount <= 0) return;
        gold += amount;
        onGoldChanged?.Invoke(gold);
    }

    public bool TrySpend(double amount)
    {
        if (amount < 0) return false;
        if (gold + 1e-9 < amount) return false;
        gold -= amount;
        onGoldChanged?.Invoke(gold);
        return true;
    }

    public bool CanAfford(double amount) => gold + 1e-9 >= amount;
}
