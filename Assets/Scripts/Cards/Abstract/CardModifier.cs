using System.Collections.Generic;
using UnityEngine;

public abstract class CardModifier : MonoBehaviour
{
    public TargetType targetType;
    protected List<Player> targets = new List<Player>();

    public abstract void Apply(Player user);
    public abstract void Remove(Player user);


    public void Play(Player user)
    {
        InitTargets(user);
        Apply(user);
    }

    private void InitTargets(Player user)
    {
        if (targetType == TargetType.Self || targetType == TargetType.Both)
            targets.Add(user);


        if (targetType == TargetType.Other || targetType == TargetType.Both)
            targets.Add(user.GetOpponent());
    }
}