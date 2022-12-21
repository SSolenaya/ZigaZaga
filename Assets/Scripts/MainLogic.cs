using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    play,
    pause
}

public class MainLogic : Singleton<MainLogic>
{
    [SerializeField] private RoadController _roadController;
    [SerializeField] private BallController _ballController;
    [SerializeField] private PoolManager _poolManager;
    private GameStates _currentGameState;


    void Start()
    {
        _poolManager.Init();
        Restart();
    }

    public void Restart()
    {
        _roadController.Restart();
        _ballController.Restart();
    }

    public void SetGameState(GameStates newState)
    {
        if(_currentGameState == newState) return;
        _currentGameState = newState;
        switch (_currentGameState)
        {
            
        }
    }
    

}
