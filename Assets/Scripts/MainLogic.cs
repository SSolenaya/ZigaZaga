using Assets.Scripts;
using UnityEngine;

public enum GameStates
{
    none,
    play,
    pause
}

public class MainLogic : Singleton<MainLogic>
{
    [SerializeField] private RoadController _roadController;
    [SerializeField] private PoolManager _poolManager;

    private GameStates _currentGameState = GameStates.none;


    private void Awake()
    {
        _poolManager.Init();
        UIController.Inst.Init();
    }

    private void Start()
    {
        SetGameState(GameStates.none);
        _roadController.Generation();
        BallController.Inst.GenerationBall();
       
    }

    public void Play()
    {
        BallController.Inst.Play();
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
            case GameStates.play:
                Play();
                break;
            case GameStates.pause:
                ClearSession();
                break;

            default:
                Debug.LogError("Cannot set new state for Main logic: " + _currentGameState);
                break;
        }

        _currentGameState = newState;
    }

    private void ClearSession()
    {
        _roadController.Clear();
        BallController.Inst.Clear();
        //UserProgressManager.Inst.Clear();
    }

    public GameStates GetState()
    {
        return _currentGameState;
    }

    public void GameSessionFailed()
    {
        if (GetState() == GameStates.pause)
        {
            return;
        }
        //UserProgressManager.Inst.SaveCurrentProgress();
        SetGameState(GameStates.pause);
        UIController.Inst.ShowMenu();
        _roadController.Generation();
        BallController.Inst.GenerationBall();
    }
}