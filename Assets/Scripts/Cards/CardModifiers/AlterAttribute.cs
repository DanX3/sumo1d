public class AlterAttribute : CardModifier
{
    public PowerupBonus targetAttribute;
    public int value;

    public override void Apply(Player user)
    {
        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, value);
    }

    public override void Remove(Player user)
    {
        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, -value);
    }
}