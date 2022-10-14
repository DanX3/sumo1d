using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    int power = 5, spirit = 5, weight = 5;

    List<PowerUp> powerUps = new List<PowerUp>();
    public bool isPlayer;

    public void AddPowerup(PowerUp powerUp)
    {
        // 1. Affect stats
        var gameState = FindObjectOfType<GameState>();
        AffectPlayerStats(ref gameState.opponent, powerUp);
        // AffectPlayerStats(ref (isPlayer ? gameState.player : gameState.opponent), powerUp);



        // 2. Activate powerup effect
        powerUp.OnActivate();
    }


    private void AffectPlayerStats(ref PlayerStats stats, PowerUp powerUp)
    {

        foreach (var modifier in powerUp.modifiers)
        {
            switch (modifier.value)
            {
                case EColor.Pow:
                    stats.powBonus += modifier.diff;
                    break;
                case EColor.Spi:
                    stats.spiBonus += modifier.diff;
                    break;
                case EColor.Wei:
                    stats.weiBonus += modifier.diff;
                    break;
            }
        }
    }
}
