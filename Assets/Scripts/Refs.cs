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
}
