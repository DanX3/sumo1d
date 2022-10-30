
using UnityEngine;

public class CardPowerup : Card
{
    public int durationInTurns;

    private Player targetPlayer;

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
    }

    private void OnTurnPassed()
    {
        durationInTurns--;

        if (durationInTurns == 0)
        {
            targetPlayer.stats.powerups.Remove(this);
            Discard();
        }
    }
}