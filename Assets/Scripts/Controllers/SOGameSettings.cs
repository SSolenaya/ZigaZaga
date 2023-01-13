using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gameSettings", menuName = "ScriptableObject/GameSettings")]
public class SOGameSettings : ScriptableObject
{
    public int scoreForGem = 2;
    public int scoreForTap = 1;

    public float gemRotatingSpeed = 30;

    public int blocksCountOnStart = 10;
    public int maxBlockRoadSize = 6;
    public float visibleRoadDistance = 30;

    public float yCoordForDestroy = -6;

    [Range(1f, 10f)]
    public float ballSpeed = 2f;

}
