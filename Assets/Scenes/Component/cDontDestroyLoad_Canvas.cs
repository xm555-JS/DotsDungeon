using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cDontDestroyLoad_Canvas : MonoBehaviour
{
    private static List<string> dontDestoryObjects = new List<string>();

    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level_Lobby")
            Destroy(gameObject);

        if (dontDestoryObjects.Contains(gameObject.name))
        {
            Destroy(gameObject);
            return;
        }

        dontDestoryObjects.Add(gameObject.name);
        DontDestroyOnLoad(gameObject);
    }
}
