public class AlterAttributeByPlayedCards : CardModifier
{
    public PowerupBonus targetAttribute;
    private int value;

    public override void Apply(Player user)
    {
        value = user.playedCardsHistory.PlayerCardHistoryInTurn(GameManager.Instance.turnCounter).Count;

        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, value);
    }

    public override void Remove(Player user)
    {
        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, -value);
    }
}