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
        Debug.Log("init");

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
        for (int i = 0; i < count; i++)
        {
            if (_deck.Count == 0)
                ReshuffleDiscards();

            var card = _deck.Pop();
            _hand.Add(card);

            Debug.Log($"draw card {card.cardName}");
            Debug.Log($"card remains: {_deck.Count}");
            OnDrawCard?.Invoke(card);
        }
    }

    public void Discard(Card card)
    {
        _hand.Remove(card);
        _discardPile.Push(card);
        Debug.Log($"discard card {card.cardName}");

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

    public void ReshuffleDiscards()
    {
        while (_discardPile.Count > 0)
            _deck.Push(_discardPile.Pop());

        ShuffleDeck();
    }
}
