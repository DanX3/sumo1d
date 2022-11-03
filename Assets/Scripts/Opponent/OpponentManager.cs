using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// mana 3
// 1. A - B - C 
// 2. B - B - C 
// 3. D - A
// 4. D - C
// 5. E
// 6. F

// mana 4
// 1. A - B - C - A
// 2. B - B - C - C
// 3. D - A - B
// 4. D - C
// 5. E - A
// 6. F - A


public class OpponentManager : MonoBehaviour
{
    public List<OpponentAction> actions;

    public List<Card> GetRandomAction(int mana)
    {
        var possibleActions = actions.FindAll(a => a.totalMana == mana);
        int randomIndex = Random.Range(0, possibleActions.Count);

        return possibleActions[randomIndex].cards;
    }
}

[System.Serializable]
public class OpponentAction
{
    public int totalMana;
    public List<Card> cards;
}
