using System.Collections;
using System.Collections.Generic;
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
    public CardPlayedDetail cardPlayedDetail;
    public CardDetail cardDetail;
    public Rewards rewards;
    public Canvas mainCanvas;
    public Canvas mainCanvas3D;

    private const string PLAYER_PREFS_OPPONENT_LEVEL = "OpponentLevel";

    void Awake() => Init();

    public void Init()
    {
        PlayerPrefs.DeleteKey("WIN");

        mainCanvas.gameObject.SetActive(false);

        opponentSpawner.SpawnOpponent();

        Application.targetFrameRate = 60;

        // must be executed before player Init
        var playerAttributes = LoadGame();

        player.Init(playerAttributes);
        opponent.Init(null);

        // callback setup
        player.OnTurnStart += OnPlayerStartTurn;
        player.OnTurnEnd += OnPlayerEndTurn;
        player.OnDefeat += OnPlayerLose;
        player.OnCardPlayed += OnPlayerCardPlayed;
        player.stats.OnPowerupBonus += CheckPlayersInArena;
        player.OnDamageDealt += (damage, criticalHit) => contactPoint.Move(damage);
        opponent.OnTurnStart += OnOpponentStartTurn;
        opponent.OnTurnEnd += OnOpponentEndTurn;
        opponent.OnDefeat += OnOpponentDefeat;
        opponent.stats.OnPowerupBonus += CheckPlayersInArena;
        opponent.OnDamageDealt += (damage, criticalHit) => contactPoint.Move(-damage);
    }

    public void StartGame()
    {
        mainCanvas.gameObject.SetActive(true);

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
        endTurnButton.interactable = false;
        manaSlots.Reset();
    }

    void OnPlayerCardPlayed(Card card)
    {
        if (card.GetManaCost() > 0)
            endTurnButton.interactable = true;
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

    public GameObject confetti;

    public void OnOpponentDefeat()
    {
        int currentOpponentLevel = PlayerPrefs.GetInt(PLAYER_PREFS_OPPONENT_LEVEL, 1) + 1;
        SoundManager.Instance.PlayOpponentDefeated();

        if (currentOpponentLevel > maxOpponentLevel)
        {
            GameManager.Instance.OnPlayerWin();
            return;
        }

        PlayerPrefs.SetInt(PLAYER_PREFS_OPPONENT_LEVEL, currentOpponentLevel);

        cardPlayedDetail.HideCardDetail();
        StartCoroutine(ShowRewards());
    }

    IEnumerator ShowRewards()
    {
        mainCanvas.gameObject.SetActive(false);
        mainCanvas3D.gameObject.SetActive(false);
        
        CameraManager.Instance.SetPlayerFollowCamera();

        yield return new WaitForSeconds(2f);
        confetti.SetActive(true);

        yield return new WaitForSeconds(7f);
        rewards.Init(player);
        mainCanvas.gameObject.SetActive(true);
        rewards.gameObject.SetActive(true);
    }

    public void OnPlayerWin()
    {
        PlayerPrefs.SetString("win", "true");
        EndGame();
    }

    void OnPlayerLose()
    {
        PlayerPrefs.SetString("win", "false");
        SoundManager.Instance.PlayLaughingCrowd();
        StartCoroutine(LoseCoroutine());
    }

    IEnumerator LoseCoroutine()
    {
        yield return new WaitForSeconds(9f);
        EndGame();
    }

    void ResetProgress()
    {
        PlayerPrefs.SetString("cards", JsonUtility.ToJson(new StartingCards(player.startingCards)));
        PlayerPrefs.SetString("stats", JsonUtility.ToJson(player.startingAttributes));
        PlayerPrefs.SetInt(PLAYER_PREFS_OPPONENT_LEVEL, 1);
        PlayerPrefs.Save();
    }

    void EndGame()
    {
        ResetProgress();
        SceneManager.LoadScene("HomeScene");
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_OPPONENT_LEVEL, 1);
    }
#endif

    PlayerAttributes LoadGame()
    {
        Debug.Log("Loading game");

        // first time init
        if (!PlayerPrefs.HasKey("stats"))
        {
            PlayerPrefs.SetString("stats", JsonUtility.ToJson(player.startingAttributes));
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("cards"))
        {
            Debug.Log(JsonUtility.ToJson(new StartingCards(player.startingCards)));
            PlayerPrefs.SetString("cards", JsonUtility.ToJson(new StartingCards(player.startingCards)));
            PlayerPrefs.Save();
        }

        // cards init
        var cards = JsonUtility.FromJson<StartingCards>(PlayerPrefs.GetString("cards"));
        Debug.Log(cards.cardsName.Length);
        player.deckManager.deckList.Clear();
        foreach (var cardName in cards.cardsName)
            player.deckManager.deckList.Add(rewards.GetCardByName(cardName));

        // stat init
        return JsonUtility.FromJson<PlayerAttributes>(PlayerPrefs.GetString("stats"));
    }
}