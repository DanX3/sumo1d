using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public UIStats stats;

    void Start()
    {
        stats.Init(3, 4, 5, 6, 7);
        stats.SetStat(Stat.Power, 5);
        stats.SetStat(Stat.Critical, 4);
    }
}
