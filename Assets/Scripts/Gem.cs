using UnityEngine;

public class Gem : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
       
        Ball ball = col.gameObject.GetComponent<Ball>();

        if (ball == null)
        {
            return;
        }
        Debug.Log("Gem triggered by: " + col.gameObject.name);
        UserProgressManager.Inst.AddOneGemToCurrentProgress();
        AudioController.Inst.PlayGemSound();
        SelfDestroy();
    }

    public void SelfDestroy()
    {
        Debug.LogError("Gem triggered by " + gameObject.name);
        PoolManager.PutGemToPool(this);
    }
}