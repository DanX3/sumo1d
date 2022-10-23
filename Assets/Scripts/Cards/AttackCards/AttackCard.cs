using UnityEngine;

public class AttackCard : Card
{
    public override CardType GetCardType()
    {
        return CardType.Attack;
    }
}