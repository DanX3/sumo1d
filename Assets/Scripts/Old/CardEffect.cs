using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardEffect", menuName = "Cards/CardEffect", order = 1)]
public abstract class CardEffect : ScriptableObject
{
    public abstract void Play();
}