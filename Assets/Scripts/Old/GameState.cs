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

    public Stats(int power, int spirit, int weight, int reflex, int critical)
    {
        this.power = power;
        this.spirit = spirit;
        this.weight = weight;
        this.reflex = reflex;
        this.critical = critical;
    }
}



public class GameState : MonoBehaviour
{
    public PlayerStats player, opponent;
}
