using UnityEngine;

public class Gem : MonoBehaviour
{
    private float _speed;
    private RoadBlock _parentRoadBlock;


    public void OnEnable()
    {
        transform.localEulerAngles += Vector3.up * Random.Range(0f, 360f);
        _speed = MainLogic.Inst.SO.gemRotatingSpeed * Random.Range(0.8f, 1.2f);
    }

    public void Setup(RoadBlock rb)
    {
        _parentRoadBlock = rb;
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