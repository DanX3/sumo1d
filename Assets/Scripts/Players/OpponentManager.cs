using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class OpponentManager : MonoBehaviour
{
    public string opponentName;
    public string opponentDescription;
    public int opponentLevel;
    public GameObject model;
    public List<OpponentAction> actions;

    [HideInInspector]
    public Player opponent;

    void Awake()
    {
        opponent = GetComponent<Player>();
        ValidateActions();
    }

    private void ValidateActions()
    {
        foreach (var action in actions)
        {
            int realTotalMana = action.cards.Sum(c => c.GetManaCost());
            if (realTotalMana != action.totalMana)
                Debug.LogWarning($"Action {actions.IndexOf(action)} cards total mana is {realTotalMana} instead of {action.totalMana}");
        }
    }

    public List<Card> GetRandomAction(int mana)
    {
        var possibleActions = actions.FindAll(a => a.totalMana == mana);
        if (possibleActions.Count == 0)
            return new List<Card>();

        int randomIndex = Random.Range(0, possibleActions.Count);

        Debug.Log($"Doing action #{randomIndex} for mana {mana}");
        return possibleActions[randomIndex].cards;
    }

    public IEnumerator DoTurn()
    {
        var usableMana = GameManager.Instance.manaSlots.manaUsed;
        foreach (Card card in GetRandomAction(usableMana))
        {
            yield return new WaitForSeconds(2f);
            opponent.PlayCard(card);
        }
        yield return new WaitForSeconds(2f);

        opponent.OnTurnEnd?.Invoke();
    }
}

[System.Serializable]
public class OpponentAction
{
    public int totalMana;
    public List<Card> cards;
}
