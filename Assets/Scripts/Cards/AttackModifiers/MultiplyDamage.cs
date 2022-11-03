public class MultiplyDamage : AttackModifier
{
    public float multiplier;

    public override float GetDamageMul(Player user) => multiplier;
}
