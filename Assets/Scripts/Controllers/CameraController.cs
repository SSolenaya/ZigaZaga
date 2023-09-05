using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{ 
    public float BoundsSize { get; private set; } //  границы экрана

    [Inject] private MainLogic _mainLogic;
    [Inject] private BallController _ballController;
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
        _speed = _mainLogic.GetCurrentBallSpeed();
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
        if (_mainLogic.GetState() != GameStates.play)
        {
            return;
        }

        //MovementUpwardsImitation();
        UpdateCameraPos();
    }

    public void UpdateCameraPos()
    {
        float c = _ballController.GetCoordsCenterBall();
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