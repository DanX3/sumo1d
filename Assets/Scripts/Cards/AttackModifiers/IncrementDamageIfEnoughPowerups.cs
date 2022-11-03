public class IncrementDamageIfEnoughPowerups : AttackModifier
{
    public int neededPowerupsCount;
    public int incrementValue;

    public override int GetDamageAdd(Player user) => 
        user.stats.powerups.Count >= neededPowerupsCount ? incrementValue : 0;
}
