using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    public static Text bestScore;

    private void Start()
    {
        bestScore = GetComponent<Text>();

        if (!PlayerPrefs.HasKey("BestScore"))
            bestScore.text = "0";
        else
            bestScore.text = PlayerPrefs.GetString("BestScore");
    }

    public static void SaveToStoreBestScore(string bestScore)
    {
        PlayerPrefs.SetString("BestScore", bestScore);
    }
}