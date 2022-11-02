using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayedCardsHistory
{
    public List<PlayedCardsInTurn> history = new List<PlayedCardsInTurn>();

    public void Add(Card card, int damageDealt = 0, bool? didCritical = null)
    {
        int turn = GameManager.Instance.turnCounter;

        if (turn <= history.Count)
            history.Add(new PlayedCardsInTurn());

        PlayedCard newPlayedCard = new PlayedCard(card, damageDealt, didCritical);

        history[turn].playedCards.Add(newPlayedCard);
    }

    public List<PlayedCard> GetTurnHistory(int turn)
    {
        if (turn <= history.Count)
            return new List<PlayedCard>();

        return history[turn].playedCards;
    }
}

public record PlayedCardsInTurn
{
    public List<PlayedCard> playedCards = new List<PlayedCard>();

    public int TotalDamage() => playedCards.Sum(p => p.damageDealt ?? 0);
    public int CritsCount() => playedCards.Count(p => p.didCritical ?? false);
    public int TotalManaUsed() => playedCards.Sum(p => p.card.manaCost);

    public PlayedCardsInTurn()
    {
        playedCards = new List<PlayedCard>();
    }
}

public record PlayedCard
{
    // TODO
    // bisogna stare attenti perchè se eliminiamo il gameobject dal gioco 
    // (tipo quando un powerup viene rimosso), questa reference punterà a null
    public Card card;
    public int? damageDealt;
    public bool? didCritical;

    public PlayedCard(Card card, int? damageDealt = null, bool? didCritical = null)
    {
        this.card = card;
        this.damageDealt = damageDealt;
        this.didCritical = didCritical;
    }
}
