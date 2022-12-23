using UnityEngine;

public static class SaveManager
{
    private static readonly string _keySaveData = "keySaveData";
    private static SaveData _saveData;

    public static void SetBestScore(int newScore)
    {
        _saveData.bestScore = newScore > _saveData.bestScore ? newScore : _saveData.bestScore;
        Save();
    }

    public static int GetBestScore()
    {
        int result = 0;
        if (_saveData != null)
        {
            result = _saveData.bestScore;
        }
        return result;
    }

    public static void SetPlayedGamesCount(int gamesAmount = 1)
    {
        _saveData.countGames++;
        Save();
    }

    public static int GetPlayedGamesCount()
    {
        int result = 0;
        if (_saveData != null)
        {
            result = _saveData.countGames;
        }
        return result;
    }

    public static void SetGemsCount(int gemsAmount = 1)
    {
        _saveData.totalGems += gemsAmount;
        Save();
    }

    public static int GetTotalGemsCount()
    {
        int result = 0;
        if (_saveData != null)
        {
            result = _saveData.totalGems;
        }
        return result;
    }

    public static void Init()
    {
        Load();
    }

    private static void Load()      //  TODO load before 1st session
    {
        string json = PlayerPrefs.GetString(_keySaveData, "");
        _saveData = json == "" ? new SaveData() : JsonUtility.FromJson<SaveData>(json);
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString(_keySaveData, json);
    }
}


public class SaveData
{
    public int bestScore;
    public int countGames;
    public int totalGems;
}