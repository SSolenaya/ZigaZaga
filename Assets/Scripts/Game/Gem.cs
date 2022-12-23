using UnityEngine;

public class Gem : MonoBehaviour
{
    private readonly int scoreForGem = 2;//TODO to SO

    public void OnTriggerEnter(Collider col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();

        if (ball == null)
        {
            return;
        }

        GameInfoManager.Inst.AddGem(1);
        GameInfoManager.Inst.AddScore(scoreForGem);
        AudioController.Inst.PlayGemSound();
        SelfDestroy();
    }

    public void SelfDestroy()
    {
        PoolManager.PutGemToPool(this);
    }
}