public class AlterAttributeByPlayedCards : CardModifier
{
    public PlayerAttribute targetAttribute;
    private int value;

    public override void Apply(Player user)
    {
        value = user.GetPlayedCardsInTurnCount();

        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, value);
    }

    public override void Discard(Player user)
    {
        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, -value);
    }
}