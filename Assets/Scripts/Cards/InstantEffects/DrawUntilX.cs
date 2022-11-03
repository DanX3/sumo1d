using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawUntilX : InstantEffect
{
    public int handTarget = 5;
    public override void Play(Player user)
    {
        while (user.deckManager.handCount < handTarget)
            user.deckManager.Draw();
    }
}
