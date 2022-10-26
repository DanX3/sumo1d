using UnityEngine;

public class CardInstant : Card
{
    public override CardType GetCardType()
    {
        return CardType.Instant;
    }
    
    public override void Play(Player user, Player target)
    {
        base.Play(user, target);

        // TODO: do instant stuff
    }
}