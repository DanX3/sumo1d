using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SavedStats
{
    public int power;
    public int spirit;
    public int weight;
    public int reflexes;
    public int critical;

    public SavedStats(int power, int spirit, int weight, int reflexes, int critical)
    {
        this.power = power;
        this.spirit = spirit;
        this.weight = weight;
        this.reflexes = reflexes;
        this.critical = critical;
    }
}

[Serializable]
public class StartingCards
{
    public string[] cardsName;

    public StartingCards(List<string> cards)
    {
        cardsName = cards.ToArray();
    }
}

public class Player : MonoBehaviour
{
    public int id;
    public Publisher publisher;
    public PlayerStats stats;
    public PlayerAttributes startingAttributes;
    public List<string> startingCards = new List<string>()
    {
        "Ki Blast",
        "Ki Blast",
        "Push",
        "Glass Cannon",
        "Counter attack",
        "ALl In",
        "Safe Terrain",
        "Gravity",
        "Mind Barrier",
    };

    public DeckManager deckManager;
    public PlayedCardsHistory playedCardsHistory = new PlayedCardsHistory();
    public UIStats uiStats;
    public UIArena uiArena;
    public PowerupList powerupList;
    public TMP_Text damageLabel;

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

    public void Init(PlayerAttributes attributes)
    {
        stats = new PlayerStats(attributes != null ? attributes : startingAttributes);
        uiStats.Init(stats);
        stats.RefreshUI();
        deckManager?.Init();
        OnCardPlayed += powerupList.OnCardPlayed;
        OnTurnStart += powerupList.TurnPassed;
        stats.OnPowerupBonus += RefreshHP;
        maxHp = hp = StartHP;
        uiArena.Init(this);
    }

    void RefreshHP(PowerupBonus stat, float delta)
    {
        if (stat != PowerupBonus.Arena)
            return;

        hp += Mathf.RoundToInt(delta);
        Debug.Log(playerString + " hp: " + hp);
    }

    public bool isPlayer { get => GameManager.Instance.player.id == id; }
    public string playerString { get => isPlayer ? "Player" : "Opponenet"; }

    public Player GetOpponent()
    {
        return isPlayer ? GameManager.Instance.opponent : GameManager.Instance.player;
    }

    public void PlayCard(Card card)
    {
        if (!HasEnoughMana(card.currentManaCost))
        {
            Debug.LogWarning("Not enough mana to play the card");
            return;
        }

        GameManager.Instance.cardPlayedDetail.ShowCardDetail(this, card);

        UseMana(card.currentManaCost);

        deckManager?.Discard(card);
        card.Play(this);
        OnCardPlayed?.Invoke(card);
    }

    public void DoDamage(Card card, int damage, bool isCritical)
    {
        OnDamageDealt?.Invoke(damage, isCritical);

        Debug.Log(playerString + " deals " + damage + (isCritical ? "!" : ""));

        GetOpponent().GetDamage(damage, isCritical);
        hp += damage;
        Debug.Log(playerString + " hp: " + hp);
    }

    public void GetDamage(int damage, bool isCritical)
    {
        hp -= damage;

        SoundManager.Instance.PlayRandomDamageReceivedSound();

        StartCoroutine(ShowDamageReceived(damage, isCritical));

        if (hp <= 0)
            OnDefeat?.Invoke();
        OnDamageReceived?.Invoke(damage, isCritical);
        Debug.Log(playerString + " hp: " + hp);
    }

    IEnumerator ShowDamageReceived(int damage, bool isCritical)
    {
        damageLabel.gameObject.SetActive(true);

        if (isCritical)
            damageLabel.color = Color.yellow;
        else
            damageLabel.color = Color.red;

        damageLabel.text = damage.ToString();

        yield return new WaitForSeconds(2);

        damageLabel.gameObject.SetActive(false);
    }

    public void StartTurn()
    {
        OnTurnStart?.Invoke();
    }

    public void EndTurn()
    {
        OnTurnEnd?.Invoke();
    }

    public void UseMana(int count)
    {
        if (isPlayer)
            GameManager.Instance.manaSlots.UseMana(count);
        else
            GameManager.Instance.manaSlots.FreeMana(count);
    }

    private bool HasEnoughMana(int manaRequired)
    {
        var ms = GameManager.Instance.manaSlots;
        Debug.Log(ms.manaLeft);
        return manaRequired <= (isPlayer ? ms.manaLeft : ms.manaUsed);
    }

    public PlayerAttributes GetSavedStats() => new PlayerAttributes(
        stats.baseStats.power,
        stats.baseStats.spirit,
        stats.baseStats.weight,
        stats.baseStats.reflexes,
        stats.baseStats.critical
    );

    public void SetBaseStats(PlayerAttributes savedStats)
    {
        stats = new PlayerStats(savedStats);
    }

}