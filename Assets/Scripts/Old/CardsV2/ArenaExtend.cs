using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardV2", menuName = "Cards/ArenaExtender", order = 1)]
public class ArenaExtend : PowerupV2
{
    public override int GetPowerup()
    {
        return 1;
    }

    public override void Play()
    {
        Debug.Log("Played arena extender");
    }
}
