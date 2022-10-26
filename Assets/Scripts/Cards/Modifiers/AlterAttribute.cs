
public class AlterAttribute : CardModifier
{
    public PlayerAttribute targetAttribute;
    public int value;

    public override void Apply(Player user)
    {
        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, value);
    }

    public override void Discard(Player user)
    {
        foreach(Player target in targets)
            target.stats.AlterAttribute(targetAttribute, -value);
    }
}