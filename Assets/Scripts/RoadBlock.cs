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

public class RoadBlock : MonoBehaviour
{
    [SerializeField] private Gem _gemPrefab;
    public List<Gem> _gemsList;
    [SerializeField] public Directions _myDirection;
    [SerializeField] private GameObject _myView;
    [SerializeField] public int _myScale = 1;
    [SerializeField] private Rigidbody _rB;
    private BlockStates _phState;
    private RoadController _roadController;

    public virtual void Setup(int scale, RoadBlock previousBlock, RoadController rc)
    {
        _roadController = rc;
        SetPhysicState(BlockStates.steady);
        _myDirection = previousBlock._myDirection == Directions.left ? Directions.right : Directions.left;
        _myScale = scale;
        Vector3 instPos = previousBlock.GetPositionForNextBlock();
        _myView.transform.localPosition = new Vector3(0, 0.5f, _myScale / 2f - 0.5f);
        _myView.transform.localScale = new Vector3(1, 1, _myScale);
        transform.position = instPos;
        transform.eulerAngles = Vector3.up * (_myDirection == Directions.left ? 0 : 90);
        InstantiatingGem();
    }


    private void InstantiatingGem()
    {
        _gemsList = new List<Gem>(_myScale);
        int maxGems = _myScale <= 2 ? 1 : _myScale - 2;
        for (int i = 1; i <= maxGems; i++)
        {
            float r = Random.Range(0, 1f);
            if (r > 0.66f)
            {
                Gem _gem = PoolManager.GetGemFromPull(_gemPrefab);
                _gem.transform.SetParent(gameObject.transform);
                float z = Random.Range(0, _myScale - 1);
                _gem.transform.localPosition = new Vector3(0, 1.5f, i);
                _gem.gameObject.name = gameObject.name + "_Gem_" + i;
                if (i > maxGems)
                {
                    Debug.LogError("Gems positioning Error: maxGems = " + maxGems + " i = " + i); //  temp
                }

                _gemsList.Add(_gem);
            }
        }
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
        float xCoord = transform.position.x + (_myDirection == Directions.right ? _myScale : 0f);
        float zCoord = transform.position.z + (_myDirection == Directions.left ? _myScale : 0f);
        Vector3 myEndPos = new Vector3(xCoord, transform.position.y, zCoord);
        return myEndPos;
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ball")
        {
            SetPhysicState(BlockStates.heavy);
        }
    }

    public void SetPhysicState(BlockStates newState)
    {
        if (_phState == newState)
        {
            return;
        }


        switch (newState)
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

        _phState = newState;
    }


    public virtual void SelfDestroy()
    {
        if (_gemsList != null)
        {
            foreach (Gem gem in _gemsList)
            {
                gem.SelfDestroy();
            }

            _gemsList.Clear();
        }

        SetPhysicState(BlockStates.inPool);
        _roadController.SendBlockToPool(this);
    }


    public virtual void SetupAsDefault() { }
}