using UnityEngine;
using Zenject;

public class Gem : MonoBehaviour
{
    private MainLogic _mainLogic;
    private float _speed;
    private RoadBlock _parentRoadBlock;

    [Inject]
    private void Setup(MainLogic mainLogic)
    {
        _mainLogic = mainLogic;
    }

    public void OnEnable()
    {
        transform.localEulerAngles += Vector3.up * Random.Range(0f, 360f);
        _speed = _mainLogic.GameSettingsSO.gemRotatingSpeed * Random.Range(0.8f, 1.2f);
    }

    public void Setup(RoadBlock parentBlock)
    {
        _parentRoadBlock = parentBlock;
    }

    public void Update()
    {
        transform.localEulerAngles += Vector3.up * Time.deltaTime * _speed;
    }

    public void SelfDestroy()
    {
       _parentRoadBlock.HideGem(this);
    }

    
}