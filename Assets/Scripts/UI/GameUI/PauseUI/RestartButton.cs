using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void Click()
    {
        GameObject.Find("Pipe").GetComponent<Pipe>().Reload();
        Score.ResetScore();
        GameObject.Find("GameManagers").GetComponent<CoinsCounter>().ResetCounter();
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameplay);
    }
}
