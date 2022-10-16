using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refs : Singleton<Refs>
{
    public Publisher pub;
    public Player _player, _opponent;
    public bool playerTurn { get; private set; }

    // returns the player of the current turn
    public Player player { get => playerTurn ? _player : _opponent; }
    
    // returns the opponent of the current turn
    public Player opponent { get => playerTurn ? _opponent : _player; }

    // the contact position of the two sumo fighters
    public int position;



    public void FinishTurn()
    {
        playerTurn = !playerTurn;
    }

    void Awake()
    {
        Debug.Log("Singleton Start");
    }   

    public void Attack(Player player, Player opponent, Attack attack)
    {
        attack.Play(player,  opponent);
        int damage = ComputeDamage(player, opponent, attack);

        bool criticalHit = Random.Range(0f, 1f) < (player.stats.critChance * attack.stats.critMultiplier) + attack.stats.critBonus;
    }

    public static int ComputeDamage(Player player, Player opponent, Attack attack)
    {
        // damage formula
        int cardPower = attack.damage + attack.stats.powBonus;
        int damage = Mathf.FloorToInt(cardPower * player.stats.powMul * opponent.stats.weiMul) 
            + player.stats.spirit - opponent.stats.reflex;
        damage = Mathf.Max(0, damage);

        return damage;
    }
}
