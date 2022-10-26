using UnityEngine;

public class AlterArenaSize : CardModifier
{
    public PlayerType targetArena;
    public int value;

    public override void Play(Player user, Player target)
    {
        Debug.Log($"Shrinking {targetArena} arena size by {value}");
    }

    public override void Discard(Player user, Player target)
    {
        throw new System.NotImplementedException();
    }
}