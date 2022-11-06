using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIArena : MonoBehaviour
{
    public int startHp = Player.StartHP;

    public void Init(Player player)
    {
        player.stats.OnPowerupBonus += OnPowerupBonus;
    }

    void OnPowerupBonus(PowerupBonus bonus, float delta)
    {
        Debug.Log($"Powerup: {bonus} + {delta}");
        
        if (bonus != PowerupBonus.Arena)
            return;

        transform.position += Vector3.right * delta;
    }

}
