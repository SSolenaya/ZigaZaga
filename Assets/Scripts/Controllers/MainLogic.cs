using UnityEngine;

public enum GameStates
{
    none = 0,
    readyToPlay = 5,
    play = 10,
    pause = 15,
    gameOver = 20,
}

public class MainLogic : Singleton<MainLogic>
{
    public SOGameSettings SO;
    private GameStates _currentGameState = GameStates.none;
    private bool _isCheatMode;

    private void Awake()
    {
        SaveManager.Init();
        AudioController.Inst.Init();
        _isCheatMode = false;
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
                GameInfoManager.Inst.ResetOldInfo();
                RoadController.Inst.Clear();
                BallController.Inst.Clear();

                RoadController.Inst.Generation();
                BallController.Inst.GenerationBall();
                CameraController.Inst.ResetPosition();
                WindowManager.Inst.OpenWindow(TypeWindow.mainMenu);
                break;
            case GameStates.play:
                WindowManager.Inst.OpenWindow(TypeWindow.inGame);
                BallController.Inst.Play();
                break;
            case GameStates.pause:
                WindowManager.Inst.OpenWindow(TypeWindow.pause);
                break;
            case GameStates.gameOver:
                GameInfoManager.Inst.EndGame();
                WindowManager.Inst.OpenWindow(TypeWindow.gameOver);
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
        _isCheatMode = var;
    }

    public bool GetCheatModeState()
    {
        return _isCheatMode;
    }
}