using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class OpponentManager : MonoBehaviour
{
    public List<OpponentAction> actions;

    private Player player;

    void Awake()
    {
        player = GetComponent<Player>();        
    }

    public List<Card> GetRandomAction(int mana)
    {
        var possibleActions = actions.FindAll(a => a.totalMana == mana);
        int randomIndex = Random.Range(0, possibleActions.Count);

        return possibleActions[randomIndex].cards;
    }

    public void DoTurn(int usableMana)
    {
        foreach (Card card in GetRandomAction(usableMana))
            player.PlayCard(card);
    }
}

[System.Serializable]
public class OpponentAction
{
    public int totalMana;
    public List<Card> cards;
}