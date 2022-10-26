using UnityEngine;

public class AlterAttribute : CardModifier
{
    public PlayerAttribute targetAttribute;
    public int value;

    public override void Play()
    {
        Debug.Log($"Modifing {targetAttribute} by {value}");
    }

    public override void Discard()
    {
        throw new System.NotImplementedException();
    }
}