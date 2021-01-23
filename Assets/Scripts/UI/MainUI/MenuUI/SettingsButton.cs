using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public void Click()
    {
        GameManager.ChangeMainMenuState(GameManager.MainMenuState.Settings);
    }
}
