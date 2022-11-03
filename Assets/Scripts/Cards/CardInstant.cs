
public class CardInstant : Card
{
    public override CardType GetCardType()
    {
        return CardType.Instant;
    }

    public override void Play(Player user)
    {
        base.Play(user);

        foreach (var effect in GetComponents<InstantEffect>())
            effect.Play(user);
    }
}