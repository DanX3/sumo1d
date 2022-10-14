using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{
    public List<ValueModifier> modifiers;

    public virtual void OnActivate() { }
    public virtual void OnDeactivate() { }
}
