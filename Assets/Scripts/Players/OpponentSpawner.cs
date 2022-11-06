using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSpawner : MonoBehaviour
{
    public UIStats opponentUiStat;
    public UIArena opponentUiArena;
    public PowerupList opponentPowerupList;
    public List<OpponentManager> opponents;
    public Transform opponentModelParent;

    public void SpawnOpponent()
    {
        OpponentManager nextOpponent = GameObject.Instantiate(GetNextRandomOpponent());
        Player opponent = nextOpponent.opponent;
        GameManager.Instance.opponent = opponent;

        nextOpponent.model.gameObject.transform.SetParent(opponentModelParent, true);

        opponent.uiArena = opponentUiArena;
        opponent.uiStats = opponentUiStat;
        opponent.powerupList = opponentPowerupList;
    }


    private OpponentManager GetNextRandomOpponent()
    {
        int currentOpponentLevel = PlayerPrefs.GetInt("OpponentLevel", 1);
		Debug.Log($"Trying to spawn opponent for level {currentOpponentLevel}");

        var possibleOpponents = opponents.FindAll(o => o.opponentLevel == currentOpponentLevel);
        int randomIndex = Random.Range(0, possibleOpponents.Count);

        return opponents[opponents.IndexOf(possibleOpponents[randomIndex])];
    }
}
