using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private static int bestScore;
    private static int coinsCount;
    private static int sessionScore = 0;

    public static int BestScore => bestScore;
    public static int CoinsCount => coinsCount;
    public static int SessionScore => sessionScore;

    public static void ChangeBestScore(int score)
    {
        bestScore = score;
    }

    public static void ChangeCoinsCount(int count)
    {
        coinsCount = count;
    }

    public static void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetInt("coins", coinsCount);
        PlayerPrefs.SetInt("bestScore", bestScore);
    }

}
