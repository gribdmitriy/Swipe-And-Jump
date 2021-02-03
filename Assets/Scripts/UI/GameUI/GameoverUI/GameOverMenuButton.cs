using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuButton : MonoBehaviour
{
    private Text GlobalCoinsCount;
    private Text SessionCoinsCount;

    private void Awake()
    {
        GlobalCoinsCount = GameObject.Find("GlobalCoinsCount").GetComponent<Text>();
        SessionCoinsCount = GameObject.Find("CoinsCounter").GetComponent<Text>();
    }


    public void Click()
    {
        GameObject.Find("Pipe").GetComponent<Pipe>().Reload();
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Disable);
        GameManager.ChangeMainMenuState(GameManager.MainMenuState.Menu);

        if (PlayerPrefs.GetInt("bestScore") < Score.score)
        {
            PlayerPrefs.SetInt("bestScore", Score.score);
        }

        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + System.Convert.ToInt32(SessionCoinsCount.text));
        GlobalCoinsCount.text = System.Convert.ToString(PlayerPrefs.GetInt("coins"));

        Score.ResetScore();
        GameObject.Find("GameManagers").GetComponent<CoinsCounter>().ResetCounter();
    }
}
