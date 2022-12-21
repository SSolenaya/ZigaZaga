using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private RoadBlock _roadBlockPrefab;
    [SerializeField] private DefaultRoadBlock _platformPrefab;
    [SerializeField] private DefaultRoadBlock _defaultStartingBlockPrefab;
    [SerializeField] private RoadBlock _platformBlock;
    [SerializeField] private RoadBlock _defaultStartingBlock;
    [SerializeField] private RoadBlock _startingBlock;
    [SerializeField] private Transform _parentForRoad;
    private readonly int _maxScale = 6;          // temp
    private int _maxAmountOfBlocks = 12;
    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _blockCameraTransform;
    private List<RoadBlock> roadBlocksList = new List<RoadBlock>();
    private Vector2 _screenCenterPos;


    [SerializeField] public static float boundsSize;            //  границы экрана

    private void Start()
    {
        float orthoSize = _cam.orthographicSize;
        boundsSize = orthoSize / Screen.height * Screen.width;
    }

    public void Restart()
    {
        if(roadBlocksList.Count > 0) { 
            foreach (var b in roadBlocksList)
            {
                DestroyBlock(b);
            }
            roadBlocksList?.Clear();
        }
        if (_platformBlock == null)
        {
            _platformBlock = Instantiate(_platformPrefab, _parentForRoad);
            _platformBlock.transform.localPosition = Vector3.zero;
        }
        if (_defaultStartingBlock == null)
        {
            _defaultStartingBlock = Instantiate(_defaultStartingBlockPrefab, _parentForRoad);
            _defaultStartingBlock.transform.localPosition = new Vector3(2.5f, 0f, 1.5f);
        }
        _blockCameraTransform.position = new Vector3(0, 10f, 0);
        _startingBlock = _defaultStartingBlock;
        GenerateRoad(_maxAmountOfBlocks);
    }

    private void Update()
    {
        Vector3 v3 = new Vector3(Mathf.Sqrt(2f), 0, Mathf.Sqrt(2f));
        _blockCameraTransform.position += v3 * Time.deltaTime;          // перемещение камеры за мячом
        float delta = Mathf.Sqrt(Mathf.Pow(_screenCenterPos.x - _blockCameraTransform.position.x, 2) + Mathf.Pow(_screenCenterPos.y - _blockCameraTransform.position.z, 2));
        if (delta < 30)
        {
            GenerateRoad(1);
        }
    }

    public void GenerateRoad(int blocksNumber)
    {
        for (int i = 1; i <= blocksNumber; i++)
        {
            RoadBlock block = PoolManager.GetRoadBlockFromPull(_roadBlockPrefab);
            block.transform.SetParent(_parentForRoad);
            Vector3 startBlockPos = _startingBlock.GetPositionForNextBlock();

            float centerXZ =  (startBlockPos.x + startBlockPos.z) / 2f;                                                     //  позиция предполагаемого центра экрана, а также объекта камеры на уровне рассматриваемого блока

            float dis = Mathf.Sqrt(Mathf.Pow(centerXZ - startBlockPos.x, 2) + Mathf.Pow(centerXZ - startBlockPos.z, 2));    //  расстояние между точкой, откуда нужно начать генерацию блока и предполагаемым центром
            float suggestedCathet = 0;
            if ((startBlockPos.x > centerXZ && _startingBlock._myDirection == Directions.left)                              //  вычисление размера проекции допустимого размера блока
                || (startBlockPos.x < centerXZ && _startingBlock._myDirection == Directions.right))
            {
                suggestedCathet = boundsSize - dis;
            }

            if ((startBlockPos.x < centerXZ && _startingBlock._myDirection == Directions.left)
                || (startBlockPos.x > centerXZ && _startingBlock._myDirection == Directions.right))
            {
                suggestedCathet = boundsSize + dis;
            }


            int suggestedHipo = (int) (suggestedCathet * Mathf.Sqrt(2f));                                                   //  по размеру проекци вычисляем допустимый размер нового блока
            int maxHipo = suggestedHipo > 10 ? 10 : suggestedHipo;                                                          //  случайно выбираем его размер в допустимых пределах
            int currentBlockScale = Random.Range(2, maxHipo);
            block.Setup(currentBlockScale, _startingBlock, this);
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

    public void DestroyBlock(RoadBlock roadBlock)       //  TODO: return to Pool
    {
        if (roadBlock == null) return;
        roadBlocksList.Remove(roadBlock);
        PoolManager.PutRoadBlockToPool(roadBlock);
    }
}