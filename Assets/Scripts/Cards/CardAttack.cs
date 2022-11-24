using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CardAttack : Card
{
    public int baseDamage;
    private readonly float CRITICAL_MULTIPLIER = 1.5f;
    [SerializeField] TMPro.TMP_Text damageLabel;


    private List<AttackModifier> attackModifiers;

    new void Start()
    {
        base.Start();
        damageLabel.text = baseDamage + "";
    }

    public override CardType GetCardType()
    {
        return CardType.Attack;
    }

    public override void Play(Player user)
    {
        base.Play(user);
        attackModifiers = GetComponents<AttackModifier>().ToList();
        SoundManager.Instance.PlayRandomAttackSound();

        int damage = CalculateCardDamage(user);

        // stats influence
        damage = Mathf.FloorToInt((damage * user.stats.powerMul / user.GetOpponent().stats.weightMul) +
            user.stats.spirit - user.GetOpponent().stats.reflex);

        // critical hit
        float critChance = CalculateCardCritical(user.stats.critChance);
        bool isCritical = Random.Range(0f, 1f) < critChance;
        if (isCritical)
        {
            SoundManager.Instance.PlayShortApplaudingCrowd();
            damage = Mathf.FloorToInt(damage * CRITICAL_MULTIPLIER);
        }

        // nulls damage if lower than opponent damage threshold
        if (damage < user.GetOpponent().stats.damageThreshold)
            damage = 0;

        user.playedCardsHistory.Add(this, damage, isCritical);

        user.DoDamage(this, damage, isCritical);
    }

    private int CalculateCardDamage(Player user)
    {
        int realDamage = baseDamage;

        foreach (AttackModifier modifier in attackModifiers)
        {
            realDamage = Mathf.FloorToInt(realDamage * modifier.GetDamageMul(user) + modifier.GetDamageAdd(user));
        }

        return realDamage;
    }

    private float CalculateCardCritical(float critChance)
    {
        foreach (AttackModifier modifier in attackModifiers)
        {
            critChance = critChance * modifier.GetCritMul(user) + modifier.GetCritAdd(user);
        }

        return critChance;
    }
}