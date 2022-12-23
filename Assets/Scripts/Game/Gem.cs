using UnityEngine;

public class Gem : MonoBehaviour
{
    private readonly int scoreForGem = 2;

    public void OnTriggerEnter(Collider col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();

        if (ball == null)
        {
            return;
        }

        PlayerManager.Inst.AddGem(1);
        PlayerManager.Inst.AddScore(scoreForGem);
        AudioController.Inst.PlayGemSound();
        SelfDestroy();
    }

    public void SelfDestroy()
    {
        PoolManager.PutGemToPool(this);
    }
}