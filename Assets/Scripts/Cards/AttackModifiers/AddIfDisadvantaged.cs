using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddIfDisadvantaged : AttackModifier
{
    public int value;

    public override int GetDamageAdd(Player user) =>
        user.stats.hp <= user.GetOpponent().stats.hp ? value : 0;
}
