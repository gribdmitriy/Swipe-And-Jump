using UnityEngine;

public static class PlayerStats
{

    private static int bestScore;
    private static int coinsCount;
    private static int sessionScore = 0;

    public static int BestScore => bestScore;
    public static int CoinsCount => coinsCount;
    public static int SessionScore => sessionScore;

    public static void AddValueToSessionScore(int value)
    {
        sessionScore += value;
    }

    public static int ResetSessionScore()
    {
        return sessionScore = 0;
    }

    public static void ChangeBestScore(int score)
    {
        bestScore = score;
    }

    public static void ChangeCoinsCount(int count)
    {
        coinsCount = count;
    }

}
