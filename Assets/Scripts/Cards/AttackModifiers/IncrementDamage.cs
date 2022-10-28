using UnityEngine;

public class IncrementDamage : AttackModifier
{
    public int value;

    public override int Apply(int damage)
    {
        return Mathf.RoundToInt(damage + value);
    }
}
