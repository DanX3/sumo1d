using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayedCardsHistory
{
    public List<PlayedCardsInTurn> history = new List<PlayedCardsInTurn>();

    public void Add(Card card, int damagedDealt = 0, bool? didCritical = null)
    {
        int turn = GameManager.Instance.turnCounter;
        
        if (turn < history.Count)
            history.Add(new PlayedCardsInTurn());

        PlayedCard newPlayedCard = new PlayedCard(card, damagedDealt, didCritical);

        history[turn].playedCards.Add(newPlayedCard);
    }

    public List<PlayedCard> PlayerCardHistoryInTurn(int turn)
    {
        if (turn < history.Count)
            return new List<PlayedCard>();

        return history[turn].playedCards;
    }
}

public record PlayedCardsInTurn
{
    public List<PlayedCard> playedCards = new List<PlayedCard>();

    public int GetTotalDamage() => playedCards.Sum(p => p.damageDealt);
    public int GetTotalManaUsed() => playedCards.Sum(p => p.card.manaCost);
}

public record PlayedCard
{
     // TODO
     // bisogna stare attenti perchè se eliminiamo il gameobject dal gioco 
     // (tipo quando un powerup viene rimosso), questa reference punterà a null
    public Card card;
    public int damageDealt;
    public bool? didCritical;

    public PlayedCard()
    {
    }

    public PlayedCard(Card card, int damageDealt = 0, bool? didCritical = null)
    {
        this.card = card;
        this.damageDealt = damageDealt;
        this.didCritical = didCritical;
    }
}
