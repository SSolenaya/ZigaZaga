using System;
using UnityEngine;

public static class SaveManager
{
    private static readonly string _keySaveData = "keySaveData";
    private static SaveData _saveData;

    public static void Init()
    {
        Load();
    }

    public static void SetBestScore(int newScore)
    {
        _saveData.bestScore = newScore > _saveData.bestScore ? newScore : _saveData.bestScore;
        Save();
    }

    public static int GetBestScore()
    {
        return _saveData.bestScore;
    }

    public static void SetPlayedGamesCount(int gamesAmount = 1)
    {
        _saveData.countGames++;
        Save();
    }

    public static int GetPlayedGamesCount()
    {
        return _saveData.countGames;
    }

    public static void SetGemsCount(int gemsAmount = 1)
    {
        _saveData.totalGems += gemsAmount;
        Save();
    }

    public static int GetTotalGemsCount()
    {
        return _saveData.totalGems;
    }

    public static void SetSoundState(bool var)
    {
        _saveData.isSoundOn = var;
        Save();
    }

    public static bool GetSoundState()
    {
        return _saveData.isSoundOn;
    }

    private static void Load() //  TODO load before 1st session
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

[Serializable]
public class SaveData
{
    public int bestScore;
    public int countGames;
    public int totalGems;
    public bool isSoundOn;
}