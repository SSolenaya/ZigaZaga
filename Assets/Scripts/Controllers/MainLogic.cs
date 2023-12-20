using System;
using UnityEngine;
using Zenject;

public enum GameStates
{
    none = 0,
    readyToPlay = 5,
    play = 10,
    pause = 15,
    gameOver = 20,
}

public class MainLogic : MonoBehaviour
{
    [Inject] private UIWindowsManager _windowsManager; 
    [Inject] private CameraController _cameraController;
    [Inject] private BallController _ballController;
    [Inject] private RoadController _roadController;
    [Inject] private AudioController _audioController;
    [Inject] private GameInfoManager _gameInfoManager;
    public SOGameSettings GameSettingsSO;
    public BallMaterialsManager BallMaterialsManagerSO;
    private GameStates _currentGameState = GameStates.none;
    private Action<bool> OnCheatModeStateChange;
    private Action<float> OnBallSpeedChange;
    private float _scoreForGem;
    private bool _isCheatMode;
    public bool IsCheatMode
    {
        get { return _isCheatMode;}
        set { _isCheatMode = value;
              OnCheatModeStateChange?.Invoke(_isCheatMode);
            }
    }
    private float _ballSpeed = 1;
    public float BallSpeed
    {
        get { return _ballSpeed; }
        set
        {
            _ballSpeed = value;
            OnBallSpeedChange?.Invoke(_ballSpeed);
        }
    }



    private void Awake()
    {
        SaveManager.Init();
        _audioController.Init();
        IsCheatMode = false;
        SetupBallSettings(1);
    }

    private void Start()
    {
        SetGameState(GameStates.readyToPlay);
    }

    public void SetGameState(GameStates newState)
    {
        if (_currentGameState == newState)
        {
            return;
        }

        switch (newState)
        {
            case GameStates.none:
                break;
            case GameStates.readyToPlay:
                _gameInfoManager.ResetOldInfo();
                _roadController.Clear();
                _ballController.Clear();
                _roadController.Generation();
                _ballController.GenerationBall();
                SetupBallSettings(BallSpeed);
                _cameraController.ResetPosition();
                _windowsManager.OpenWindow(TypeWindow.mainMenu);
                break;
            case GameStates.play:
                _windowsManager.OpenWindow(TypeWindow.inGame);
                _ballController.Play();
                break;
            case GameStates.pause:
                _windowsManager.OpenWindow(TypeWindow.pause);
                break;
            case GameStates.gameOver:
                _gameInfoManager.EndGame();
                _windowsManager.OpenWindow(TypeWindow.gameOver);
                break;

            default:
                Debug.LogError("Cannot set new state for Main logic: " + _currentGameState);
                break;
        }

        _currentGameState = newState;
    }

    public GameStates GetState()
    {
        return _currentGameState;
    }

    public void SetCheatMode(bool var)
    {
        IsCheatMode = var;
    }

    public bool GetCheatModeState()
    {
        return IsCheatMode;
    }

    public void SubscribeForCheatModeStateChange(Action<bool> act)
    {
        OnCheatModeStateChange += act;
    }

    public void SetupBallSettings(float ballSpeed)
    {
        BallSpeed = ballSpeed;
       _scoreForGem = GameSettingsSO.scoreForGemModifier * BallSpeed;
    }

    public float GetCurrentScoreForGem()
    {
        return _scoreForGem;
    }

    public float GetCurrentBallSpeed()
    {
        return BallSpeed;
    }

    public void SubscribeForBallSpeedChange(Action<float> act)
    {
        OnBallSpeedChange += act;
    }

    public void ChangeBallSkin(BallSkinData newData)
    {
        _audioController.PlayClickSound();
        _ballController.ChangeBallSkin(newData);
        
    }
}