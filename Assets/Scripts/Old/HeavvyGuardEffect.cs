using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HeavyGuard", menuName = "Cards/HeavyGuard")]
public class HeavvyGuardEffect : CardEffect
{
    public override void Play()
    {
        Debug.Log("HeavyGuard");
    }
}
