using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBody : MonoBehaviour
{
    private bool _isCheatModeOn;
    private bool _isBallOnMath;
    private Action _onTriggerEnterAction;
    private Action _onGemTriggerAction;

    public void SubscribeForTriggerEnter(Action action) 
    { 
    _onTriggerEnterAction = action;
    }

    public void SubscribeForGemTrigger(Action action)
    {
        _onGemTriggerAction = action;
    }

    public void SetCheatMode(bool newCheatMode)
    {
        _isCheatModeOn = newCheatMode;
    }

    public void SetBallMovementMode(bool isBallMovedByMath)
    {
        _isBallOnMath = isBallMovedByMath;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (_isCheatModeOn && !_isBallOnMath)                                   // ball is running on physics
        {
            BotTrigger botTrigger = col.gameObject.GetComponent<BotTrigger>();
            if (botTrigger != null && !botTrigger.GetState())
            {
                _onTriggerEnterAction?.Invoke();
                botTrigger.ChangeState(true);
            }
        }

        Gem gem = col.gameObject.GetComponent<Gem>();

        if (gem != null)
        {
            _onGemTriggerAction?.Invoke();
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