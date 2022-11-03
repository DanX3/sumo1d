public class IncrementDamageIfDisadvantaged : AttackModifier
{
    public int value;

    public override int GetDamageAdd(Player user) =>
        user.stats.hp <= user.GetOpponent().stats.hp ? value : 0;
}
