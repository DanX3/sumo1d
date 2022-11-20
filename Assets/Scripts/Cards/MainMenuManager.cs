using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TMPro.TMP_Text winText;
    public TMPro.TMP_Text loseText;
    public TMPro.TMP_Text startGameButtonText;


    public void StartGame()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Start()
    {
        string hasPlayerWin = PlayerPrefs.GetString("win");

        if (hasPlayerWin == "true")
        {
            winText.gameObject.SetActive(true);
            loseText.gameObject.SetActive(false);
        }
        else if (hasPlayerWin == "false")
        {
            winText.gameObject.SetActive(false);
            loseText.gameObject.SetActive(true);
        }
    }
}
