using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public UIStats stats;
    public ManaSlots slots;

    void Start()
    {
        // stats.Init(3, 4, 5, 6, 7);
        stats.SetStat(PlayerAttribute.Power, 5);
        stats.SetStat(PlayerAttribute.Critical, 4);
        StartCoroutine(TestSlots());

    }

    IEnumerator TestSlots()
    {
        slots.AddManaDiff(3);
        yield return new WaitForSeconds(1f);
        slots.AddManaDiff(1);
        yield return new WaitForSeconds(1f);
        slots.AddManaDiff(-2);
        yield return new WaitForSeconds(1f);
        slots.AddManaDiff(7);
        // slots.Reset();
    }
}
