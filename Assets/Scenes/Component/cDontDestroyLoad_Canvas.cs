using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cDontDestroyLoad_Canvas : MonoBehaviour
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
    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level_Lobby")
        {
            // Level_Lobby 씬에서는 자신을 파괴하지 않음
            if (dontDestoryObjects.Contains(gameObject.name))
            {
                dontDestoryObjects.Remove(gameObject.name);
            }
            Destroy(gameObject);
        }

    }
}
