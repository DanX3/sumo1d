using UnityEngine;

public abstract class CardModifier : MonoBehaviour
{
   public abstract void Play(Player user, Player target);
   public abstract void Discard(Player user, Player target);
}