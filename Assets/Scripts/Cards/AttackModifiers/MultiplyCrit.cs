using UnityEngine;

public class MultiplyCrit : AttackModifier
{
    public float value;

    public override float GetCritMul(Player user) => value;

}
