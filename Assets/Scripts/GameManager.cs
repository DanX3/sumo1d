using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Player opponent;
    public Button endTurnButton;
    public VerticalLayoutGroup playerPowerupList;
    public VerticalLayoutGroup opponentPowerupList;
    bool isPlayerTurn;
    [HideInInspector] public int turnCounter = 0;

    void Start() => Init();

    public void Init()
    {
        player.Init();
        opponent.Init();

        // callback setup
        player.OnTurnStart += OnPlayerStartTurn;
        player.OnTurnEnd += OnPlayerEndTurn;
        opponent.OnTurnStart += OnOpponentStartTurn;
        opponent.OnTurnEnd += OnOpponentEndTurn;

        turnCounter = 0;
        player.OnTurnStart?.Invoke();
    }

    void OnPlayerStartTurn()
    {
        Debug.Log("Player start turn");
        endTurnButton.interactable = true;
        player.deck.Draw(4);
    }

    void OnPlayerEndTurn()
    {
        Debug.Log("Player end turn");
        player.deck.DiscardHand();
        endTurnButton.interactable = false;
        opponent.OnTurnStart?.Invoke();
    }

    void OnOpponentStartTurn()
    {
        Debug.Log("Opponent start turn");
        opponent.OnTurnEnd?.Invoke();
    }

    void OnOpponentEndTurn()
    {
        Debug.Log("Opponent end turn");
        turnCounter++;
        player.OnTurnStart?.Invoke();
    }

    void ShrinkArena()
    {

    }

    public void OnEndTurnPressed()
    {
        player.OnTurnEnd?.Invoke();
    }



}
