using UnityEngine;

public class DamageFromHurtLastTurn : AttackModifier
{
    public float factor = 0.5f;

    public override int GetDamageAdd(Player user) 
    {
        int turn = GameManager.Instance.turnCounter;
        Debug.Log("turn: " + (turn));
        if (turn == 0)
            return 0;

        var cardsPlayerLastTurn = user.GetOpponent().playedCardsHistory.GetTurnHistory(turn - 1);
        int res = 0;
        foreach (var card in cardsPlayerLastTurn)
            if (card.damageDealt != null)
                res += card.damageDealt.Value;
        Debug.Log("Card base damage: " + Mathf.FloorToInt(factor * res));
        return Mathf.FloorToInt(factor * res);
    }

}
