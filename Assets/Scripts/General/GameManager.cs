using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GlobalState gs;
    private Text GlobalCoinsCount;
    private static Dictionary<string, GameObject> MainMenu = new Dictionary<string, GameObject>();
    private static Dictionary<string, GameObject> Game = new Dictionary<string, GameObject>();
    static GameObject x2UI;

    private void Awake()
    {
        x2UI = GameObject.Find("X2UI");
        GlobalCoinsCount = GameObject.Find("GlobalCoinsCount").GetComponent<Text>();
        if (!PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", 0);
        }

        if(!PlayerPrefs.HasKey("bestScore"))
        {
            PlayerPrefs.SetInt("bestScore", 0);
        }

        GlobalCoinsCount.text = System.Convert.ToString(PlayerPrefs.GetInt("coins"));
    }

    private void Start()
    {
        MainMenu.Add("Menu", GameObject.Find("MenuUI"));
        MainMenu.Add("Settings", GameObject.Find("SettingsUI"));
        MainMenu.Add("Leaderboard", GameObject.Find("LeaderboardUI"));
        MainMenu.Add("Shop", GameObject.Find("UpgradesUI"));

        Game.Add("Gameplay", GameObject.Find("GamePlayUI"));
        Game.Add("Pause", GameObject.Find("PauseUI"));
        Game.Add("Gameover", GameObject.Find("GameOverUI"));
        Game.Add("Advertising", GameObject.Find("AdvertisingUI"));

        ChangeGamePlayState(GamePlayState.Disable);
        ChangeMainMenuState(MainMenuState.Menu);
    }

    public static void ChangeMainMenuState(MainMenuState mainMenuState)
    {
        switch (mainMenuState)
        {
            case MainMenuState.Menu:
                MainMenu["Menu"].SetActive(true);
                MainMenu["Settings"].SetActive(false);
                MainMenu["Leaderboard"].SetActive(false);
                MainMenu["Shop"].SetActive(false);
                gs = GlobalState.MainMenu;
                break;
            case MainMenuState.Settings:
                MainMenu["Menu"].SetActive(false);
                MainMenu["Settings"].SetActive(true);
                MainMenu["Leaderboard"].SetActive(false);
                MainMenu["Shop"].SetActive(false);
                gs = GlobalState.MainMenu;
                break;
            case MainMenuState.Leaderboards:
                MainMenu["Menu"].SetActive(false);
                MainMenu["Settings"].SetActive(false);
                MainMenu["Leaderboard"].SetActive(true);
                MainMenu["Shop"].SetActive(false);
                gs = GlobalState.MainMenu;
                break;
            case MainMenuState.Shop:
                MainMenu["Menu"].SetActive(false);
                MainMenu["Settings"].SetActive(false);
                MainMenu["Leaderboard"].SetActive(false);
                MainMenu["Shop"].SetActive(true);
                gs = GlobalState.MainMenu;
                break;
            default:
                MainMenu["Menu"].SetActive(false);
                MainMenu["Settings"].SetActive(false);
                MainMenu["Leaderboard"].SetActive(false);
                MainMenu["Shop"].SetActive(false);
                gs = GlobalState.Game;
                break;
        }
    }

    public static void ChangeGamePlayState(GamePlayState gamePlayState)
    {
       
        switch (gamePlayState)
        {
            case GamePlayState.Gameplay:
                Game["Gameplay"].SetActive(true);
                Game["Pause"].SetActive(false);
                Game["Gameover"].SetActive(false);
                Game["Advertising"].SetActive(false);
                Game["Gameplay"].GetComponent<Animator>().Play("ShowConcentration");
                gs = GlobalState.Game;
                Time.timeScale = 1;
                break;
            case GamePlayState.Pause:
                Game["Gameplay"].SetActive(true);
                Game["Pause"].SetActive(true);
                Game["Gameover"].SetActive(false);
                Game["Advertising"].SetActive(false);
                Time.timeScale = 0;
                gs = GlobalState.Game;
                break;
            case GamePlayState.Gameover:
                Game["Gameplay"].SetActive(true);
                Game["Pause"].SetActive(false);
                Game["Gameover"].SetActive(true);
                Game["Advertising"].SetActive(true);
                Game["Gameover"].GetComponent<Animator>().Play("IdlePanelTop");
                GameObject.Find("AdvertisingUI").GetComponent<AdvertisingUI>().timeLeft = 5;
                GameObject.Find("TimerA").GetComponent<Image>().fillAmount = 1;
     
                gs = GlobalState.Game;
                Time.timeScale = 1;
                break;
            default:
                Game["Gameplay"].SetActive(false);
                Game["Pause"].SetActive(false);
                Game["Gameover"].SetActive(false);
                Game["Advertising"].SetActive(false);
                Time.timeScale = 1;
                gs = GlobalState.MainMenu;
                break;
        }
    }

    public static void SwitchGameOver()
    {
        Game["Gameover"].SetActive(true);
        Game["Gameover"].GetComponent<Animator>().Play("ShowPanel");
        Game["Gameplay"].GetComponent<Animator>().Play("HideConcentration");
        Game["Advertising"].SetActive(false);
    }

    public static void DisableX2UI()
    {
        x2UI.GetComponent<X2UIController>().ResetUI();
        x2UI.SetActive(false);
    }

    public static void PickUpX2()
    {
        x2UI.SetActive(true);
        x2UI.GetComponent<X2UIController>().PickUpX2();
        if(!x2UI.GetComponent<X2UIController>().isStarted) 
            x2UI.GetComponent<X2UIController>().StartTimer();
    }

    public enum GlobalState
    {
        MainMenu,
        Game
    }

    public enum MainMenuState
    {
        Menu,
        Leaderboards,
        Settings,
        Shop,
        Disable
    }

    public enum GamePlayState
    {
        Gameplay,
        Pause,
        Gameover,
        Disable
    }
}