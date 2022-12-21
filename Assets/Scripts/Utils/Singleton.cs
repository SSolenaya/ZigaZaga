using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T s_Instance;

    public static T Inst
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType<T>();
                if (s_Instance == null)
                {
                    if (s_Instance == null)
                    {
                        var gameObject = new GameObject(typeof(T).Name);
                        s_Instance = gameObject.AddComponent<T>();
                    }
                }
            }

            return s_Instance;
        }
    }

    protected virtual void OnDestroy()
    {
        if (s_Instance)
        {
            Destroy(s_Instance);
        }

        s_Instance = null;
    }
}


