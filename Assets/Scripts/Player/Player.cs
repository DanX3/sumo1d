using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public Publisher publisher;
    public PlayerStats stats;

    public Deck<Card> deck;
    public PlayedCardsHistory playedCardsHistory;
    public List<Card> deckCards = new List<Card>();
    public UIStats uiStats;

    public delegate void VoidEvent();
    public VoidEvent OnTurnStart;
    public VoidEvent OnTurnEnd;

    void Start()
    {
        stats = new PlayerStats(this, 3, 3, 3, 3, 3);
        deck = new Deck<Card>(deckCards);
    }

    public Player GetOpponent()
    {
        return GameManager.Instance.player.id == id
                ? GameManager.Instance.opponent
                : GameManager.Instance.player;
    }

    public void DrawCard(int count)
    {

    }

    public void PlayCard(Card card)
    {
        if (card.GetCardType() != CardType.Attack)
            playedCardsHistory.Add(this, card);

        card.Play(this);
    }

    public void DoDamage(Card card, int totalDamage, bool isCritical)
    {
        GetOpponent().GetDamage(totalDamage);

        playedCardsHistory.Add(this, card, totalDamage, isCritical);
    }

    public bool IsCriticalHit()
    {
        return Random.Range(0f, 1f) < stats.critChance;
    }

    public void GetDamage(int damage)
    {
        Debug.Log($"GetDamage({damage})");
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
