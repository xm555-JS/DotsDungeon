using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDontDestroyLoad : MonoBehaviour
{
    private static List<string> dontDestoryObjects = new List<string>();

    void Awake()
    {
        if (dontDestoryObjects.Contains(gameObject.name))
        {
            Destroy(gameObject);
            return;
        }

        dontDestoryObjects.Add(gameObject.name);
        DontDestroyOnLoad(gameObject);
    }
}
