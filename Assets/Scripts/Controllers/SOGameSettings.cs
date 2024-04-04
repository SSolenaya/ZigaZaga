using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gameSettings", menuName = "ScriptableObject/GameSettings", order = 1)]
public class SOGameSettings : ScriptableObject
{
    public int scoreForGemModifier = 2;
    public int scoreForTap = 1;

    public float gemRotatingSpeed = 30;

    public int blocksCountOnStart = 10;
    public int maxBlockRoadSize = 6;
    public float visibleRoadDistance = 30;

    public float yCoordForDestroy = -6;

    public float ballMaxSpeed = 120f;
    
    public bool IsBallRuningOnMath;

    public List<GameModeData> gameModes = new List<GameModeData> (4);

}
