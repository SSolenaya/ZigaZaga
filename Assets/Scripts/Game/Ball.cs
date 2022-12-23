using DG.Tweening;
using UnityEngine;

public enum BallStates
{
    wait,
    move,
    fall
}

public class Ball : MonoBehaviour
{
    private Directions _currentDir;
    private Vector3 _currentDirectionV3;
    [SerializeField] private float _speed;
    [SerializeField] private RoadBlock _roadBlock;
    [SerializeField] private BallStates _ballState = BallStates.wait;

    private void Start()
    {
        _currentDir = Directions.right;
        _currentDirectionV3 = Vector3.forward;
    }
    
    private void Update()
    {
        if (MainLogic.Inst.GetState() != GameStates.play)
        {
            return;
        }
        SendRay();
        Move();
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
            if (transform.position.y < -6)
            {
                MainLogic.Inst.SetGameState(GameStates.gameOver);
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
                Vector3 simulationGravity = new Vector3(_currentDirectionV3.x, _currentDirectionV3.y, _currentDirectionV3.z);
                AudioController.Inst.PlayFailSound();
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

        _currentDir = _currentDir == Directions.right ? Directions.left : Directions.right;
        _currentDirectionV3 = _currentDir == Directions.right ? Vector3.forward : Vector3.right;
    }
}