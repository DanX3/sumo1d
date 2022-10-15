using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public List<CardV2> cards;

    void Start()
    {
        foreach (var card in cards)
        {
            card.Play();
        }
    }
}
