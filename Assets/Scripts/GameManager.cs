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
    public ManaSlots manaSlots;
    public UIContactPoint contactPoint;    

    void Start() => Init();

    public void Init()
    {
        player.Init();
        opponent.Init();

        // callback setup
        player.OnTurnStart += OnPlayerStartTurn;
        player.OnTurnEnd += OnPlayerEndTurn;
        player.OnDefeat += OnPlayerLose;
        player.OnDamage += (damage, criticalHit) => contactPoint.Move(damage);
        opponent.OnTurnStart += OnOpponentStartTurn;
        opponent.OnTurnEnd += OnOpponentEndTurn;
        opponent.OnDefeat += OnPlayerWin;
        opponent.OnDamage += (damage, criticalHit) => contactPoint.Move(-damage);

        turnCounter = 0;
        player.OnTurnStart?.Invoke();
    }

    void OnPlayerStartTurn()
    {
        Debug.Log("Player start turn");
        endTurnButton.interactable = true;
        player.deck.Draw(6);
        manaSlots.Reset();
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


    void OnPlayerWin()
    {
        Debug.Log("YOU WIN");
    }

    void OnPlayerLose()
    {
        Debug.Log("GAME OVER");
    }

    public void PlayPlayerCard(int index)
    {
        if (player.deck.hand.Count <= index)
        {
            Debug.LogWarning($"Tried to play card with index {index} but hands has only {player.deck.hand.Count} cards");
            return;
        }

        var card = player.deck.hand[index];
        if (manaSlots.manaLeft < card.manaCost)
        {
            Debug.LogWarning("Not enough mana to play the card");
            return;
        }

        player.PlayCard(card);
        manaSlots.UseMana(card.manaCost);
        player.deck.Discard(index);
    }
}
