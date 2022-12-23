using UnityEngine;

public class Gem : MonoBehaviour
{
    private float _speed;

    public void OnEnable()
    {
        transform.localEulerAngles += Vector3.up * Random.Range(0f, 360f);
        _speed = MainLogic.Inst.SO.gemRotatingSpeed * Random.Range(0.8f, 1.2f);
    }

    public void OnTriggerEnter(Collider col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();

        if (ball == null)
        {
            return;
        }

        GameInfoManager.Inst.AddGem(1);
        GameInfoManager.Inst.AddScore(MainLogic.Inst.SO.scoreForGem);
        AudioController.Inst.PlayGemSound();
        SelfDestroy();
    }

    public void Update()
    {
        transform.localEulerAngles += Vector3.up * Time.deltaTime * _speed;
    }

    public void SelfDestroy()
    {
        PoolManager.PutGemToPool(this);
    }
}