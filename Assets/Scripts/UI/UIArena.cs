using UnityEngine;
using DG.Tweening;

public class UIArena : MonoBehaviour
{
    public int startHp = Player.StartHP;
    public int dir;

    public void Init(Player player)
    {
        player.stats.OnPowerupBonus += OnPowerupBonus;
    }

    void OnPowerupBonus(PowerupBonus bonus, float delta)
    {
        Debug.Log($"Powerup: {bonus} + {delta}");

        if (bonus != PowerupBonus.Arena)
            return;


        transform.DOMoveX(transform.position.x + delta * dir, 1f)
            .SetEase(Ease.OutCubic)
            .Play();
    }

}
