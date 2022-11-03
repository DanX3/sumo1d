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
    public UIArena uiArena;
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
        uiArena.Init(this);
    }

    public Player GetOpponent()
    {
        return GameManager.Instance.player.id == id
                ? GameManager.Instance.opponent
                : GameManager.Instance.player;
    }

    public void PlayCard(Card card)
    {
        deckManager.Discard(card);
        card.Play(this);
        OnCardPlayed?.Invoke(card);
    }

    public void DoDamage(Card card, int damage, bool isCritical)
    {
        OnDamageDealt?.Invoke(damage, isCritical);

        Debug.Log(isOpponent ? "Opponent" : "Player" + " deals " + damage + (isCritical ? "!" : ""));
        
        GetOpponent().GetDamage(damage, isCritical);
        hp += damage;
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
