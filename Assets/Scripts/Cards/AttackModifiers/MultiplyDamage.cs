using UnityEngine;

public class MultiplyDamage : AttackModifier
{
    public float multiplier;

    public override int Apply(Player user, int damage)
    {
        return Mathf.RoundToInt(damage * multiplier);
    }
}
