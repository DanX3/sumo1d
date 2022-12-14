using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DeckManager : MonoBehaviour
{
    public List<Card> deckList = new List<Card>();
    [SerializeField] TMPro.TMP_Text cardsCounter;


    private Stack<Card> _deck = new Stack<Card>();
    private List<Card> _hand = new List<Card>();
    private Stack<Card> _discardPile = new Stack<Card>();

    private const float drawDelay = 0.3f;
    private const float reshuffleDelay = 0.2f;


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

    IEnumerator DrawCoroutine(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (_deck.Count == 0)
                yield return ReshuffleDiscards();

            // player has all the cards in its hand
            // interrupt drawing
            if (_deck.Count == 0)
                yield return null;

            var card = _deck.Pop();
            _hand.Add(card);

            OnDrawCard?.Invoke(card);

            RefreshUI();
            yield return new WaitForSeconds(drawDelay);
        }
    }

    public void Draw(int count = 1)
    {
        StartCoroutine(DrawCoroutine(count));
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
        RefreshUI();
    }

    public IEnumerator ReshuffleDiscards()
    {
        while (_discardPile.Count > 0)
        {
            var card = _discardPile.Pop();
            card.OnReshuffled();
            _deck.Push(card);

            yield return new WaitForSeconds(reshuffleDelay);
            RefreshUI();
        }

        ShuffleDeck();
    }

    public Card GetRandomCardInHand()
    {
        if (_hand.Count == 0)
            return null;

        return _hand[new System.Random().Next() % _hand.Count];
    }

    private void RefreshUI()
    {
        cardsCounter.text = _deck.Count().ToString();
    }
}