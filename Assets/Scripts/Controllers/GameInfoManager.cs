using System;
using UnityEngine;

public class GameInfoManager : Singleton<GameInfoManager>
{
    private int currentGems;
    private int _currentScore;
    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            changeCurrentScore?.Invoke(_currentScore);
        }
    }
    private Action<int> changeCurrentScore;

    private void Start()
    {
        SaveManager.Init();
    }

    public void ResetOldInfo()
    {
        CurrentScore = 0;
        currentGems = 0;
    }
    
    public void EndGame()
    {
        SaveManager.SetBestScore(_currentScore);
    }

    public void AddGem(int count)
    {
        currentGems += count;
        SaveManager.SetGemsCount(currentGems);          //  TODO on app exit
    }

    public void AddScore(int count)
    {
        CurrentScore += count;
        SaveManager.SetBestScore(_currentScore);          //  TODO on app exit
    }

    public void SubscribeForScore(Action<int> act)
    {
        changeCurrentScore += act;
    }
}
