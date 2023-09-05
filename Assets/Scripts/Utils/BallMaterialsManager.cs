using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BallMaterialsManager", menuName = "ScriptableObject/BallMaterialsManager", order = 2)]
public class BallMaterialsManager : ScriptableObject
{
 public List<BallSkinData> skinsList;

    public BallSkinData GetRandomSkinData()
    {
        if (skinsList.Count > 0)
        {
            int r = Random.Range(0, skinsList.Count);
            return skinsList[r];
        } else { 
            Debug.LogError("BallMaterialsManager doesn't have any skin data on its list");
            return null;
        }
    }
}
