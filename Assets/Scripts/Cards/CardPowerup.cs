using System.Collections.Generic;
using System.Linq;

public class CardPowerup : Card
{
    public int durationInTurns;

    public override CardType GetCardType()
    {
        return CardType.Powerup;
    }

    public override void Play(Player user, Player target)
    {
        base.Play(user, target);

        // TODO: apply powerup to player
    }
}