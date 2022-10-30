public class AlterAttributeByPlayedCards : CardModifier
{
    public PlayerAttribute targetAttribute;
    private int value;

    public override void Apply(Player user)
    {
        value = user.playedCardsHistory.PlayerCardHistoryInTurn(user, GameManager.Instance.turnCounter).Count;

        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, value);
    }

    public override void Remove(Player user)
    {
        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, -value);
    }

    public void OnEventFired()
    {
        // controlla se è ancora valido
        // se non lo è => Discard()
    }
}