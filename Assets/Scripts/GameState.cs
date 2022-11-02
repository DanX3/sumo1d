using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public int power;
    public int spirit;
    public int weight;
    public int reflex;
    public int critical;
    public int damageAdd = 0;
    public float damageMul = 1f;
    public float critAdd = 0f;
    public float critMul = 1f;
    public int handCount = 6;
    public int arena = 50;
    public int damageThreshold = 0;

    public Stats(int power, int spirit, int weight, int reflex, int critical)
    {
        this.power = power;
        this.spirit = spirit;
        this.weight = weight;
        this.reflex = reflex;
        this.critical = critical;
    }

    public Stats(int power, int spirit, int weight, int reflex, int critical, int damageAdd, float damageMul, float critAdd, float critMul, int handCount, int arena) : this(power, spirit, weight, reflex, critical)
    {
        this.damageAdd = damageAdd;
        this.damageMul = damageMul;
        this.critAdd = critAdd;
        this.critMul = critMul;
        this.handCount = handCount;
        this.arena = arena;
    }
}



public class GameState : MonoBehaviour
{
    public PlayerStats player, opponent;
}
