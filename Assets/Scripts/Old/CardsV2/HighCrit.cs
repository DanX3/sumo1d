using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighCrit", menuName = "Attacks/HighCrit", order = 1)]
public class HighCrit : Attack
{
    public override void Play(Player player, Player opponent)
    {
        stats.critMultiplier = 2f;
    }
}
