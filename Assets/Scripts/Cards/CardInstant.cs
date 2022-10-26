using UnityEngine;

public class CardInstant : Card
{
    public override CardType GetCardType()
    {
        return CardType.Instant;
    }
    
    public override void Play(Player targets)
    {
        base.Play(targets);

        // TODO: do instant stuff
    }
}