using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class RoadController : Singleton<RoadController>
{
    [SerializeField] private RoadBlock _roadBlockPrefab;
    [SerializeField] private DefaultRoadBlock _platformPrefab;
    [SerializeField] private DefaultRoadBlock _defaultStartingBlockPrefab;
    private DefaultRoadBlock _platformBlock;
    private DefaultRoadBlock _defaultStartingBlock;
    private RoadBlock _startingBlock;
    [SerializeField] private Transform _parentForRoad;
    private readonly int _maxScale = 6; // temp
    private int _currentBlocksAmount = 2; // temp
    private readonly int _maxAmountOfBlocks = 10;

    public List<RoadBlock> roadBlocksList = new List<RoadBlock>();


    public void Generation()
    {
        _platformBlock = Instantiate(_platformPrefab, _parentForRoad);
        _platformBlock.SetupAsDefault();
        _platformBlock.transform.localPosition = new Vector3(0f, -2f, 0f);


        _defaultStartingBlock = Instantiate(_defaultStartingBlockPrefab, _parentForRoad);

        _defaultStartingBlock.SetupAsDefault();
        _defaultStartingBlock.transform.localPosition = new Vector3(2.5f, 0f, 1.5f);
        _defaultStartingBlock._myDirection = Directions.right;
        _defaultStartingBlock._myScale = 2;


        _startingBlock = _defaultStartingBlock;
        GenerateRoad(_maxAmountOfBlocks);
    }

    public void Clear()
    {
        while (true)
        {
            foreach (RoadBlock roadBlock in roadBlocksList)
            {
                roadBlock.SelfDestroy();
                break;
            }

            if (roadBlocksList.Count == 0)
            {
                break;
            }
        }
       

        roadBlocksList.Clear();

        if (_platformBlock != null)
        {
            _platformBlock.SelfDestroy();
        }

        if (_defaultStartingBlock != null)
        {
            _defaultStartingBlock.SelfDestroy();
        }
        
    }


    public void GenerateRoad(int blocksNumber)
    {
        for (int i = 1; i <= blocksNumber; i++)
        {
            RoadBlock block = PoolManager.GetRoadBlockFromPull(_roadBlockPrefab);
            block.transform.SetParent(_parentForRoad);
            Vector3 startBlockPos = _startingBlock.GetPositionForNextBlock();

            float centerXZ = (startBlockPos.x + startBlockPos.z) / 2f; //  позиция предполагаемого центра экрана, а также объекта камеры на уровне рассматриваемого блока

            float dis = Mathf.Sqrt(Mathf.Pow(centerXZ - startBlockPos.x, 2) + Mathf.Pow(centerXZ - startBlockPos.z, 2)); //  расстояние между точкой, откуда нужно начать генерацию блока и предполагаемым центром
            float suggestedCathet = 0;
            if (startBlockPos.x >= centerXZ && _startingBlock._myDirection == Directions.left //  вычисление размера проекции допустимого размера блока
                || startBlockPos.x <= centerXZ && _startingBlock._myDirection == Directions.right)
            {
                suggestedCathet = BallController.boundsSize - dis;
            }

            if (startBlockPos.x <= centerXZ && _startingBlock._myDirection == Directions.left
                || startBlockPos.x >= centerXZ && _startingBlock._myDirection == Directions.right)
            {
                suggestedCathet = BallController.boundsSize + dis;
            }


            int suggestedHipo = (int) (suggestedCathet * Mathf.Sqrt(2f)); //  по размеру проекци вычисляем допустимый размер нового блока
            int maxHipo = suggestedHipo > 10 ? 10 : suggestedHipo; //  случайно выбираем его размер в допустимых пределах
            int currentBlockScale = Random.Range(2, maxHipo);
            if (currentBlockScale < 2)
            {
                Debug.LogError("Wrong RoadBlock Scale = " + currentBlockScale + " hipo = " + maxHipo);
            }

            _currentBlocksAmount++;
            block.name = "Block_" + _currentBlocksAmount;
            block.Setup(currentBlockScale, _startingBlock, this);
            _startingBlock = block;
            roadBlocksList.Add(block);
            if (i == blocksNumber)
            {
                Vector3 v3c = block.GetPositionForNextBlock();
                float c = (v3c.x + v3c.z) / 2f;
                BallController.Inst._screenCenterPos = new Vector2(v3c.x, v3c.z);
            }
        }
    }

    public void SendBlockToPool(RoadBlock roadBlock)
    {
        roadBlocksList.Remove(roadBlock);
        PoolManager.PutRoadBlockToPool(roadBlock);
    }
}