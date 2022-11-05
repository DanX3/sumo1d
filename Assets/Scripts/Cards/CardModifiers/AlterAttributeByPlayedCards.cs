using UnityEngine;

public class AlterAttributeByPlayedCards : CardModifier
{
    public PowerupBonus targetAttribute;
    private int value;

    public override void Apply(Player user)
    {
        value = user.playedCardsHistory.GetTurnHistory(GameManager.Instance.turnCounter).Count;
        // Debug.Log("AlterAttributeByPlayedCard value " + value);
        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, value);
    }

    public override void Remove(Player user)
    {
        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, -value);
    }
}