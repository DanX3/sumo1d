using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class OpponentActionsManager : MonoBehaviour
{
    public List<OpponentAction> actions;

    private Player opponent;

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
        int randomIndex = Random.Range(0, possibleActions.Count);

        Debug.Log($"Doing action #{randomIndex} for mana {mana}");

        return possibleActions[randomIndex].cards;
    }

    public void DoTurn()
    {
        StartCoroutine(DoTurnRoutine());
    }

    IEnumerator DoTurnRoutine()
    {
        var usableMana = GameManager.Instance.manaSlots.manaUsed;
        foreach (Card card in GetRandomAction(usableMana))
        {
            yield return new WaitForSeconds(1.5f);
            opponent.PlayCard(card);
        }
        opponent.OnTurnEnd?.Invoke();
    }
}

[System.Serializable]
public class OpponentAction
{
    public int totalMana;
    public List<Card> cards;
}
