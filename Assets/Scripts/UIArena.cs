using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIArena : MonoBehaviour
{

    int hp;
    public int startHp = Player.StartHP;
    float startSizeX;

    public void Init(Player player)
    {
        hp = startHp = player.stats.baseStats.arena;
        startSizeX = GetComponent<RectTransform>().sizeDelta.x;
        player.stats.OnPowerupBonus += OnPowerupBonus;
    }

    public void AddDiff(int diff)
    {
        hp += diff;
        float scaleFactor = (float)hp / startHp;
        float targetSize = startSizeX * scaleFactor;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetSize);
    }

    void OnPowerupBonus(PowerupBonus bonus, float delta)
    {
        Debug.Log(bonus.ToString() + ", " + delta);
        if (bonus != PowerupBonus.Arena)
            return;

        AddDiff(Mathf.RoundToInt(delta));
    }

}
