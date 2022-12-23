using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTrigger : MonoBehaviour
{
    private bool _isUsed;

    public void ChangeState(bool newState)
    {
        _isUsed = newState;
    }

    public bool GetState()
    {
        return _isUsed;
    }
}
