using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardAdded : MonoBehaviour, IPointerClickHandler
{

    void Start()
    {
        Select(false);
    }

    public void Select(bool select)
    {
        if (select)
        {
            foreach (var card in FindObjectsOfType<CardAdded>())
                card.Select(false);
        }

        var color = GetComponent<Card>().background.color;
        color.a = select ? 1f : 0.5f;
        GetComponent<Card>().background.color = color;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select(true);
    }
}
