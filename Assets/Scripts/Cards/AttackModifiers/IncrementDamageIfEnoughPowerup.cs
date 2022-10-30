public class IncrementDamageIfEnoughPowerup : AttackModifier
{
    public int neededPowerupsCount;
    public int incrementValue;

    public override int Apply(Player user, int damage)
    {
        return user.stats.powerups.Count >= neededPowerupsCount 
            ? damage + incrementValue
            : damage;
    }
}
