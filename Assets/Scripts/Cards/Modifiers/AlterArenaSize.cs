using UnityEngine;

public class AlterArenaSize : CardModifier
{
    public PlayerType targetArena;
    public int value;

    public override void Play()
    {
        Debug.Log($"Shrinking {targetArena} arena size by {value}");
    }

    public override void Remove()
    {
        throw new System.NotImplementedException();
    }
}