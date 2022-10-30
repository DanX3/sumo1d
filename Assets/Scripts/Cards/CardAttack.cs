using System.Collections.Generic;
using System.Linq;

public class CardAttack : Card
{
    public int baseDamage;

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

        CalculateDamage(user);

        // TODO user.DoDamage();
    }

    private int CalculateDamage(Player user)
    {
        int realDamage = baseDamage;

        foreach (AttackModifier modifier in attackModifiers)
            realDamage = modifier.Apply(user, realDamage);

        return realDamage;
    }
}