using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHand : MonoBehaviour
{
    public void AddCard(Card card)
    {

    }

    public void PlayCard(int index)
    {
        if (index >= transform.childCount)
            return;

        var card = transform.GetChild(index).GetComponent<Card>();
        card.Play(GameManager.Instance.player);
        Destroy(card.gameObject);
    }
}
