using UnityEngine;

public class AlterAttribute : CardModifier
{
    public PowerupBonus targetAttribute;
    public float value;

    public override void Apply(Player user)
    {
        // Debug.Log($"AlterAttribute of {gameObject.name} added");
        foreach (Player target in targets)
            target.stats.AlterAttribute(targetAttribute, value);
    }

    public override void Remove(Player user)
    {
        // Debug.Log($"AlterAttribute of {gameObject.name} removed");
        foreach (Player target in targets)
            target.stats.AlterAttribute(targetAttribute, -value);
    }
}