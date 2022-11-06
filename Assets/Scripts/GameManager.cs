using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Player opponent;
    public Button endTurnButton;
    public VerticalLayoutGroup playerPowerupList;
    public VerticalLayoutGroup opponentPowerupList;
    bool isPlayerTurn;
    public int turnCounter = 0;
    public ManaSlots manaSlots;
    public UIContactPoint contactPoint;

    void Start() => Init();

    public void Init()
    {
        Application.targetFrameRate = 30;
        player.Init();
        opponent.Init();

        // callback setup
        player.OnTurnStart += OnPlayerStartTurn;
        player.OnTurnEnd += OnPlayerEndTurn;
        player.OnDefeat += OnPlayerLose;
        player.OnDamageDealt += (damage, criticalHit) => contactPoint.Move(damage);
        opponent.OnTurnStart += OnOpponentStartTurn;
        opponent.OnTurnEnd += OnOpponentEndTurn;
        opponent.OnDefeat += OnPlayerWin;
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

        if (opponent.stats.hp <= 0)
            OnPlayerWin();
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
        opponent.GetComponent<OpponentActionsManager>().DoTurn();
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

    void OnPlayerWin()
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
        SceneManager.LoadScene("HomeScene");
    }
}