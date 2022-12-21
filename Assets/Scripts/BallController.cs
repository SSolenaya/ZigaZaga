using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Ball _ball;
    [SerializeField] private Transform _parentForRoad;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ball?.ChangeDirection();
        }
    }

    public void Restart()
    {
        if (_ball == null)
        {
            _ball = Instantiate(_ballPrefab, _parentForRoad);
        }
        _ball.transform.localPosition = new Vector3(0f, 1.3f, -1.2f);
        _ball.Restart();
    }
}
