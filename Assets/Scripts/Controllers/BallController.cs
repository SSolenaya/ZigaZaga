using UnityEngine;

public class BallController : Singleton<BallController>
{
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _parentForRoad;
    private Ball _ball;
    private Vector3 coordsCenterBall = new Vector3();

    private void Start()
    {
        WindowManager.Inst.GetWindow<InGameWindow>(TypeWindow.inGame).fullScreenClickObserver.SubscribeForClick(() => {
            AudioController.Inst.PlayTapSound();
            _ball.ChangeDirection();
        });
    }

    public void BallChangeDirection()
    {
        _ball?.ChangeDirection();
    }

    public void GenerationBall()
    {
        _ball = Instantiate(_ballPrefab, _parentForRoad);
        _ball.transform.localPosition = new Vector3(0f, 1.3f, -1.2f);
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
}