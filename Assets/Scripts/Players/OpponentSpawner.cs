using System.Linq;
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
        OpponentManager nextOpponent = GameObject.Instantiate(GetNextRandomOpponent(), opponentModelParent);
        Player opponent = nextOpponent.opponent;
        GameManager.Instance.opponent = opponent;

        opponent.uiArena = opponentUiArena;
        opponent.uiStats = opponentUiStat;
        opponent.powerupList = opponentPowerupList;
    }

    private OpponentManager GetNextRandomOpponent()
    {
        int currentOpponentLevel = PlayerPrefs.GetInt("OpponentLevel", 1);

        var random = new System.Random();

        return opponents
            .FindAll(o => o.opponentLevel == currentOpponentLevel)
            .OrderBy(x => random.Next())
            .First();
    }
}
