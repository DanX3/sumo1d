using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public Player player;

    public void Start()
    {
        player.OnDamageDealt += (_, _) => animator.SetTrigger("Hit");
        player.OnDamageReceived += (_, _) => animator.SetTrigger("Hurt");
        player.GetOpponent().OnDefeat += () => animator.SetTrigger("Win");
        player.OnDefeat += () => animator.SetTrigger("Lost");
    }
}
