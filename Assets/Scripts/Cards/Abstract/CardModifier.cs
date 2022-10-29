using System.Collections.Generic;
using UnityEngine;

public abstract class CardModifier : MonoBehaviour
{
    public int playOrder;
    public TargetType targetType;
    protected List<Player> targets = new List<Player>();

    public abstract void Apply(Player user);

    public void Play(Player user)
    {
        InitTargets(user);
        Apply(user);
    }

    public abstract void Remove(Player user);


    private void InitTargets(Player user)
    {
        if (targetType == TargetType.Self)
            targets.Add(user);
        else if (targetType == TargetType.Other)
            targets.Add(user.GetOpponent());
        else
        {
            targets.Add(user);
            targets.Add(user.GetOpponent());
        }
    }
}