using UnityEngine;
using Zenject;

public class BallController : MonoBehaviour
{
    [Inject] private UIWindowsManager _windowsManager;
    [Inject] private MainLogic _mainLogic;
    [Inject] private RoadController _roadController;
    [Inject] private GameObjParentManager _parentManager;
    [Inject] private AudioController _audioController;
    [Inject] private GameInfoManager _gameInfoManager;
    [SerializeField] private Ball _ballPrefab;
    private Ball _ball;
    private Vector3 coordsCenterBall = new Vector3();
    

    private void Start()
    {
        var _inGameWin = _windowsManager.GetWindow<InGameWindow>(TypeWindow.inGame);
        _inGameWin.fullScreenClickObserver.SubscribeForClick(() => {
            if (_mainLogic.GetCheatModeState()) return;
            _audioController.PlayTapSound();
            BallChangeDirection();
        });
    }

    public void BallChangeDirection()
    {
        _ball?.OnReachingTurningPoint();
    }

    public void GenerationBall()
    {
        _ball = Instantiate(_ballPrefab, _parentManager.parentForRoad);
        _ball.transform.localPosition = new Vector3(0f, 1.3f, -1.2f);
        _ball.Setup(_mainLogic, _audioController, _windowsManager, _gameInfoManager, _roadController);
        _ball.SetState(BallStates.wait);
    }

    public float GetCoordsCenterBall()
    {
        coordsCenterBall = _ball.transform.position;
        return (coordsCenterBall.x + coordsCenterBall.z) * 0.5f;
    }

    public void Play()
    {
        _ball.SetState(BallStates.move);
    }

    public BallStates GetBallStates()
    {
        return _ball.GetState();
    }

    public void Clear()
    {
        Destroy(_ball?.gameObject);
    }

    public void ChangeBallSkin(BallSkinData newData)
    {
        _ball.ChangeSkin(newData);
    }
}