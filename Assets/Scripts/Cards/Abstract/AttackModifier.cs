using UnityEngine;

public abstract class AttackModifier : MonoBehaviour
{
    public int playOrder;

    public abstract int Apply(int damage);

    public int Play(int damage)
    {
        return Apply(damage);
    }
}
