using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupList : MonoBehaviour
{
    public UIPowerup powerupPrefab;
    public void OnCardPlayed(Card card)
    {
        if (card.GetCardType() != CardType.Powerup)
            return;

        var powerup = Instantiate(powerupPrefab, transform);
        powerup.Init(card as CardPowerup);
    }

    public void TurnPassed()
    {
        foreach (Transform t in transform)
        {
            var powerup = t.GetComponent<UIPowerup>();
            if (powerup != null)
                powerup.DescreaseTurnsLeft();
        }
    }
}
