using UnityEngine;

public abstract class AttackModifier : MonoBehaviour
{
    public abstract int Apply(Player user, int damage);

    public int Play(Player user, int damage)
    {
        return Apply(user, damage);
    }
}
