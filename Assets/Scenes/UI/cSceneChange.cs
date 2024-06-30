using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cSceneChange : MonoBehaviour
{
    public void SceneChange_Level_Lobby()
    {
        StartCoroutine("StartSceneLoad_Lobby");
    }

    public void SceneChange_Level_Stage()
    {
        StartCoroutine("StartSceneLoad_Stage");
    }

    IEnumerator StartSceneLoad_Lobby()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    IEnumerator StartSceneLoad_Stage()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
}
