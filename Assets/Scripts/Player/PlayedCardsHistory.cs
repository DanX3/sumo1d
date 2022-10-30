using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayedCardsHistory
{
    public List<PlayedCardInTurn> cardsHistory = new List<PlayedCardInTurn>();

    public void Add(Player player, Card card)
    {
        int turn = GameManager.Instance.turnCounter;

        if (cardsHistory[turn] == null)
            cardsHistory[turn] = new PlayedCardInTurn();

        PlayedCard newPlayedCard = new PlayedCard(player, card);

        cardsHistory[turn].playedCards.Add(newPlayedCard);
    }

    public void Add(Player player, Card card, int damagedDealt, bool didCritical)
    {
        int turn = GameManager.Instance.turnCounter;
        
        if (cardsHistory[turn] == null)
            cardsHistory[turn] = new PlayedCardInTurn();

        PlayedCard newPlayedCard = new PlayedCard(player, card, damagedDealt, didCritical);

        cardsHistory[turn].playedCards.Add(newPlayedCard);
    }

    public List<PlayedCard> PlayerCardHistoryInTurn(Player player, int turn)
    {
        if (cardsHistory[turn] == null)
            return new List<PlayedCard>();

        return cardsHistory[turn].PlayerPlayedCards(player);
    }
}

public record PlayedCardInTurn
{
    public List<PlayedCard> playedCards = new List<PlayedCard>();

    public List<PlayedCard> PlayerPlayedCards(Player player) => playedCards.FindAll(c => c.player == player);


    public int GetTotalDamage() => playedCards.Sum(p => p.damageDealt);
    public int GetTotalManaUsed() => playedCards.Sum(p => p.card.manaCost);
}

public record PlayedCard
{
    public Player player;
    public Card card;
    public int damageDealt;
    public bool? didCritical;

    public PlayedCard()
    {
    }

    public PlayedCard(Player player, Card card, int damageDealt = 0, bool? didCritical = null)
    {
        this.player = player;
        this.card = card;
        this.damageDealt = damageDealt;
        this.didCritical = didCritical;
    }
}
