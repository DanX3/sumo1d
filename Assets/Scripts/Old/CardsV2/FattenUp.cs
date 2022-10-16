using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardV2", menuName = "Cards/FattenUp", order = 1)]
public class FattenUp : PowerupV2
{
    public override Powerup GetPowerup()
    {
        return null;
    }

    public override void Play(Player player, Player opponent)
    {
        Debug.Log("Played Fatten Up");
    }
}
