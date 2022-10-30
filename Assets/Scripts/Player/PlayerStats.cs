using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats
{

    public Stats baseStats;
    public Stats bonus;
    public int arenaBonus;
    public List<CardPowerup> powerups = new List<CardPowerup>();
    public int hp;

    public int maxHp = 50;
    Player player;
    

    public PlayerStats(Player player, int power, int spirit, int weight, int reflex, int critical)
    {
        this.player = player;
        baseStats = new Stats(power, spirit, weight, reflex, critical);
        bonus = new Stats(0, 0, 0, 0, 0);
        hp = maxHp; 
    }

    public void AlterAttribute(PlayerAttribute attribute, int delta)
    {
        switch (attribute)
        {
            case PlayerAttribute.Power: bonus.power += delta; break;
            case PlayerAttribute.Spirit: bonus.spirit += delta; break;
            case PlayerAttribute.Weight: bonus.weight += delta; break;
            case PlayerAttribute.Reflex: bonus.reflex += delta; break;
            case PlayerAttribute.Critical: bonus.critical += delta; break;
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        player.uiStats.SetStat(Stat.Power, baseStats.power + bonus.power);
        player.uiStats.SetStat(Stat.Spirit, baseStats.spirit + bonus.spirit);
        player.uiStats.SetStat(Stat.Weight, baseStats.weight + bonus.weight);
        player.uiStats.SetStat(Stat.Reflexes, baseStats.reflex + bonus.reflex);
        player.uiStats.SetStat(Stat.Critical, baseStats.critical + bonus.critical);
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