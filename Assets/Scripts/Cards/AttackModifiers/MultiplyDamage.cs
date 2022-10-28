using UnityEngine;

public class MultiplyDamage : AttackModifier
{
    public float multiplier;

    public override int Apply(int damage)
    {
        return Mathf.RoundToInt(damage * multiplier);
    }
}
