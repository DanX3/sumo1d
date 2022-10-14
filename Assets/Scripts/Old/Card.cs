using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Card", menuName = "Cards/Card", order = 1)]
public class Card : ScriptableObject
{
    public string name;
    public int cost;
}
