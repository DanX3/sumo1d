using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardAttack : Card
{
    public int baseDamage;
    private readonly float CRITICAL_MULTIPLIER = 0.5f;

    private List<AttackModifier> attackModifiers;

    public override CardType GetCardType()
    {
        return CardType.Attack;
    }

    public override void Play(Player user)
    {
        base.Play(user);
        attackModifiers = GetComponents<AttackModifier>()
                            .OrderBy(m => m.playOrder)
                            .ToList();
                            
        int cardDamage = CalculateCardDamage(user);
        bool isCritical = user.IsCriticalHit();
        int totalDamage = isCritical 
            ? Mathf.FloorToInt(cardDamage * CRITICAL_MULTIPLIER) 
            : cardDamage;

        user.DoDamage(this, totalDamage, isCritical);
    }

    private int CalculateCardDamage(Player user)
    {
        int realDamage = baseDamage;

        foreach (AttackModifier modifier in attackModifiers)
            realDamage = modifier.Apply(user, realDamage);

        return realDamage;
    }
}