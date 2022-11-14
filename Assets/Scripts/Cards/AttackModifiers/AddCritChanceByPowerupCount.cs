using UnityEngine;

public class AddCritChanceByPowerupCount : AttackModifier
{
    public int addPerPowerup = 5;

    public override float GetCritAdd(Player user) => 
        user.stats.powerups.Count * addPerPowerup;

}
