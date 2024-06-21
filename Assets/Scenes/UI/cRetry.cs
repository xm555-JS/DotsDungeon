using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cRetry : MonoBehaviour
{
    public void Reload_Scene()
    {
        GameManager.instance.DeadScene.transform.localScale = new Vector3(0f, 0f, 0f);
        SceneManager.LoadScene("Level_Stage1");
    }

    public void Home()
    {
        GameManager.instance.DeadScene.transform.localScale = new Vector3(0f, 0f, 0f);
        SceneManager.LoadScene("Level_Lobby");
    }
}
