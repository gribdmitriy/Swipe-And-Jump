﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void Click()
    {
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Pause);
    }
}
