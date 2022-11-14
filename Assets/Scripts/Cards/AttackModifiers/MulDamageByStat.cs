using UnityEngine;

public class MulDamageByStat : AttackModifier
{
    public PlayerAttribute stat;


    public override float GetDamageMul(Player player)
    {
        switch (stat)
        {
            case PlayerAttribute.Power: return player.stats.power;
            case PlayerAttribute.Spirit: return player.stats.spirit;
            case PlayerAttribute.Weight: return player.stats.weight;
            case PlayerAttribute.Reflex: return player.stats.reflex;
            case PlayerAttribute.Critical: return player.stats.critical;
        }
        return 1f;
    }

}
