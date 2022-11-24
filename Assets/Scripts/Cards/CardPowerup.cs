using UnityEngine;

public class CardPowerup : Card
{
    [Range(1, 10)] public int durationInTurns = 1;
    int turnsLeft;
    [SerializeField] TMPro.TMP_Text durationLabel;

    private Player target;
    public PowerupWeakness weakness;
    public int weaknessCount = 1;
    private int weaknessLeft;
    public Player.VoidEvent OnRemoved;


    new void Start()
    {
        base.Start();
        weaknessLeft = weaknessCount;
        durationLabel.text = durationInTurns + "";
    }

    public override CardType GetCardType()
    {
        return CardType.Powerup;
    }

    public override void Play(Player target)
    {
        turnsLeft = durationInTurns;
        weaknessLeft = weaknessCount;
        this.target = target;
        target.stats.powerups.Add(this);

        base.Play(target);

        target.OnTurnEnd += OnTurnPassed;

        // V   V   V    V   V   V    V   V   V    V   V   V    V   V   V    
        // V                                                           V
        // V                Implement here weaknesses                  V
        // V                                                           V
        // V   V   V    V   V   V    V   V   V    V   V   V    V   V   V    
        switch (weakness)
        {
            case PowerupWeakness.None:
                break;
            case PowerupWeakness.OnDamageReceived:
                target.OnDamageReceived += WeaknessTickDamage;
                break;
            case PowerupWeakness.OnDamageDealt:
                target.OnDamageDealt += WeaknessTickDamage;
                break;
            case PowerupWeakness.OnCardsPlayed:
                target.OnCardPlayed += WeaknessTickCard;
                break;
            case PowerupWeakness.OnAttacksPlayed:
                target.OnCardPlayed += WeaknessTickAttack;
                break;
            case PowerupWeakness.OnPowerupPlayed:
                target.OnCardPlayed += WeaknessTickPowerup;
                break;
            case PowerupWeakness.OnInstantPlayed:
                target.OnCardPlayed += WeaknessTickInstant;
                break;
        }

        user.playedCardsHistory.Add(this);
        SoundManager.Instance.PlayRandomPowerupsSound();
    }


    void WeaknessTickDamage(int damage, bool isCritical)
    {
        TickWeakness(damage);
    }

    void WeaknessTickCard(Card card)
    {
        TickWeakness();
    }

    void WeaknessTickAttack(Card card)
    {
        if (card.GetCardType() == CardType.Attack)
            TickWeakness();
    }

    void WeaknessTickPowerup(Card card)
    {
        if (card.GetCardType() == CardType.Powerup)
            TickWeakness();
    }

    void WeaknessTickInstant(Card card)
    {
        if (card.GetCardType() == CardType.Instant)
            TickWeakness();
    }

    private void Remove()
    {
        // Debug.LogWarning($"Powerup: removing {this.cardName}");

        target.OnTurnEnd -= OnTurnPassed;
        target.stats.powerups.Remove(this);
#if true
        switch (weakness)
        {
            case PowerupWeakness.None:
                break;
            case PowerupWeakness.OnDamageReceived:
                target.OnDamageReceived -= WeaknessTickDamage;
                break;
            case PowerupWeakness.OnDamageDealt:
                target.OnDamageDealt -= WeaknessTickDamage;
                break;
            case PowerupWeakness.OnCardsPlayed:
                target.OnCardPlayed -= WeaknessTickCard;
                break;
            case PowerupWeakness.OnAttacksPlayed:
                target.OnCardPlayed -= WeaknessTickAttack;
                break;
            case PowerupWeakness.OnPowerupPlayed:
                target.OnCardPlayed -= WeaknessTickPowerup;
                break;
            case PowerupWeakness.OnInstantPlayed:
                target.OnCardPlayed -= WeaknessTickInstant;
                break;
        }
#endif
        OnRemoved?.Invoke();
        Discard();
    }

    private void OnTurnPassed()
    {
        // Debug.Log("Turn Passed for " + gameObject.name);
        if (--turnsLeft <= 0)
            Remove();
    }

    private void TickWeakness(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (--weaknessLeft <= 0)
            {
                Remove();
                break;
            }
        }
    }
}