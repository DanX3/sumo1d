using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupV2 : CardV2
{
    public int duration = 1;
    public abstract Powerup GetPowerup();
    public List<ValueModifier> modifiers;
}
