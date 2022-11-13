using System.Collections;
using UnityEngine;

public class CardDetail : MonoBehaviour
{
    private Card detailCard;
    private Coroutine showCardCoroutine;

    public void ShowCard(Card card)
    {
        if (detailCard != null)
        {
            StopCoroutine(showCardCoroutine);
            GameObject.Destroy(detailCard.gameObject);
        }

        if (card != null)
        {
            detailCard = GameObject.Instantiate(card, transform);

            RectTransform originalTransform = card.GetComponent<RectTransform>();
            RectTransform showingCardTransform = detailCard.GetComponent<RectTransform>();

            showingCardTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalTransform.rect.width);
            showingCardTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalTransform.rect.height);

            showingCardTransform.anchorMin = new Vector2(0.5f, 0.5f);
            showingCardTransform.anchorMax = new Vector2(0.5f, 0.5f);
            showingCardTransform.pivot = new Vector2(0.5f, 0.5f);

            showCardCoroutine = StartCoroutine(ShowCardCoroutine());
        }
    }

    IEnumerator ShowCardCoroutine()
    {
        yield return new WaitForSeconds(4);
        GameObject.Destroy(detailCard.gameObject);
    }
}
