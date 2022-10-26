using UnityEngine;

public class AttackCard : Card
{
    public override CardType GetCardType()
    {
        return CardType.Attack;
    }
    
    public override void Play(Player user, Player target)
    {
        base.Play(user, target);

        // TODO: do damage to target
    }
}