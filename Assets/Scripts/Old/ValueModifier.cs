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
    MonoBehaviour effect;
    public Condition condition;
    public Target target;
    public EColor value;
    public int diff;
}
