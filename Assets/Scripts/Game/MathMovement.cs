using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathMovement : AbstractStrategyMovement
{
    private Ball _ball;
    private float lastDistanseToTurningPoint = -1f;
    private bool _canCalculateDistance = false;

    public MathMovement(Ball ball):base(ball.transform)
    {
        _ball = ball;
    }

    public override bool IsBallRunningByPysics()
    {
        return false;
    }

    protected override void CalculateMovement(MainLogic _mainLogic, Vector3 nextTurningPoint, Vector3 nextPos)
    {
        if (_mainLogic.IsCheatMode && _mainLogic.GameSettingsSO.IsBallRuningOnMath)
        {
            var currentDistance = Vector3.Distance(nextTurningPoint, nextPos);
            if (_canCalculateDistance && (currentDistance > lastDistanseToTurningPoint))
            {
                _ball.OnReachingTurningPoint();
                nextPos = nextTurningPoint;
                _canCalculateDistance = false;
                lastDistanseToTurningPoint = float.MaxValue;
            }
            else
            {
                _canCalculateDistance = true;
                lastDistanseToTurningPoint = currentDistance;
            }
        }
    }
}
