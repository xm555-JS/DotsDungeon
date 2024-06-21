using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cGameEnd : MonoBehaviour
{
    public GameObject EndUI;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isEnd)
            EndUI.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Home_Button()
    {
        GameManager.instance.money += 10000;
        SceneManager.LoadScene("Level_Lobby");
    }
}
