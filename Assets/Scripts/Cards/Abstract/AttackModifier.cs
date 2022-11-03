using UnityEngine;

public abstract class AttackModifier : MonoBehaviour
{
    public virtual int GetDamageAdd(Player user) => 0;
    public virtual float GetDamageMul(Player user) => 1f;
    public virtual float GetCritAdd(Player user) => 0f;
    public virtual float GetCritMul(Player user) => 1f;
}
