using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    PlayerAttribute attributeToIncrease;
    bool setAttribute = false;
    public Transform cardAddParent;

    public List<Card> cards;

    public void IncreaseStat(int stat)
    {
        IncreaseStat((PlayerAttribute)stat);
    }

    public void IncreaseStat(PlayerAttribute stat)
    {
        setAttribute = true;
        attributeToIncrease = stat;
        RefreshButton();
    }



    public void Init(Player player)
    {
        foreach (var row in FindObjectsOfType<IncreaseAttributeRow>())
            row.SetAttribute(player);

        for (int i = 0; i < 3; i++)
        {

            var newCard = Instantiate(cards[Random.Range(0, cards.Count)], cardAddParent);
            newCard.gameObject.AddComponent<CardAdded>();
        }
    }

    Card cardAdded = null;

    private void RefreshButton()
    {
        OkButton.interactable = setAttribute && (cardAdded != null);
    }

    public void AddCard(Card card)
    {
        cardAdded = card;
        RefreshButton();
    }

    public Button OkButton;

    public void Accept()
    {
        // modificare player prefs
    }
}
