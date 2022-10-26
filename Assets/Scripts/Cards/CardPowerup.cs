using System.Collections.Generic;
using System.Linq;

public class CardPowerup : Card
{
    public int durationInTurns;

    public override CardType GetCardType()
    {
        return CardType.Powerup;
    }

    public override void Play(Player targets)
    {
        base.Play(targets);

        // TODO: apply powerup to player
    }
}