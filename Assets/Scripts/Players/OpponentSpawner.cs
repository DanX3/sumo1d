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
    public Transform startingPoint;
    public float enterSpeed = 0.5f;

    private OpponentManager nextOpponent;

    public void SpawnOpponent()
    {
        nextOpponent = GameObject.Instantiate(GetNextRandomOpponent(), opponentModelParent);
        //nextOpponent = GameObject.Instantiate(GetNextRandomOpponent(), opponentModelParent);
        //nextOpponent.transform.position = startingPoint.position;

        //CameraManager.Instance.SetOpponentFollowCamera();

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

    //private void Update() // TODO farlo diventare una coroutine
    //{
    //    if (nextOpponent.transform.localPosition.x >= 0)
    //        nextOpponent.transform.Translate(-nextOpponent.transform.right * enterSpeed);
    //    else
    //        CameraManager.Instance.SetDefaultCamera();
    //}
}
