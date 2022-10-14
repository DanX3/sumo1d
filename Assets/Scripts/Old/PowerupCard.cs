using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Powerup", order = 2)]
public class PowerupCard : Card
{
    public int duration;
    
    [TextArea]
    public string description;
    public List<ValueModifier> modifiers;
}
