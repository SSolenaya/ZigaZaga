using System;
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
    private Directions _currentDir = Directions.left;
    private Vector3 _currentDirectionV3;
    [SerializeField] private float _speed;
    [SerializeField] private RoadBlock _roadBlock;
    [SerializeField] private BallStates _ballState = BallStates.move;

    private void Start()
    {
        ChangeDirection();
    }


    private void Update()
    {
        SendRay();
        Move();
    }

    private void SendRay()
    {
        if (_ballState == BallStates.fall) return;
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
            Vector3 vec = _currentDirectionV3 + Vector3.down;
            transform.position += vec * Time.deltaTime * _speed * 3;
            if (transform.position.y < -6)
            {
                MainLogic.Inst.Restart();
            }
        }
    }

    public void SetState(BallStates newState)
    {
        if (_ballState == newState) return;
        _ballState = newState;
        switch (newState)
        {
            case BallStates.fall:
                var v3 = new Vector3(_currentDirectionV3.x , _currentDirectionV3.y, _currentDirectionV3.z);
                //var v3_0 = new Vector3(0,0,0);
                DOVirtual.Float(1, 0, 0.5f, (var) => {
                    _currentDirectionV3 = v3 * var;
                }).SetEase(Ease.OutQuad);
                Debug.Log("falling !!!");
                break;
            case BallStates.move:
                break;
            case BallStates.wait:
                break;
            default:
                break;
        }
    }

    public BallStates GetState()
    {
        return _ballState;
    }

    public void ChangeDirection()
    {
        if(_ballState == BallStates.fall) return;
        _currentDir = _currentDir == Directions.right ? Directions.left : Directions.right;
        _currentDirectionV3 = _currentDir == Directions.right ? Vector3.forward : Vector3.right;
    }

    public void Restart()
    {
        SetState(BallStates.move);
    }
}