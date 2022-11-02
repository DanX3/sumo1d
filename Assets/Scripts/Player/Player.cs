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
    public DamageEvent OnDamageDealt;
    public DamageEvent OnDamageReceived;
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
        OnDamageDealt?.Invoke(totalDamage, isCritical);
        Debug.Log(isOpponent ? "Opponent" : "Player" + " deals " + totalDamage + (isCritical ? "!" : ""));
        GetOpponent().GetDamage(totalDamage, isCritical);
        hp += totalDamage;
        // playedCardsHistory.Add(this, card, totalDamage, isCritical);
    }

    public bool IsCriticalHit(float critMul, int critAdd)
    {
        return Random.Range(0f, 1f) < (critMul * stats.critChance + critAdd);
    }

    public static bool IsCriticalHit(float chance)
    {
        return Random.Range(0f, 1f) < chance;
    }

    public void GetDamage(int damage, bool isCritical)
    {
        OnDamageReceived?.Invoke(damage, isCritical);
        hp -= damage;
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
