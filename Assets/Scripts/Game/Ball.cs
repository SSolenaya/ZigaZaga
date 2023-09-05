using DG.Tweening;
using UnityEngine;
using Zenject;
using static UnityEngine.GraphicsBuffer;

public enum BallStates
{
    wait,
    move,
    fall
}

public class Ball : MonoBehaviour
{
    [SerializeField] private BallView _ballView;
    [SerializeField] private BallBody _ballBody;
    private RoadBlock _currentRoadBlock;
    private BallStates _ballState = BallStates.wait;
    private Directions _currentDir;
    private Vector3 _currentDirectionV3;
    private float _speed;
    private bool _isCheatModeOn;
    private MainLogic _mainLogic;
    private AudioController _audioController;
    private UIWindowsManager _windowsManager;
    private GameInfoManager _gameInfoManager;
    private Vector3 _nextTurningPoint;
    private AbstractStrategyMovement _abstractStrategyMovement;


    private void Update()
    {
        if (_mainLogic.GetState() != GameStates.play)
        {
            return;
        }

        SendRay();
        Move();
    }

    public void Setup(MainLogic mainLogic, AudioController audioController, UIWindowsManager windowsManager, GameInfoManager gameInfoManager, RoadController roadController)
    {
        _mainLogic = mainLogic;
        _audioController = audioController;
        _windowsManager = windowsManager;
        _gameInfoManager = gameInfoManager;
        _currentDir = Directions.right;
        _currentDirectionV3 = Vector3.forward;
        _currentRoadBlock = roadController.GetStartingPlatformRoadBlock();
        SetTurningPoints();
        _mainLogic.SubscribeForCheatModeStateChange((newState) =>
        {
            _ballBody.SetCheatMode(newState);
            _isCheatModeOn = newState;
        }); 
        _mainLogic.SubscribeForBallSpeedChange((newSpeed) =>
        {
            _speed = newSpeed;
            _ballView.ChangeViewRotationSpeed(_speed);
        });
        ChangeSkin(mainLogic.BallMaterialsManagerSO.GetRandomSkinData());
        
        _ballBody.SubscribeForGemTrigger(OnGemCollision);
       
         _abstractStrategyMovement = _mainLogic.GameSettingsSO.IsBallRuningOnMath? new MathMovement(this) : new PhysicsMovement(this, _ballBody);
        _ballBody.Setup(_abstractStrategyMovement);

    }

    

    private void SetTurningPoints()
    {
        Vector3 vecHelper = _currentRoadBlock.GetTurningPoint();
        _nextTurningPoint = new Vector3(vecHelper.x, transform.position.y, vecHelper.z);
    }

    private void SendRay()
    {
        if (_ballState != BallStates.move)
        {
            return;
        }

        Ray ray = new Ray(transform.position, -Vector3.up);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit) && !_isCheatModeOn)
        {
           SetState(BallStates.fall);
        }
    }

    

    private void Move()
    {
        
        if (_ballState == BallStates.move)
        {
            _abstractStrategyMovement.UpdateMovement(_mainLogic, _currentDirectionV3, _nextTurningPoint);
            bool isForwardMove = _currentDir == Directions.right;
            _ballView.RotateView(isForwardMove);
        }

        if (_ballState == BallStates.fall)
        {
            Vector3 fallingDir = _currentDirectionV3 + Vector3.down;
            transform.position += fallingDir * Time.deltaTime * _speed * 3;
            if (transform.position.y < _mainLogic.GameSettingsSO.yCoordForDestroy)
            {
                _mainLogic.SetGameState(GameStates.gameOver);
            }
        }
    }

    public void SetState(BallStates newState)
    {
        if (_ballState == newState)
        {
            return;
        }

        switch (newState)
        {
            case BallStates.fall:
                _windowsManager.CloseWindow(TypeWindow.inGame);
                _audioController.PlayFailSound();
                Vector3 simulationGravity = new Vector3(_currentDirectionV3.x, _currentDirectionV3.y, _currentDirectionV3.z);
                DOVirtual.Float(1, 0, 0.5f, var => { _currentDirectionV3 = simulationGravity * var; }).SetEase(Ease.OutQuad);
                break;
            case BallStates.move:
                break;
            case BallStates.wait:
                break;
        }

        _ballState = newState;
    }

    public BallStates GetState()
    {
        return _ballState;
    }

    public void ChangeDirection()
    {
        if (_ballState == BallStates.fall)
        {
            return;
        }
        _ballView.OnBallTurning();
        _gameInfoManager.AddScore(_mainLogic.GameSettingsSO.scoreForTap);
        _currentDir = _currentDir == Directions.right ? Directions.left : Directions.right;
        _currentDirectionV3 = _currentDir == Directions.right ? Vector3.forward : Vector3.right;
    }

    public void OnReachingTurningPoint()
    {
        _currentRoadBlock = _currentRoadBlock.GetNextBlock();
        SetTurningPoints();
        ChangeDirection();
    }

    public void OnGemCollision()
    {
        _gameInfoManager.AddGem(1);
        _gameInfoManager.AddScore(_mainLogic.GameSettingsSO.scoreForGemModifier);
        _audioController.PlayGemSound();
    }


    public void ChangeSkin(BallSkinData newData)
    {
        _ballView.ChangeSkin(newData);
    }

}