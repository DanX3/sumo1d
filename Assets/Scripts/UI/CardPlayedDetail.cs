using System.Collections;
using UnityEngine;

public class CardPlayedDetail : MonoBehaviour
{
    public Transform playerDetailCardParent;
    public Transform opponentDetailCardParent;
    public float duration;

    private Card showingCard;

    public void ShowCardDetail(Player player, Card card)
    {
        if (showingCard != null)
            GameManager.Destroy(showingCard.gameObject);

        if (player.isPlayer)
            showingCard = GameObject.Instantiate(card, playerDetailCardParent);
        else
            showingCard = GameObject.Instantiate(card, opponentDetailCardParent);

        StartCoroutine(RemoveShowingCard());
    }

    IEnumerator RemoveShowingCard()
    {
        yield return new WaitForSeconds(duration);
        if (showingCard != null)
            GameManager.Destroy(showingCard.gameObject);
    }
}
