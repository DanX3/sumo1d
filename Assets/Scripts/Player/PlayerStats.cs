using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats
{

    public Stats baseStats;
    public Stats bonus;
    public int arenaBonus;

    // List<Powerup> powerups;

    // public int powerupCount { get => powerups.Count; }

    public PlayerStats(int power, int spirit, int weight, int reflex, int critical)
    {
        baseStats = new Stats(power, spirit, weight, reflex, critical);
        bonus = new Stats(0, 0, 0, 0, 0);
    }

    // public void AddPowerup(Powerup powerup)
    // {
    //     foreach (var modifier in powerup.modifiers)
    //         ChangeBonus(modifier.value, modifier.diff);
    // }

    // public void RemovePowerup(Powerup powerup)
    // {
    //     foreach (var modifier in powerup.modifiers)
    //         ChangeBonus(modifier.value, -modifier.diff);
    // }

    private void ChangeBonus(Attribute attribute, int diff)
    {
        switch (attribute)
        {
            case Attribute.Power: bonus.power += diff; break;
            case Attribute.Spirit: bonus.spirit += diff; break;
            case Attribute.Weight: bonus.weight += diff; break;
            case Attribute.Reflex: bonus.reflex += diff; break;
            case Attribute.Critical: bonus.critical += diff; break;
        }
    }

    public int power { get => baseStats.power + bonus.power; }
    public int spirit { get => baseStats.spirit + bonus.spirit; }
    public int weight { get => baseStats.weight + bonus.weight; }
    public int reflex { get => baseStats.reflex + bonus.reflex; }
    public int critical { get => baseStats.critical + bonus.critical; }

    public float powMul { get => 0.375f + 0.125f * (baseStats.power + bonus.power); }
    public float weiMul { get => 1.625f - 0.125f * (baseStats.weight + bonus.weight); }
    public float critChance { get => 0.01f * (5f + 5f * critical); }

    // public bool IsCriticalHit() => Random.Range(0f, 1f) < critChance;

}