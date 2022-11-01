using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHand : MonoBehaviour
{
    public void AddCard(Card card)
    {
        Instantiate(card, transform);
    }

    public void DiscardCard(int index)
    {
        if (index >= transform.childCount)
            return;
        
        Destroy(transform.GetChild(index).gameObject);
    }

    public void PlayCard(int index)
    {
        if (index >= transform.childCount)
            return;

        var card = transform.GetChild(index).GetComponent<Card>();
        GameManager.Instance.player.PlayCard(card);
        GameManager.Instance.player.deck.Discard(index);
        // card.Play(GameManager.Instance.player);
        // Destroy(card.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayCard(0);
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayCard(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            PlayCard(2);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            PlayCard(3);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            PlayCard(4);
    }
}
