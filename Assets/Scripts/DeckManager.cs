using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DeckManager : MonoBehaviour
{
    public List<Card> deckList = new List<Card>();

    private Stack<Card> _deck = new Stack<Card>();
    private List<Card> _hand = new List<Card>();
    private Stack<Card> _discardPile = new Stack<Card>();

    public Action<Card> OnDrawCard;
    public Action<Card> OnDiscardCard;

    public int handCount { get => _hand.Count; }

    public void Init()
    {
        Debug.Log("Init Deck");

        ClearAll();

        foreach (Card card in deckList)
        {
            var newCard = Instantiate(card, transform).GetComponent<Card>();
            _deck.Push(newCard);
        }

        ShuffleDeck();
    }

    public void ClearAll()
    {
        while (_deck.Count > 0)
            Destroy(_deck.Pop());

        foreach (Card card in _hand)
            Destroy(card);

        _hand.Clear();

        foreach (Card card in _discardPile)
            Destroy(card);

        _discardPile.Clear();
    }

    public void Draw(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (_deck.Count == 0)
                ReshuffleDiscards();

            // player has all the cards in its hand
            // interrupt drawing
            if (_deck.Count == 0)
                break;

            var card = _deck.Pop();
            _hand.Add(card);

            OnDrawCard?.Invoke(card);
        }
    }

    public void Discard(Card card)
    {
        _hand.Remove(card);
        _discardPile.Push(card);

        OnDiscardCard?.Invoke(card);
    }

    public void DiscardHand()
    {
        for (int i = _hand.Count - 1; i >= 0; i--)
            Discard(_hand[i]);
    }

    public void ShuffleDeck()
    {
        var random = new System.Random();
        _deck = new Stack<Card>(_deck.OrderBy(k => random.Next()));
    }

    public void ReshuffleDiscards()
    {
        while (_discardPile.Count > 0)
            _deck.Push(_discardPile.Pop());

        ShuffleDeck();
    }
}

[System.Serializable]
public class DeckRow
{
    public int count = 1;
    public Card card;
}