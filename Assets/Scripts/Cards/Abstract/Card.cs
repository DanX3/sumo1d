using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public string cardName;
    public string description;
    public int manaCost;

    private List<CardModifier> modifiers = new List<CardModifier>();
    protected Player user;


    public abstract CardType GetCardType();

    // TODO deve fare cose quando viene pescata

    public virtual void Play(Player user)
    {
        this.user = user;

        modifiers = GetComponents<CardModifier>()
                        .OrderBy(m => m.playOrder)
                        .ToList();

        foreach (var modifier in modifiers)
        {
            modifier.Play(user);
        }
    }

    public void Discard()
    {
        foreach (var modifier in modifiers)
        {
            modifier.Remove(user);
        }
    }
}