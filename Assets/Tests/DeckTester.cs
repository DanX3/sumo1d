using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DeckTester
{
    [Test]
    public void Draw()
    {
        var deck = new Deck<int>(new int[] { 1, 2, 3, 4, 5 });
        deck.Draw(4);
        Assert.IsTrue(deck.hand.Count == 4);
        Assert.IsTrue(deck.GetDeckCount() == 1);
        Assert.True(
            deck.hand[0] == 5
            && deck.hand[1] == 4
            && deck.hand[2] == 3
            && deck.hand[3] == 2
        );
    }

    [Test]
    public void Discard()
    {
        var deck = new Deck<int>(new int[] { 1, 2, 3, 4, 5 });
        deck.Draw(4);
        Assert.True(deck.Discard(0) == 5);
        Assert.True(deck.Discard(0) == 4);
        Assert.True(deck.Discard(0) == 3);
        Assert.True(deck.Discard(0) == 2);
        Assert.Zero(deck.hand.Count);
        Assert.True(deck.GetDiscardCount() == 4);
    }

    [Test]
    public void Shuffle()
    {
        var deck = new Deck<char>(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' });
        deck.Shuffle();
        deck.Draw(4);
        Assert.IsFalse(deck.hand[0] == 'a'
            && deck.hand[1] == 'b'
            && deck.hand[2] == 'c'
            && deck.hand[3] == 'd', "" + deck.hand[0] + deck.hand[1] + deck.hand[2] + deck.hand[3]);
    }

    [Test]
    public void MoveDiscardPile()
    {
        var deck = new Deck<int>(new int[] { 1, 2, 3, 4, 5 });
        deck.Draw(4);
        deck.DiscardHand();
        deck.Draw();
        deck.ReshuffleDiscards();

        Assert.True(deck.hand[0] == 1);
        Assert.True(deck.GetDiscardCount() == 0);
        Assert.True(deck.GetDeckCount() == 4);

        deck.Draw(4);
        Assert.True(deck.hand[0] != deck.hand[1]);
        Assert.True(deck.hand[0] != deck.hand[2]);
        Assert.True(deck.hand[0] != deck.hand[3]);
        Assert.True(deck.hand[0] != deck.hand[4]);
    }

    [Test]
    public void DrawWithNoCards()
    {
        var deck = new Deck<int>(new int[] { 1, 2, 3, 4, 5 });
        deck.Draw(4);
        deck.DiscardHand();
        deck.Draw();
        deck.Draw();

        Assert.True(deck.hand.Count == 2);
        Assert.True(deck.GetDiscardCount() == 0);
        Assert.True(deck.GetDeckCount() == 3);
    }

    class SampleClass
    {
        public int integer = 0;
        public bool boolean = true;
    }

    [Test]
    public void DrawNullCard()
    {
        int cardsDrawn = 5;
        var deck = new Deck<SampleClass>(new SampleClass[] {
            new SampleClass(),
            new SampleClass(),
            new SampleClass(),
            new SampleClass(),
            new SampleClass(),
        });
        deck.OnCardDrawn += (card) => cardsDrawn--;
        deck.Draw(10);
        Assert.True(cardsDrawn == 0);
    }
}
