using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    [HideInInspector]
    public Player opponent;
    public OpponentSpawner opponentSpawner;
    public Button endTurnButton;
    bool isPlayerTurn;
    public int turnCounter = 0;
    public ManaSlots manaSlots;
    public UIContactPoint contactPoint;
    public int maxOpponentLevel = 10;

    private const string PLAYER_PREFS_OPPONENT_LEVEL = "OpponentLevel";

    void Start() => Init();

    public void Init()
    {
        opponentSpawner.SpawnOpponent();

        Application.targetFrameRate = 60;
        player.Init();
        opponent.Init();

        // callback setup
        player.OnTurnStart += OnPlayerStartTurn;
        player.OnTurnEnd += OnPlayerEndTurn;
        player.OnDefeat += OnPlayerLose;
        player.stats.OnPowerupBonus += CheckPlayersInArena;
        player.OnDamageDealt += (damage, criticalHit) => contactPoint.Move(damage);
        opponent.OnTurnStart += OnOpponentStartTurn;
        opponent.OnTurnEnd += OnOpponentEndTurn;
        opponent.OnDefeat += OnOpponentDefeat;
        opponent.stats.OnPowerupBonus += CheckPlayersInArena;
        opponent.OnDamageDealt += (damage, criticalHit) => contactPoint.Move(-damage);

        turnCounter = 0;
        player.OnTurnStart?.Invoke();
    }

    void CheckPlayersInArena(PowerupBonus stat, float delta)
    {
        if (player.stats.hp <= 0)
        {
            OnPlayerLose();
            return;
        }
    }

    void OnPlayerStartTurn()
    {
        Debug.Log("Player start turn");
        endTurnButton.interactable = true;
        player.deckManager.Draw(player.stats.handCount);
        manaSlots.Reset();
    }

    void OnPlayerEndTurn()
    {
        Debug.Log("Player end turn");
        player.deckManager.DiscardHand();
        endTurnButton.interactable = false;
        opponent.OnTurnStart?.Invoke();
    }

    void OnOpponentStartTurn()
    {
        Debug.Log("Opponent start turn");
        opponent.GetComponent<OpponentManager>().DoTurn();
        // opponent.OnTurnEnd?.Invoke();
    }

    void OnOpponentEndTurn()
    {
        // shrink arena
        player.stats.AlterAttribute(PowerupBonus.Arena, -5);
        opponent.stats.AlterAttribute(PowerupBonus.Arena, -5);

        Debug.Log("Opponent end turn");
        turnCounter++;

        player.OnTurnStart?.Invoke();
    }

    public void OnEndTurnPressed()
    {
        player.OnTurnEnd?.Invoke();
    }

    public void OnOpponentDefeat()
    {
        int currentOpponentLevel = PlayerPrefs.GetInt(PLAYER_PREFS_OPPONENT_LEVEL, 1) + 1;

        if (currentOpponentLevel > maxOpponentLevel)
        {
            GameManager.Instance.OnPlayerWin();
            return;
        }

        PlayerPrefs.SetInt(PLAYER_PREFS_OPPONENT_LEVEL, currentOpponentLevel);
        SceneManager.LoadScene("BattleScene");
    }

    public void OnPlayerWin()
    {
        Debug.Log("YOU WIN");
        EndGame();
    }

    void OnPlayerLose()
    {
        Debug.Log("GAME OVER");
        EndGame();
    }

    void EndGame()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_OPPONENT_LEVEL, 1);
        SceneManager.LoadScene("HomeScene");
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_OPPONENT_LEVEL, 1);
    }
#endif
}