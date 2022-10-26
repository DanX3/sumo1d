using UnityEngine;

public class AlterArenaSize : CardModifier
{
    public int value;

    public override void Play(Player user, Player target)
    {
        Debug.Log($"Shrinking {target.id} arena size by {value}");
    }

    public override void Discard(Player user, Player target)
    {
        throw new System.NotImplementedException();
    }
}