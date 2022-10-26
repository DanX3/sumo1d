using UnityEngine;

public class CardAttack : Card
{
    public override CardType GetCardType()
    {
        return CardType.Attack;
    }
    
    public override void Play(Player targets)
    {
        base.Play(targets);

        // TODO: do damage to target
    }
}