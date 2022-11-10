using UnityEngine;

public class UIHand : MonoBehaviour
{
    public Player player;


    private void Awake()
    {
        player.deckManager.OnDrawCard += AddCard;
        player.deckManager.OnDiscardCard += RemoveCard;
    }

    private void AddCard(Card card)
    {
        card.transform.SetParent(transform, false);
    }

    private void RemoveCard(Card card)
    {
        card.transform.SetParent(player.transform, false);
    }
}
