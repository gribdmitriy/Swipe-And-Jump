using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverContinueButton : MonoBehaviour
{

    public void Click()
    {
        GameObject.Find("Pipe").GetComponent<Pipe>().Reload();
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameplay);
        
    }


}
