using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStats : MonoBehaviour
{

    [SerializeField] ProgressBar powerBar;
    [SerializeField] ProgressBar spiritBar;
    [SerializeField] ProgressBar weightBar;
    [SerializeField] ProgressBar reflexesBar;
    [SerializeField] ProgressBar criticalBar;

    Dictionary<Stat, ProgressBar> bars;

    public void Init(int power, int spirit, int weight, int reflexes, int critical)
    {
        bars = new Dictionary<Stat, ProgressBar>()
        {
            {Stat.Power, powerBar},
            {Stat.Spirit, spiritBar},
            {Stat.Weight, weightBar},
            {Stat.Reflexes, reflexesBar},
            {Stat.Critical, criticalBar},
        };

        powerBar.Init(power);
        spiritBar.Init(spirit);
        weightBar.Init(weight);
        reflexesBar.Init(reflexes);
        criticalBar.Init(critical);
    }

    public void SetStat(Stat stat, int value)
    {
        bars[stat].SetValue(value);
    }
}
