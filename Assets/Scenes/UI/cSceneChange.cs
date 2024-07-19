using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cSceneChange : MonoBehaviour
{
    public GameObject saveAndLoad;

    public void SceneChange_Level_Lobby()
    {
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.CONFIRM);

        StartCoroutine("StartSceneLoad_Lobby");
    }

    public void SceneChange_Level_Stage()
    {
        // 아이템 저장
        PlayerPrefs.SetInt("Key", 1);
        saveAndLoad.GetComponent<SaveAndLoad>().save();

        AudioManager.instance.PlayerSfx(AudioManager.Sfx.CONFIRM);

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
