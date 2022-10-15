using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardV2", menuName = "Cards/Card", order = 1)]
public abstract class CardV2 : ScriptableObject
{
    public int cost;
    public new string name;
    public string description;

    public abstract void Play();
}
