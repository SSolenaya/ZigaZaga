using System.Collections.Generic;
using DG.Tweening;
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
    public Directions Direction { get; set; }
    public int Scale { get; set; }

    [SerializeField] private Gem _gemPrefab;
    [SerializeField] private GameObject _view;
    [SerializeField] protected BotTrigger _botTrigger;

    private BlockStates _phState;
    private List<Gem> _gemsList = new List<Gem>();
    private Tween _tweenMove;

    public virtual void Setup(int scale, RoadBlock previousBlock)
    {
        SetPhysicState(BlockStates.steady);
        Direction = previousBlock.Direction == Directions.left ? Directions.right : Directions.left;
        Scale = scale;

        Vector3 instPos = previousBlock.GetPositionForNextBlock();
        _view.transform.localPosition = new Vector3(0, 0.5f, Scale / 2f - 0.5f);
        _view.transform.localScale = new Vector3(1, 1, Scale);

        transform.position = instPos;
        transform.eulerAngles = Vector3.up * (Direction == Directions.left ? 0 : 90);

        
        _botTrigger.transform.localPosition = new Vector3(0, 1, Scale + 0.4f);
        _botTrigger.ChangeState(false);
        InstantiatingGem();
    }
    
    private void InstantiatingGem()
    {
        _gemsList = new List<Gem>(Scale);
        int maxGems = Scale <= 2 ? 1 : Scale - 2;
        for (int i = 1; i <= maxGems; i++)
        {
            float r = Random.Range(0, 1f);
            if (r > 0.66f)
            {
                Gem _gem = PoolManager.GetGemFromPull(_gemPrefab);
                _gem.transform.SetParent(gameObject.transform);
                _gem.transform.localPosition = new Vector3(0, 1.5f, i);
                _gem.gameObject.name = "Gem_" + i + "_on_" + gameObject.name;
                _gemsList.Add(_gem);
            }
        }
    }
    
    public Vector3 GetPositionForNextBlock()
    {
        float xCoord = transform.position.x + (Direction == Directions.right ? Scale : 0f);
        float zCoord = transform.position.z + (Direction == Directions.left ? Scale : 0f);
        Vector3 myEndPos = new Vector3(xCoord, transform.position.y, zCoord);
        return myEndPos;
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
                _tweenMove = transform.DOMove(transform.position + Vector3.up * MainLogic.Inst.SO.yCoordForDestroy, 1.2f).SetEase(Ease.InQuad).OnComplete(() => {
                    SelfDestroy();
                });
                break;
            case BlockStates.steady:
            case BlockStates.inPool:
            default:
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
        _tweenMove?.Kill();
        SetPhysicState(BlockStates.inPool);
        RoadController.Inst.SendBlockToPool(this);
    }


    public virtual void SetupAsDefault() { }
}