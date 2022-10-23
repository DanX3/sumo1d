using System.Collections.Generic;
using System.Linq;

public class PowerupCard : Card
{
    public int durationInTurns;

    private List<CardModifier> modifiers = new List<CardModifier>();

    public override CardType GetCardType()
    {
        return CardType.Powerup;
    }
}