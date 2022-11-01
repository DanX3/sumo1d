using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public Publisher publisher;
    public PlayerStats stats;

    public DeckManager deckManager;
    public PlayedCardsHistory playedCardsHistory = new PlayedCardsHistory();
    public UIStats uiStats;
    public PowerupList powerupList;
    public bool isOpponent;

    public delegate void VoidEvent();
    public delegate void DamageEvent(int damage, bool isCritical);
    public DamageEvent OnDamage;
    public delegate void CardEvent(Card card);
    public CardEvent OnCardPlayed;
    public VoidEvent OnTurnStart;
    public VoidEvent OnTurnEnd;
    public VoidEvent OnDefeat;

    public static int StartHP = 50;

    int hp, maxHp;

    public void Init()
    {
        stats = new PlayerStats(3, 3, 3, 3, 3);
        uiStats.Init(stats);
        stats.RefreshUI();
        deckManager.Init();
        OnCardPlayed += powerupList.OnCardPlayed;
        OnTurnStart += powerupList.TurnPassed;
        maxHp = hp = StartHP;
    }

    public Player GetOpponent()
    {
        return GameManager.Instance.player.id == id
                ? GameManager.Instance.opponent
                : GameManager.Instance.player;
    }

    public void PlayCard(Card card)
    {
        card.Play(this);
        deckManager.Discard(card);
        OnCardPlayed?.Invoke(card);
    }

    public void DoDamage(Card card, int totalDamage, bool isCritical)
    {
        OnDamage?.Invoke(totalDamage, isCritical);
        GetOpponent().GetDamage(totalDamage);
        hp += totalDamage;
        // playedCardsHistory.Add(this, card, totalDamage, isCritical);
    }

    public bool IsCriticalHit()
    {
        return Random.Range(0f, 1f) < stats.critChance;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"GetDamage({damage})");
        if (hp <= 0)
            OnDefeat?.Invoke();
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
