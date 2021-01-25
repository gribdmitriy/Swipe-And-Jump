using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void Click()
    {
        GameObject.Find("Pipe").GetComponent<Pipe>().Reload();
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameplay);
        Score.ResetScore();
        GameObject.Find("GameManagers").GetComponent<CoinsCounter>().ResetCounter();
    }
}
