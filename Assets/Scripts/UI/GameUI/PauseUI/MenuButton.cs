using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void Click()
    {
        GameObject.Find("Pipe").GetComponent<Pipe>().Reload();
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Disable);
        GameManager.ChangeMainMenuState(GameManager.MainMenuState.Menu);
    }
}
