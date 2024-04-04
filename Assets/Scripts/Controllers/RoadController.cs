using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Zenject;

public class RoadController : MonoBehaviour
{
    [Inject] private GameObjParentManager _parentManager;
    [Inject] private CameraController _cameraController;
    [Inject] private MainLogic _mainLogic;
    [Inject] private BallController _ballController;
    [SerializeField] private RoadBlock _roadBlockPrefab;
    [SerializeField] private StartingPlatformBlock _platformPrefab;
    [SerializeField] private StartingRoadBlock _defaultStartingBlockPrefab;
    private Transform _parentForRoad;

    private StartingPlatformBlock _platformBlock;
    private StartingRoadBlock _defaultStartingBlock;
    private RoadBlock _startingBlock;
    private int _currentBlocksAmount = 2; // ещё есть стартовая платформа и 1й участок дороги
    private Vector2 _screenCenterPos;

    public List<RoadBlock> roadBlocksList = new List<RoadBlock>();

    public void Generation()
    {
        Clear();
        _parentForRoad = _parentManager.parentForRoad;
        _platformBlock = Instantiate(_platformPrefab, _parentForRoad);
        _platformBlock.SetupAsDefault();
        _platformBlock.Setup(_mainLogic, this);
        _platformBlock.transform.localPosition = new Vector3(0f, -2f, 0f);

        _defaultStartingBlock = Instantiate(_defaultStartingBlockPrefab, _parentForRoad);
        _defaultStartingBlock.Setup(_mainLogic, this);
        _defaultStartingBlock.SetupAsDefault();
        _defaultStartingBlock.transform.localPosition = new Vector3(2.5f, 0f, 1.5f);
        _defaultStartingBlock.Direction = Directions.right;
        _defaultStartingBlock.Scale = 2;
        _platformBlock.SetNextBlock(_defaultStartingBlock);

        _startingBlock = _defaultStartingBlock;
        GenerateRoad(_mainLogic.GameSettingsSO.blocksCountOnStart);
    }

    private void Update()
    {
        if (_mainLogic.GetState() != GameStates.play)
        {
            return;
        }

        RuntimeGeneration();
    }

    public RoadBlock GetStartingPlatformRoadBlock()
    {
        return _platformBlock;
    }

    private void RuntimeGeneration()
    {
        if (_ballController.GetBallStates() == BallStates.move)
        {
            float delta = Mathf.Sqrt(Mathf.Pow(_screenCenterPos.x - _cameraController.GetCameraTransform().position.x, 2)
                                     + Mathf.Pow(_screenCenterPos.y - _cameraController.GetCameraTransform().position.z, 2));
            if (delta < _mainLogic.GameSettingsSO.visibleRoadDistance)
            {
                GenerateRoad(1);
            }
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
            if (startBlockPos.x >= centerXZ && _startingBlock.Direction == Directions.left //  вычисление размера проекции допустимого размера блока
                || startBlockPos.x <= centerXZ && _startingBlock.Direction == Directions.right)
            {
                suggestedCathet = _cameraController.BoundsSize - dis;
            }

            if (startBlockPos.x <= centerXZ && _startingBlock.Direction == Directions.left
                || startBlockPos.x >= centerXZ && _startingBlock.Direction == Directions.right)
            {
                suggestedCathet = _cameraController.BoundsSize + dis;
            }


            int suggestedHipo = (int) (suggestedCathet * Mathf.Sqrt(2f)); //  по размеру проекци вычисляем допустимый размер нового блока
            int maxHipo = suggestedHipo > _mainLogic.GameSettingsSO.maxBlockRoadSize ? _mainLogic.GameSettingsSO.maxBlockRoadSize : suggestedHipo; //  случайно выбираем его размер в допустимых пределах
            int currentBlockScale = Random.Range(2, maxHipo);
            if (currentBlockScale < 2)
            {
                Debug.LogError("Wrong RoadBlock Scale = " + currentBlockScale + " hipo = " + maxHipo);
            }

            _currentBlocksAmount++;
            block.name = "Block_" + _currentBlocksAmount;
            block.Setup(_mainLogic, this);
            block.Setup(currentBlockScale, _startingBlock);
            _startingBlock = block;
            roadBlocksList.Add(block);
            if (i == blocksNumber)
            {
                Vector3 v3c = block.GetPositionForNextBlock();
                float c = (v3c.x + v3c.z) / 2f;
                _screenCenterPos = new Vector2(v3c.x, v3c.z);
            }
        }
    }

    public void SendBlockToPool(RoadBlock roadBlock)
    {
        roadBlocksList.Remove(roadBlock);
        PoolManager.PutRoadBlockToPool(roadBlock);
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
}