using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    public CardCondition condition;
    public int value;

}

[System.Serializable]
public class ValueModifier
{
    public Attribute value;
    public int diff;
}
