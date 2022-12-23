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
    [SerializeField] private RoadController _roadController;
    [SerializeField] private PoolManager _poolManager;

    private GameStates _currentGameState = GameStates.none;


    private void Awake()
    {
        _poolManager.Init();
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
                ClearSession();
                _roadController.Generation();
                BallController.Inst.GenerationBall();
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

    private void ClearSession()
    {
        _roadController.Clear();
        BallController.Inst.Clear();
    }

    public GameStates GetState()
    {
        return _currentGameState;
    }

   //private void GameSessionFailed()
   //{
   //    if (GetState() == GameStates.pause)
   //    {
   //        return;
   //    }
   //
   //    SetGameState(GameStates.pause);
   //    _roadController.Generation();
   //    BallController.Inst.GenerationBall();
   //}
}