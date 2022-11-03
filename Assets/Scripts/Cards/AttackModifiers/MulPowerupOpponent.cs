public class MulPowerupOpponent : AttackModifier
{
    public override float GetDamageMul(Player user) =>
        (float)user.GetOpponent().stats.powerups.Count;
}
