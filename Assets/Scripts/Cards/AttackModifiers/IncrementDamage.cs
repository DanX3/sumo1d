public class IncrementDamage : AttackModifier
{
    public int value;

    public override int GetDamageAdd(Player user) => value;

}
