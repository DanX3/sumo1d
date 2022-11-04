using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Card : MonoBehaviour, IPointerClickHandler
{
    public string cardName;
    public string description;
    
    [Range(0, 8)]
    public int manaCost;
    public TMPro.TMP_Text manaLabel;
    public TMPro.TMP_Text nameLabel;
    public TMPro.TMP_Text descLabel;

    private List<CardModifier> modifiers = new List<CardModifier>();
    protected Player user;

    public abstract CardType GetCardType();

    protected void Start()
    {
        manaLabel.text = "" + manaCost;
        nameLabel.text = cardName;
        descLabel.text = description;
    }

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
        GameManager.Instance.player.PlayCard(this);
    }
}