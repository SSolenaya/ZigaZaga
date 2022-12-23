using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public float BoundsSize { get; private set; } //  границы экрана

    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _blockCameraTransform;
    [SerializeField] private float _speed;

    private Vector3 posCamera;

    private void Awake()
    {
        float orthoSize = _cam.orthographicSize;
        BoundsSize = orthoSize / Screen.height * Screen.width;
    }

    private void Start()
    {
        _speed = MainLogic.Inst.SO.ballSpeed;
    }

    public void ResetPosition()
    {
        UpdateCameraPos();
    }

    public Transform GetCameraTransform()
    {
        return _blockCameraTransform;
    }

    private void LateUpdate()
    {
        if (MainLogic.Inst.GetState() != GameStates.play)
        {
            return;
        }

        //MovementUpwardsImitation();
        UpdateCameraPos();
    }

    public void UpdateCameraPos()
    {
        float c = BallController.Inst.GetCoordsCenterBall();
        posCamera.x = c;
        posCamera.y = 10;
        posCamera.z = c;

        _blockCameraTransform.position = posCamera;
    }

    //private void MovementUpwardsImitation()
    //{
    //    if (BallController.Inst.GetBallStates() == BallStates.move)
    //    {
    //        Vector3 v3 = new Vector3(Mathf.Sqrt(2f), 0, Mathf.Sqrt(2f));
    //        _blockCameraTransform.position += v3 * _speed * Time.deltaTime; // перемещение камеры за мячом
    //    }
    //}
}