public enum CardType
{
    Instant,
    Attack,
    Powerup
}

public enum PlayerAttribute
{
    Power,
    Spirit,
    Weight,
    Reflex,
    Critical
}

public enum TargetType
{
    Self,
    Other, 
    Both
}

public enum PowerupBonus
{
    Power,
    Spirit,
    Weight,
    Reflex,
    Critical,
    DamageAdd,
    DamageMul,
    CritAdd,
    CritMul,
    HandCount,
    Arena,
    DamageThreshold
}

public enum PowerupWeakness
{
    None,
    OnDamageReceived,
    OnDamageDealt,
    OnCardsPlayed,
    OnPlayerStartTurn,
    OnPlayerEndTurn,
    OnManaUsed,
    OnAttacksPlayed,
    OnPowerupPlayed,
    OnInstantPlayed,
}