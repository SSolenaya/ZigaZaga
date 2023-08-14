using DG.Tweening;
using UnityEngine;
using Zenject;

public enum BallStates
{
    wait,
    move,
    fall
}

public class Ball : MonoBehaviour
{
    private RoadBlock _roadBlock;
    private BallStates _ballState = BallStates.wait;
    private Directions _currentDir;
    private Vector3 _currentDirectionV3;
    private float _speed;
    private bool _isCheatModeOn;
    private MainLogic _mainLogic;
    private AudioController _audioController;
    private UIWindowsManager _windowsManager;
    private GameInfoManager _gameInfoManager;

    private void Update()
    {
        if (_mainLogic.GetState() != GameStates.play)
        {
            return;
        }

        SendRay();
        Move();
    }

    public void Setup(MainLogic mainLogic, AudioController audioController, UIWindowsManager windowsManager, GameInfoManager gameInfoManager)
    {
        _mainLogic = mainLogic;
        _audioController = audioController;
        _windowsManager = windowsManager;
        _gameInfoManager = gameInfoManager;
        _currentDir = Directions.right;
        _currentDirectionV3 = Vector3.forward;
        _mainLogic.SubscribeForCheatModeStateChange((newState) =>
        {
            _isCheatModeOn = newState;
        }); 
        _mainLogic.SubscribeForBallSpeedChange((newSpeed) =>
        {
            _speed = newSpeed;
        });
        
    }

    private void SendRay()
    {
        if (_ballState != BallStates.move)
        {
            return;
        }

        Ray ray = new Ray(transform.position, -Vector3.up);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            SetState(BallStates.fall);
        }
    }

    private void Move()
    {
        if (_ballState == BallStates.move)
        {
            transform.Translate(_currentDirectionV3 * Time.deltaTime * _speed);
        }

        if (_ballState == BallStates.fall)
        {
            Vector3 fallingDir = _currentDirectionV3 + Vector3.down;
            transform.position += fallingDir * Time.deltaTime * _speed * 3;
            if (transform.position.y < _mainLogic.SO.yCoordForDestroy)
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

        _gameInfoManager.AddScore(_mainLogic.SO.scoreForTap);
        _currentDir = _currentDir == Directions.right ? Directions.left : Directions.right;
        _currentDirectionV3 = _currentDir == Directions.right ? Vector3.forward : Vector3.right;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (_isCheatModeOn)
        {
            BotTrigger botTrigger = col.gameObject.GetComponent<BotTrigger>();
            if (botTrigger != null && !botTrigger.GetState())
            {
                ChangeDirection();
                botTrigger.ChangeState(true);
            }
        }

        Gem gem = col.gameObject.GetComponent<Gem>();

        if (gem != null)
        {
            _gameInfoManager.AddGem(1);
            _gameInfoManager.AddScore(_mainLogic.SO.scoreForGemModifier);
            _audioController.PlayGemSound();
            gem.SelfDestroy();
        }
    }

    public void OnTriggerExit(Collider col)
    {
        RoadBlock roadBlock = col.gameObject.transform.parent.GetComponent<RoadBlock>();
        if (roadBlock != null)
        {
            roadBlock.SetPhysicState(BlockStates.heavy);
        }
    }

}