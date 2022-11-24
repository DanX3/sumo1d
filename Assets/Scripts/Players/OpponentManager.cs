using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class OpponentManager : MonoBehaviour
{
    public string opponentName;
    public string opponentDescription;
    public int opponentLevel;
    public GameObject model;

    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text levelText;

    public List<OpponentAction> actions;

    [HideInInspector]
    public Player opponent;

    void Awake()
    {
        opponent = GetComponent<Player>();

        nameText.text = opponentName;
        descriptionText.text = opponentDescription;
        levelText.text = "LEV " + opponentLevel;

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

    public void DoTurn()
    {
        StartCoroutine(DoTurnCoroutine());
    }
    
    private IEnumerator DoTurnCoroutine()
    {
        var usableMana = GameManager.Instance.manaSlots.manaUsed;
        foreach (Card card in GetRandomAction(usableMana))
        {
            yield return new WaitForSeconds(1.5f);
            opponent.PlayCard(card);
        }
        yield return new WaitForSeconds(2f);

        opponent.OnTurnEnd?.Invoke();
    }

    public void ShowLabels(bool show)
    {
        nameText.gameObject.SetActive(show);
        descriptionText.gameObject.SetActive(show);
        levelText.gameObject.SetActive(show);
    }
}

[System.Serializable]
public class OpponentAction
{
    public int totalMana;
    public List<Card> cards;
}
