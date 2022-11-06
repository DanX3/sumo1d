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

    private int currentOpponentLevel = 1;
    private int maxOpponentLevel = 10;


    public void SpawnNextOpponent()
    {
		GameManager.Instance.RemoveCallbacks();
		GameObject.Destroy(GameManager.Instance.opponent.GetComponent<OpponentManager>().model.gameObject);
		GameObject.Destroy(GameManager.Instance.opponent.gameObject);

        currentOpponentLevel++;

        if (currentOpponentLevel > maxOpponentLevel)
        {
            GameManager.Instance.OnPlayerWin();
            return;
        }

		SpawnOpponent();
		GameManager.Instance.Init();
    }
	
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
        var possibleOpponents = opponents.FindAll(o => o.opponentLevel == currentOpponentLevel);
        int randomIndex = Random.Range(0, possibleOpponents.Count);

        return opponents[opponents.IndexOf(possibleOpponents[randomIndex])];
    }
}
