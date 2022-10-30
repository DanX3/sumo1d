using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Player opponent;
    bool isPlayerTurn;
    int turnCounter = 0;

    public void Init()
    {
        // callback setup
        player.OnTurnStart += OnPlayerStartTurn;
        player.OnTurnEnd += OnPlayerEndTurn;
        opponent.OnTurnStart += OnOpponentStartTurn;
        opponent.OnTurnEnd += OnOpponentEndTurn;

        turnCounter = 0;
    }

    void OnPlayerStartTurn()
    {

    }

    void OnPlayerEndTurn()
    {

    }

    void OnOpponentStartTurn()
    {

    }

    void OnOpponentEndTurn()
    {
        turnCounter++;
    }

    void ShrinkArena()
    {

    }



}
