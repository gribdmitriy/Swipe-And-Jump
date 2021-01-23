using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    public static int score = 0;
    private static Text text;
    
    private void Start() 
    {
        text = GetComponent<Text>();
        score = Convert.ToInt32(text.text);
    }

    public static void changeScore(int currentGainToScore)
    {
        score += currentGainToScore;
        text.text = Convert.ToString(score);
    }

    public static void ResetScore()
    {
        score = 0;
        text.text = Convert.ToString(score);
    }

    public void TransferScoreToGameovePanel()
    {
        GameObject.Find("ScoreText").GetComponent<Text>().text = text.text;
    }
}
