using System;
using UnityEngine;

public class GameInfoManager : Singleton<GameInfoManager>
{
    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            _changeCurrentScore?.Invoke(_currentScore);
        }
    }
    private Action<int> _changeCurrentScore;

    private int _currentGems;
    private int _currentScore;

    public void ResetOldInfo()
    {
        CurrentScore = 0;
        _currentGems = 0;
    }
    
    public void EndGame()
    {
        SaveManager.SetBestScore(_currentScore);
    }

    public void AddGem(int count)
    {
        _currentGems += count;
        SaveManager.SetGemsCount(count);
    }

    public void AddScore(int count)
    {
        CurrentScore += count;
    }

    public void SubscribeForScore(Action<int> act)
    {
        _changeCurrentScore += act;
    }

    private void OnApplicationQuit()
    {
        SaveManager.Save();
    }
}
