using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SampleAttack", menuName = "Attacks/SampleAttack", order = 1)]
public class SampleAttack : Attack
{
    public override void Play(Player user, Player target)
    {
        if (user.stats.powerupCount >= 3)
            stats.powBonus += 5;
    }
}
