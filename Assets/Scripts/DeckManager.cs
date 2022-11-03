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
    private List<Card> _discardPile = new List<Card>();

    public Action<Card> OnDrawCard;
    public Action<Card> OnDiscardCard;

    public int handCount { get => _hand.Count; }

    public void Init()
    {
        ClearAll();

        foreach (Card cardPrefab in deckList)
        {
            var card = Instantiate(cardPrefab, transform).GetComponent<Card>();
            _deck.Push(card);
        }
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
        for(int i = 0; i < count; i++)
        {
            var card = _deck.Pop();
            _hand.Add(card);
            OnDrawCard?.Invoke(card);
        }
    }

    public void Discard(Card card)
    {
        _hand.Remove(card);
        _discardPile.Add(card);
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
        _deck.OrderBy(k => random.Next());
    }
}
