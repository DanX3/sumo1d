using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public string cardName;
    public string description;
    public int manaCost;

    private List<CardModifier> modifiers = new List<CardModifier>();

    private Player user;
    private Player target;
    

    public abstract CardType GetCardType();


    public virtual void Play(Player user, Player target)
    {
        this.user = user;
        this.target = target;
        modifiers = GetComponents<CardModifier>().ToList();

        foreach (var modifier in modifiers)
        {
            modifier.Play(user, target);
        }
    }

    public void Discard()
    {
        foreach (var modifier in modifiers)
        {
            modifier.Discard(user, target);
        }
    }
}