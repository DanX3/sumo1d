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


    public abstract CardType GetCardType();


    public virtual void Play(Player user)
    {
        this.user = user;

        modifiers = GetComponents<CardModifier>().ToList();

        foreach (var modifier in modifiers)
        {
            modifier.Play(user);
        }
    }

    public void Discard()
    {
        foreach (var modifier in modifiers)
        {
            modifier.Discard(user);
        }
    }
}