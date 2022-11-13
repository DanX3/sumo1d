using System.Collections;
using UnityEngine;

public class CardPlayedDetail : MonoBehaviour
{
    public Transform playerDetailCardParent;
    public Transform opponentDetailCardParent;
    public float duration;

    private Card showingCard;
    private Coroutine removeShowingCard;

    public void ShowCardDetail(Player player, Card card)
    {
        HideCardDetail();

        if (player.isPlayer)
            showingCard = GameObject.Instantiate(card, playerDetailCardParent);
        else
            showingCard = GameObject.Instantiate(card, opponentDetailCardParent);

        // manually set card width and height because player cards are in a grid layout and they have width and height set to 0)
        RectTransform originalTransform = card.GetComponent<RectTransform>();
        RectTransform showingCardTransform = showingCard.GetComponent<RectTransform>();

        showingCardTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalTransform.rect.width);
        showingCardTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalTransform.rect.height);

        showingCardTransform.anchorMin = new Vector2(0.5f, 0.5f);
        showingCardTransform.anchorMax = new Vector2(0.5f, 0.5f);
        showingCardTransform.pivot = new Vector2(0.5f, 0.5f);

        removeShowingCard = StartCoroutine(RemoveShowingCard());
    }

    public void HideCardDetail()
    {
        if (showingCard != null)
        {
            StopCoroutine(removeShowingCard);
            GameManager.Destroy(showingCard.gameObject);
        }
    }

    IEnumerator RemoveShowingCard()
    {
        yield return new WaitForSeconds(duration);
        if (showingCard != null)
            GameManager.Destroy(showingCard.gameObject);
    }
}
