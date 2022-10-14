using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Deck<T>
{
    List<T> availableCards = new List<T>();
    Stack<T> deck = new Stack<T>();
    Stack<T> discards = new Stack<T>();
    public List<T> hand = new List<T>();

    public delegate void DrawnCard(T card);
    public DrawnCard OnCardDrawn;

    public Deck(IEnumerable<T> availableCards)
    {
        foreach (var card in availableCards)
            AddAvailableCard(card);
    }

    public void AddAvailableCard(T availableCard)
    {
        availableCards.Add(availableCard);
        deck.Push(availableCard);
    }

    public void Draw()
    {
        if (deck.Count == 0)
            ReshuffleDiscards();
        
        // if there is no card even after moving the discard pile
        // there is no card left to draw
        if (deck.Count == 0)
            return;

        var drawnCard = deck.Pop();
        hand.Add(drawnCard);
        OnCardDrawn?.Invoke(drawnCard);
    }

    public void Draw(int count)
    {
        for (int i = 0; i < count; i++)
            Draw();
    }

    public T Discard(int handIndex)
    {
        if (handIndex > hand.Count)
        {
            Debug.LogWarning($"Discarding index greater than hand count ({handIndex} > {hand.Count})");
            return default(T);
        }

        var discarded = hand[handIndex];
        discards.Push(hand[handIndex]);
        hand.RemoveAt(handIndex);
        return discarded;
    }

    public void DiscardHand()
    {
        while (hand.Count > 0)
            Discard(0);
    }

    public void Shuffle()
    {
        // shyffle the cards
        var random = new System.Random();
        deck.OrderBy(k => random.Next());
    }

    public void ReshuffleDiscards()
    {
        while (discards.Count > 0)
            deck.Push(discards.Pop());

        Shuffle();
    }

    public int GetDeckCount() => deck.Count();
    public int GetDiscardCount() => discards.Count();

}