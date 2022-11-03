using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddArenaBoth : InstantEffect
{
    public int value = 5;
    public override void Play(Player user)
    {
        user.stats.AlterAttribute(PowerupBonus.Arena, value);
        user.GetOpponent().stats.AlterAttribute(PowerupBonus.Arena, value);
    }
}
