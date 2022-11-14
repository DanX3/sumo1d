using UnityEngine;

public class MulLastTurnDamage : AttackModifier
{
    public float factor = 2f;

    public override int GetDamageAdd(Player user) 
    {
        if (GameManager.Instance.turnCounter == 0)
            return 0;

        var cardsPlayerLastTurn = user.playedCardsHistory.GetTurnHistory(GameManager.Instance.turnCounter - 1);
        int res = 0;
        foreach (var card in cardsPlayerLastTurn)
            if (card.damageDealt != null)
                res += card.damageDealt.Value;
        Debug.Log("Card base damage: " + Mathf.FloorToInt(factor * res));
        return Mathf.FloorToInt(factor * res);
    }

}
