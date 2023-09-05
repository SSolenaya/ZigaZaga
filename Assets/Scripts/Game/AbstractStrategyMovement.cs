using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStrategyMovement
{
    protected Transform _transform;

    public AbstractStrategyMovement(Transform transform)
    {
        _transform = transform;
    }

    public void UpdateMovement(MainLogic _mainLogic, Vector3 _currentDirectionV3, Vector3 nextTurningPoint)
    {
        Vector3 nextPos = _transform.position + _currentDirectionV3 * Time.deltaTime * _mainLogic.BallSpeed;
        CalculateMovement(_mainLogic, nextTurningPoint, nextPos);
        _transform.position = nextPos;
       
    }

    protected virtual void CalculateMovement(MainLogic mainLogic, Vector3 nextTurningPoint, Vector3 nextPos) { }

    public abstract bool IsBallRunningByPysics();
}
   