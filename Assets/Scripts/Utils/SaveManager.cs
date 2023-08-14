using System;
using UnityEngine;

public static class SaveManager
{
    private static readonly string _keySaveData = "keySaveData";
    private static SaveData  _saveData;
    private static SaveData SaveData
    {
        get {
            if (_saveData == null)
            {
                Load();
            }
            return _saveData;
        }
        set {
             _saveData = value; }
    }

    public static void Init()
    {
        Load();
    }

    public static void SetBestScore(int newScore)
    {
        SaveData.bestScore = newScore > SaveData.bestScore ? newScore : SaveData.bestScore;
        Save();
    }

    public static int GetBestScore()
    {
        return SaveData.bestScore;
    }

    public static void SetPlayedGamesCount(int gamesAmount = 1)
    {
        SaveData.countGames++;
        Save();
    }

    public static int GetPlayedGamesCount()
    {
        return SaveData.countGames;
    }

    public static void SetGemsCount(int gemsAmount = 1)
    {
        SaveData.totalGems += gemsAmount;
        Save();
    }

    public static int GetTotalGemsCount()
    {
        return SaveData.totalGems;
    }

    public static void SetSoundState(bool var)
    {
        SaveData.isSoundOn = var;
        Save();
    }

    public static bool GetSoundState()
    {
        return SaveData.isSoundOn;
    }

    private static void Load() //  TODO load before 1st session
    {
        string json = PlayerPrefs.GetString(_keySaveData, "");
        SaveData = json == "" ? new SaveData() : JsonUtility.FromJson<SaveData>(json);
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(SaveData);
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