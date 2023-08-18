using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBody : MonoBehaviour
{
    private bool _isCheatModeOn;
    private Action _onTriggerEnterAction;
    private Action _onTriggerExitAction;

    public void SubscribeForTriggerEnter(Action action) 
    { 
    _onTriggerEnterAction = action;
    }

    public void SubscribeForTriggerExit(Action action)
    {
        _onTriggerExitAction = action;
    }

    public void SetCheatMode(bool newCheatMode)
    {
        _isCheatModeOn = newCheatMode;
    }
    public void OnTriggerEnter(Collider col)
    {
        if (_isCheatModeOn)
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
            _onTriggerExitAction?.Invoke();
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
