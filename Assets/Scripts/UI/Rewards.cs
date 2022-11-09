using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    PlayerAttribute attributeToIncrease;
    bool setAttribute = false;
    public Transform cardAddParent;

    public List<Card> cards;
    Player player;

    public IncreaseAttributeRow power;
    public IncreaseAttributeRow spirit;
    public IncreaseAttributeRow weight;
    public IncreaseAttributeRow reflexes;
    public IncreaseAttributeRow critical;

    public void Init(Player player)
    {
        this.player = player;
        
        power.SetAttribute(player);
        spirit.SetAttribute(player);
        weight.SetAttribute(player);
        reflexes.SetAttribute(player);
        critical.SetAttribute(player);

        foreach (var row in FindObjectsOfType<IncreaseAttributeRow>())
        {
            Debug.LogWarning(row.name);
            row.SetAttribute(player);
        }

        for (int i = 0; i < 3; i++)
        {

            var newCard = Instantiate(cards[Random.Range(0, cards.Count)], cardAddParent);
            newCard.gameObject.AddComponent<CardAdded>();
        }
        gameObject.SetActive(false);
    }

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

    public void ChoseCard(Card card)
    {
        Debug.Log("CardChosen");
        cardAdded = card;
        RefreshButton();
    }

    public Button OkButton;

    public void Accept()
    {
        var cardsName = new List<string>() { cardAdded.cardName };
        foreach (var card in player.deckManager.deckList)
            cardsName.Add(card.cardName);
        var stats = player.GetSavedStats();
        switch (attributeToIncrease)
        {
            case PlayerAttribute.Power: stats.power++; break;
            case PlayerAttribute.Spirit: stats.spirit++; break;
            case PlayerAttribute.Weight: stats.weight++; break;
            case PlayerAttribute.Reflex: stats.reflex++; break;
            case PlayerAttribute.Critical: stats.critical++; break;
        }
        PlayerPrefs.SetString("cards", JsonUtility.ToJson(new StartingCards(cardsName)));
        PlayerPrefs.SetString("stats", JsonUtility.ToJson(stats));

        SceneManager.LoadScene("BattleScene");
    }

    public Card GetCardByName(string name)
    {
        foreach (var card in cards)
            if (card.cardName == name)
                return card;

        Debug.LogWarning("No card found with name " + name);
        return null;
    }
}
