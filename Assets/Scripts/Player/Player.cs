using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public Publisher publisher;
    public PlayerStats stats;
    public Deck<int> deck;
    public delegate void VoidEvent();
    public VoidEvent OnTurnStart;
    public VoidEvent OnTurnEnd;
    

    private int playedCardsInTurnCount; // TODO diventera' il numero di elementi dentro la history delle carte giocate nel turno

    void Start()
    {
        stats = new PlayerStats(3, 3, 3, 3, 3);
    }

    public Player GetOpponent()
    {
        return GameManager.Instance.player.id == id
                ? GameManager.Instance.opponent
                : GameManager.Instance.player;
    }
    
    public int GetPlayedCardsInTurnCount()
    {
        return playedCardsInTurnCount;
    }

    public void DrawCard(int count)
    {

    }

    public void PlayCard(int index)
    {

    }

    public void StartTurn()
    {
        OnTurnStart?.Invoke();
    }

    public void EndTurn()
    {
        OnTurnEnd?.Invoke();
    }
}
