using UnityEngine;

public class CardPowerup : Card
{
    [Range(1, 10)] public int durationInTurns = 1;

    private Player targetPlayer;
    public PowerupWeakness weakness;
    public int weaknessCount = 1;
    int weaknessLeft;


    new void Start()
    {
        base.Start();
        weaknessLeft = weaknessCount;
    }

    public override CardType GetCardType()
    {
        return CardType.Powerup;
    }

    public override void Play(Player target)
    {
        targetPlayer = target;
        target.stats.powerups.Add(this);

        base.Play(target);

        target.OnTurnEnd += OnTurnPassed;
        switch (weakness)
        {
            case PowerupWeakness.None:
                break;
            case PowerupWeakness.OnDamageReceived:
                target.OnDamageDealt += (_, _) => TickWeakness();
                break;
        }
    }


    private void OnTurnPassed()
    {
        if (--durationInTurns <= 0)
        {
            targetPlayer.stats.powerups.Remove(this);
            Discard();
        }
    }

    private void TickWeakness()
    {
        if (--weaknessLeft <= 0)
        {
            targetPlayer.stats.powerups.Remove(this);
            Discard();
        }
    }
}