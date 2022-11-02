using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulPowerupOpponent : AttackModifier
{
    public override float GetDamageMul(Player user) =>
        (float)user.GetOpponent().stats.powerups.Count;
}
