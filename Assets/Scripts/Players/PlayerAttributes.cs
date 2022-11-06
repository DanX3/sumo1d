using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttributes
{
    [Range(1, 9)]
    public int power;
    [Range(1, 9)]
    public int spirit;
    [Range(1, 9)]
    public int weight;
    [Range(1, 9)]
    public int reflex;
    [Range(1, 9)]
    public int critical;
}
