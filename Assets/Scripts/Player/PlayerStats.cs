using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats
{

    public Stats baseStats;
    public Stats bonus;
    public List<CardPowerup> powerups = new List<CardPowerup>();
    public int hp;

    public int maxHp = 50;

    public delegate void RefreshStatEvent(Stat stat, int value);
    public RefreshStatEvent onRefreshStat;
    public delegate void PowerupBonusEvent(PowerupBonus bonus, float delta);
    public PowerupBonusEvent OnPowerupBonus;
    

    public PlayerStats(int power, int spirit, int weight, int reflex, int critical)
    {
        baseStats = new Stats(power, spirit, weight, reflex, critical);
        bonus = new Stats(0, 0, 0, 0, 0, 0, 1f, 0f, 1f, 0, 0);
        hp = maxHp; 
        RefreshUI();
    }

    public void AlterAttribute(PowerupBonus attribute, float delta)
    {
        var intDelta = Mathf.RoundToInt(delta);
        switch (attribute)
        {
            case PowerupBonus.Power: bonus.power += intDelta; break;
            case PowerupBonus.Spirit: bonus.spirit += intDelta; break;
            case PowerupBonus.Weight: bonus.weight += intDelta; break;
            case PowerupBonus.Reflex: bonus.reflex += intDelta; break;
            case PowerupBonus.Critical: bonus.critical += intDelta; break;
            case PowerupBonus.DamageAdd: bonus.damageAdd += intDelta; break;
            case PowerupBonus.DamageMul: bonus.damageMul += delta; break;
            case PowerupBonus.CritAdd: bonus.critAdd += delta; break;
            case PowerupBonus.CritMul: bonus.critMul += delta; break;
            case PowerupBonus.HandCount: bonus.handCount += intDelta; break;
            case PowerupBonus.Arena: bonus.arena += intDelta; break;
        }

        OnPowerupBonus?.Invoke(attribute, delta);

        RefreshUI();
    }

    public void RefreshUI()
    {
        onRefreshStat?.Invoke(Stat.Power, baseStats.power + bonus.power);
        onRefreshStat?.Invoke(Stat.Spirit, baseStats.spirit + bonus.spirit);
        onRefreshStat?.Invoke(Stat.Weight, baseStats.weight + bonus.weight);
        onRefreshStat?.Invoke(Stat.Reflexes, baseStats.reflex + bonus.reflex);
        onRefreshStat?.Invoke(Stat.Critical, baseStats.critical + bonus.critical);
    }

    public int power { get => baseStats.power + bonus.power; }
    public int spirit { get => baseStats.spirit + bonus.spirit; }
    public int weight { get => baseStats.weight + bonus.weight; }
    public int reflex { get => baseStats.reflex + bonus.reflex; }
    public int critical { get => baseStats.critical + bonus.critical; }
    public int handCount { get => baseStats.handCount + bonus.handCount; }
    public float damageThreshold { get => baseStats.damageThreshold + bonus.damageThreshold; }

    public float powMul { get => 0.375f + 0.125f * (baseStats.power + bonus.power); }
    public float weiMul { get => 1.625f - 0.125f * (baseStats.weight + bonus.weight); }
    public float critChance { get => 0.01f * (5f + 5f * critical); }
    
}