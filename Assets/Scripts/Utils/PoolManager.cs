using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static Stack<RoadBlock> _roadBlockStack;
    private static Stack<Gem> _gemStack;
    private static Transform _parentForDeactivatedGO;
    public Transform parentGO;

    public void Init()
    {
        _parentForDeactivatedGO = parentGO;
        _roadBlockStack = new Stack<RoadBlock>();
        _gemStack = new Stack<Gem>();
    }

    public static RoadBlock GetRoadBlockFromPull(RoadBlock roadBlockPrefab)
    {
        RoadBlock result;
        if (_roadBlockStack.Count > 0)
        {
            result = _roadBlockStack.Pop();
            result.gameObject.SetActive(true);
            return result;
        }
        result = Instantiate(roadBlockPrefab, _parentForDeactivatedGO);
        result.name = roadBlockPrefab.name;
        result.gameObject.SetActive(true);
        return result;
    }

    public static Gem GetGemFromPull(Gem gemPrefab)
    {
        Gem result;
        if (_gemStack.Count > 0)
        {
            result = _gemStack.Pop();
            result.gameObject.SetActive(true);
            return result;
        }
        result = Instantiate(gemPrefab, _parentForDeactivatedGO);
        result.gameObject.SetActive(true);
        result.name = gemPrefab.name;
        return result;
    }

    public static void PutRoadBlockToPool(RoadBlock target)
    {
        target.transform.SetParent(_parentForDeactivatedGO);
        target.gameObject.SetActive(false);
        _roadBlockStack.Push(target);
       
    }

    public static void PutGemToPool(Gem target)
    {
        target.transform.SetParent(_parentForDeactivatedGO);
        target.gameObject.SetActive(false);
        _gemStack.Push(target);
    
    }

}
