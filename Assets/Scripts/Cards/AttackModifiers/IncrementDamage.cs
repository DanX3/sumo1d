public class IncrementDamage : AttackModifier
{
    public int value;

    public override int Apply(Player user, int damage)
    {
        return damage + value;
    }
}
