using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour, IPointerClickHandler
{
    public string cardName;
    public string description;

    [Range(0, 8)]
    [SerializeField] protected int manaCost;
    public Image background;
    public int currentManaCost { get; protected set; }
    public TMPro.TMP_Text manaLabel;
    public TMPro.TMP_Text nameLabel;
    public TMPro.TMP_Text descLabel;

    private List<CardModifier> modifiers = new List<CardModifier>();
    protected Player user;

    public abstract CardType GetCardType();

    void Awake()
    {
        currentManaCost = manaCost;
    }

    protected void Start()
    {
        RefreshUI();
    }

    protected void RefreshUI()
    {
        manaLabel.text = "" + currentManaCost;
        nameLabel.text = cardName;
        descLabel.text = description;
    }

    public void SetManaCost(int newCost)
    {
        currentManaCost = newCost;
        RefreshUI();
    }

    public int GetManaCost() => manaCost;

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

    public virtual void OnReshuffled()
    {
        SetManaCost(manaCost);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (GetComponentInParent<UIHand>() == null) 
            return;

        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            GameManager.Instance.cardDetail.ShowCard(null);
            GameManager.Instance.player.PlayCard(this);
        }
        else if (pointerEventData.button == PointerEventData.InputButton.Right)
            GameManager.Instance.cardDetail.ShowCard(this);
    }
}