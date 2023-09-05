using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : AbstractStrategyMovement
{
    public PhysicsMovement(Ball ball, BallBody ballBody) : base(ball.transform)
    {
        ballBody.SubscribeForTriggerEnter(ball.OnReachingTurningPoint);         //  TODO:
    }

    public override bool IsBallRunningByPysics()
    {
        return true;
    }
}
