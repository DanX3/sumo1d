using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterStat : InstantEffect
{
    public TargetType target;
    public PowerupBonus stat;
    public int delta = 5;
    public override void Play(Player user)
    {
        if (target == TargetType.Self || target == TargetType.Both)
            user.stats.AlterAttribute(stat, delta);
        
        if (target == TargetType.Other || target == TargetType.Both)
            user.GetOpponent().stats.AlterAttribute(stat, delta);
    }
}
