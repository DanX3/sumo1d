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
    public PlayedCardsHistory playedCardsHistory = new PlayedCardsHistory();
    public List<Card> deckCards = new List<Card>();
    public UIStats uiStats;
    public PowerupList powerupList;
    public bool isOpponent;

    public delegate void VoidEvent();
    public delegate void CardEvent(Card card);
    public CardEvent OnCardPlayed;
    public VoidEvent OnTurnStart;
    public VoidEvent OnTurnEnd;

    public void Init()
    {
        stats = new PlayerStats(3, 3, 3, 3, 3);
        uiStats.Init(stats);
        stats.RefreshUI();
        deck = new Deck<Card>(deckCards);
        deck.OnCardDrawn += DrawCardFromDeck;
        deck.OnDiscardCard += (index) => FindObjectOfType<UIHand>().DiscardCard(index);
        OnCardPlayed += powerupList.OnCardPlayed;
        OnTurnStart += powerupList.TurnPassed;
    }

    public Player GetOpponent()
    {
        return GameManager.Instance.player.id == id
                ? GameManager.Instance.opponent
                : GameManager.Instance.player;
    }

    void DrawCardFromDeck(Card card)
    {
        FindObjectOfType<UIHand>().AddCard(card);
    }

    public void PlayCard(Card card)
    {
        // if (card.GetCardType() != CardType.Attack)
        //     playedCardsHistory.Add(this, card);

        card.Play(this);
        OnCardPlayed?.Invoke(card);
    }

    public void DoDamage(Card card, int totalDamage, bool isCritical)
    {
        GetOpponent().GetDamage(totalDamage);

        // playedCardsHistory.Add(this, card, totalDamage, isCritical);
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
