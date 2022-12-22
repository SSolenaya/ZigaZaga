using UnityEngine;

public class BallController : Singleton<BallController>
{
    [SerializeField] private Ball _ballPrefab;
    private Ball _ball;
    [SerializeField] private Transform _parentForRoad;
    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _blockCameraTransform;
    public static float boundsSize; //  границы экрана
    public Vector2 _screenCenterPos;

    private void Awake()
    {
        float orthoSize = _cam.orthographicSize;
        boundsSize = orthoSize / Screen.height * Screen.width;
    }

    public void Start()
    {
        UIController.Inst._clickInPlayObserver.SubscribeForClick(() => {
            _ball.ChangeDirection();
        });
    }

    public void BallChangeDirection()
    {
        _ball?.ChangeDirection();
    }

    public void GenerationBall()
    {        
        _blockCameraTransform.position = new Vector3(0, 10f, 0);

        _ball = Instantiate(_ballPrefab, _parentForRoad);
        _ball.transform.localPosition = new Vector3(0f, 1.3f, -1.2f);
        _ball.SetState(BallStates.wait);
    }

    public void Play()
    {
        _ball.SetState(BallStates.move);
    }

    private void Update()
    {
        MovementUpwardsImitation();
    }

    private void MovementUpwardsImitation()
    {
        if (_ball.GetState() == BallStates.move)
        {
            Vector3 v3 = new Vector3(Mathf.Sqrt(2f), 0, Mathf.Sqrt(2f));
            _blockCameraTransform.position += v3 * Time.deltaTime; // перемещение камеры за мячом
            float delta = Mathf.Sqrt(Mathf.Pow(_screenCenterPos.x - _blockCameraTransform.position.x, 2) + Mathf.Pow(_screenCenterPos.y - _blockCameraTransform.position.z, 2));
            if (delta < 30)
            {
               RoadController.Inst.GenerateRoad(1);
            }
        }
    }

    public void Clear()
    {
        Destroy(_ball.gameObject);
    }
}