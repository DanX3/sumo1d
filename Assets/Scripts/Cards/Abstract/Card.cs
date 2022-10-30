using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Card : MonoBehaviour, IPointerClickHandler
{
    public string cardName;
    public string description;
    public int manaCost;

    private List<CardModifier> modifiers = new List<CardModifier>();
    protected Player user;
    
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
            modifier.Remove(user);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("On click");
        FindObjectOfType<UIHand>().PlayCard(transform.GetSiblingIndex());
    }
}