using UnityEngine;

public class CardPowerup : Card
{
    [Range(1, 10)] public int durationInTurns = 1;
    int turnsLeft;
    [SerializeField] TMPro.TMP_Text durationLabel;

    private Player target;
    public PowerupWeakness weakness;
    public int weaknessCount = 1;
    int weaknessLeft;
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
                target.OnCardPlayed += (card) => { if (card.GetCardType() == CardType.Attack) TickWeakness(); };
                break;
            case PowerupWeakness.OnPowerupPlayed:
                target.OnCardPlayed += (card) => { if (card.GetCardType() == CardType.Powerup) TickWeakness(); };
                break;
            case PowerupWeakness.OnInstantPlayed:
                target.OnCardPlayed += (card) => { if (card.GetCardType() == CardType.Instant) TickWeakness(); };
                break;
        }

        user.playedCardsHistory.Add(this);
    }

    void WeaknessTickDamage(int damage, bool isCritical)
    {
        TickWeakness(damage);
    }

    void WeaknessTickCard(Card card)
    {
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
                Debug.LogWarning("Removed tick on user damage received");
                break;
            case PowerupWeakness.OnDamageDealt:
                target.OnDamageDealt -= WeaknessTickDamage;
                break;
            case PowerupWeakness.OnCardsPlayed:
                target.OnCardPlayed -= WeaknessTickCard;
                break;
            // case PowerupWeakness.OnAttacksPlayed:
            //     target.OnCardPlayed += (card) => { if (card.GetCardType() == CardType.Attack) TickWeakness(); };
            //     break;
            // case PowerupWeakness.OnPowerupPlayed:
            //     target.OnCardPlayed += (card) => { if (card.GetCardType() == CardType.Powerup) TickWeakness(); };
            //     break;
            // case PowerupWeakness.OnInstantPlayed:
            //     target.OnCardPlayed += (card) => { if (card.GetCardType() == CardType.Instant) TickWeakness(); };
            //     break;
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