using System;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private int _currentScore;

    private int currentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            changeCurrentScore?.Invoke(_currentScore);
        }
    }

    private int currentGems;

    public Action<int> changeCurrentScore;

    public void StartNewGame()
    {
        currentScore = 0;
        currentGems = 0;
    }
    
    public void EndGame()
    {

    }

    public void AddGem(int count)
    {
        currentGems += count;
    }

    public void AddScore(int count)
    {
        currentScore += count;
    }



}


public class SaveData
{
    public int bestScore;
    public int countGames;
    public int countGems;
}