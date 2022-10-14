using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public int cardsDrawn;
    public int powBonus, spiBonus, weiBonus;
}

public class PlayerRefs
{
    public PowerUpManager powerupManager;
    public Deck<int> deck;
}

public class GameState : MonoBehaviour
{
    public PlayerStats player, opponent;
}
