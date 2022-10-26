using UnityEngine;

public class AlterUserAttribute : CardModifier
{
    public PlayerAttribute targetAttribute;
    public int value;

    public override void Play(Player user, Player target)
    {
        Debug.Log($"Adding {user.id} {targetAttribute} by {value}");
        
        user.stats.AlterAttribute(targetAttribute, value);
    }

    public override void Discard(Player user, Player target)
    {
        Debug.Log($"Removing {targetAttribute} by {value}");
        
        user.stats.AlterAttribute(targetAttribute, -value);
    }
}