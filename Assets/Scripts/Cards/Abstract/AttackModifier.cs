using UnityEngine;

public abstract class AttackModifier : MonoBehaviour
{
    public int playOrder;

    public abstract int Apply(Player user, int damage);

    public int Play(Player user, int damage)
    {
        return Apply(user, damage);
    }
}
