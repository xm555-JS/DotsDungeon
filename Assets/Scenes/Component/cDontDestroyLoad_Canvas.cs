using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cDontDestroyLoad_Canvas : MonoBehaviour
{
    private static List<string> dontDestroyObjects = new List<string>();

    void Awake()
    {
        if (dontDestroyObjects.Contains(gameObject.name))
        {
            Destroy(gameObject);
            return;
        }

        dontDestroyObjects.Add(gameObject.name);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level_Lobby")
        {
            // Level_Lobby 씬에서는 자신을 파괴하지 않음
            if (dontDestroyObjects.Contains(gameObject.name))
            {
                dontDestroyObjects.Remove(gameObject.name);
            }
            Destroy(gameObject);
        }

    }
}
