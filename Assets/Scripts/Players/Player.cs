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
        if (!HasEnoughMana(card.manaCost))
        {
            Debug.LogWarning("Not enough mana to play the card");
            return;
        }

        UseMana(card.manaCost);

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
        OnDamageReceived?.Invoke(damage, isCritical);
        hp -= damage;
        Debug.Log(playerString + " hp: " + hp);
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
}