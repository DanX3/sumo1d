using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class OpponentSpawner : MonoBehaviour
{
    public UIStats opponentUiStat;
    public UIArena opponentUiArena;
    public PowerupList opponentPowerupList;
    public List<OpponentManager> opponents;
    public Transform opponentModelParent;
    public Transform spawingStartingPoint;
    public float enteringSpeed = 10f;

    private OpponentManager nextOpponent;

    public void SpawnOpponent()
    {
        nextOpponent = GameObject.Instantiate(GetNextRandomOpponent(), opponentModelParent);
        nextOpponent.transform.position = spawingStartingPoint.position;

        CameraManager.Instance.SetOpponentToFollow(nextOpponent.gameObject);
        var movingComponent = nextOpponent.gameObject.AddComponent<MoveToLocalPosition>();
        CameraManager.Instance.SetOpponentFollowCamera();
        movingComponent.Init(Vector3.zero, enteringSpeed, () => StartCoroutine(OnOpponentFinishEnter()));

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

    private IEnumerator OnOpponentFinishEnter()
    {
        yield return new WaitForSeconds(1);
        CameraManager.Instance.SetDefaultCamera();

        yield return new WaitForSeconds(1);
        nextOpponent.ShowLabels(false);

        yield return new WaitForSeconds(2);
        GameManager.Instance.StartGame();

        yield return null;
    }
}
