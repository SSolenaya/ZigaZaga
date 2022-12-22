using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager
{
    private static string _keyForGamesCount = "GamesPlayed";
    private static string _keyForTotalGemsAmount = "TotalGems";
    private static string _keyForBestScore = "BestScore";

    public static void IncrementGamesCounter()
    {
        int gamesPlayed = PlayerPrefs.GetInt(_keyForGamesCount, 0);
        gamesPlayed++;
        PlayerPrefs.SetInt(_keyForGamesCount, gamesPlayed);
        PlayerPrefs.Save();
    }

    public static int GetGamesNumber()
    {
        int result = PlayerPrefs.GetInt(_keyForGamesCount, 0);
        return result;
    }

    public static void IncreaseTotalGemsCounter(int addGemsAmount)
    {
        int gemsTotal = PlayerPrefs.GetInt(_keyForTotalGemsAmount, 0);
        gemsTotal += addGemsAmount;
        PlayerPrefs.SetInt(_keyForTotalGemsAmount, gemsTotal);
        PlayerPrefs.Save();
    }

    public static int GetTotalScore()
    {
        int result = PlayerPrefs.GetInt(_keyForTotalGemsAmount, 0);
        return result;
    }

    public static void ChangeBestScore(int newBestScore)
    {
        int currentBestScore = PlayerPrefs.GetInt(_keyForBestScore, 0);
        int score = newBestScore > currentBestScore ? newBestScore : currentBestScore;
        PlayerPrefs.SetInt(_keyForBestScore, score);
        PlayerPrefs.Save();
    }

    public static int GetBestScore()
    {
        int result = PlayerPrefs.GetInt(_keyForBestScore, 0);
        return result;
    }

}
