using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    left,
    right
}

public enum BlockStates
{
    steady,
    heavy,
    inPool
}

public class RoadBlock : MonoBehaviour, ISelfDestroyable
{
    [SerializeField] public Directions _myDirection;
    [SerializeField] private GameObject _myView;
    [SerializeField]  private int _myScale = 1;
    [SerializeField] private Rigidbody _rB;
    private BlockStates _phState;
    private RoadController _roadController;

    public void Setup(int scale, RoadBlock previousBlock, RoadController rc)
    {
        _roadController = rc;
        SetPhysicState(BlockStates.steady);
        _myDirection = previousBlock._myDirection == Directions.left ? Directions.right : Directions.left;
        _myScale = scale;
        var instPos = previousBlock.GetPositionForNextBlock();
        _myView.transform.localPosition = new Vector3(0, 0.5f, _myScale/2f - 0.5f);
        _myView.transform.localScale = new Vector3(1, 1, _myScale);
        this.transform.position = instPos;
        this.transform.eulerAngles = Vector3.up * (_myDirection == Directions.left ? 0 : 90) ;
    }

    private void LateUpdate()
    {
        if (_phState == BlockStates.heavy && transform.position.y < -6)
        {
            SelfDestroy();
        }
    }

    public Vector3 GetPositionForNextBlock()
    {
        float xCoord = this.transform.position.x + (_myDirection == Directions.right ? _myScale : 0f);
        float zCoord = this.transform.position.z + (_myDirection == Directions.left ? _myScale : 0f);
        Vector3 myEndPos = new Vector3(xCoord, this.transform.position.y, zCoord);
        return myEndPos;
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ball") SetPhysicState(BlockStates.heavy);
    }

    public void SetPhysicState(BlockStates newState)
    {
        if (_phState == newState) return;
        _phState = newState;
        switch (_phState)
        {
            case BlockStates.heavy:
                _rB.useGravity = true;
                _rB.isKinematic = false;
                break;
            case BlockStates.steady:
            case BlockStates.inPool:
            default:
                _rB.useGravity = false;
                _rB.isKinematic = true;
                break;
        }
    }


    public virtual void SelfDestroy()
    {
        SetPhysicState(BlockStates.inPool);
        _roadController.DestroyBlock(this);
    }
}
