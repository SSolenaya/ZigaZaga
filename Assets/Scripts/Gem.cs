using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, ISelfDestroyable
{
    private void OnTriggerEnter(Collider col)
    {

    }

    public void SelfDestroy()
    {
        PoolManager.PutGemToPool(this);
    }
}
