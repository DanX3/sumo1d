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

    Dictionary<PlayerAttribute, ProgressBar> bars;

    // public void Init(int power, int spirit, int weight, int reflexes, int critical)
    public void Init(PlayerStats stats)
    {
        bars = new Dictionary<PlayerAttribute, ProgressBar>()
        {
            {PlayerAttribute.Power, powerBar},
            {PlayerAttribute.Spirit, spiritBar},
            {PlayerAttribute.Weight, weightBar},
            {PlayerAttribute.Reflex, reflexesBar},
            {PlayerAttribute.Critical, criticalBar},
        };

        powerBar.Init(stats.baseStats.power);
        spiritBar.Init(stats.baseStats.spirit);
        weightBar.Init(stats.baseStats.weight);
        reflexesBar.Init(stats.baseStats.reflexes);
        criticalBar.Init(stats.baseStats.critical);

        stats.onRefreshStat += SetStat;
    }

    public void SetStat(PlayerAttribute stat, int value)
    {
        bars[stat].SetValue(value);
    }
}
