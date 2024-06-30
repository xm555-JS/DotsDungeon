using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cRetry : MonoBehaviour
{
    public void Reload_Scene()
    {
        Time.timeScale = 1f;
        GameManager.instance.DeadScene.transform.localScale = new Vector3(0f, 0f, 0f);
        StartCoroutine("StartSceneLoad_Stage");
    }

    public void Home()
    {
        Time.timeScale = 1f;
        GameManager.instance.DeadScene.transform.localScale = new Vector3(0f, 0f, 0f);
        StartCoroutine("StartSceneLoad_Lobby");
    }

    IEnumerator StartSceneLoad_Lobby()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level_Lobby");
    }

    IEnumerator StartSceneLoad_Stage()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level_Stage1");
    }
}
