using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    Text timeText;

    private void Awake()
    {
        timeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int sec = ((int)GameManager.instance.gameTime % 60);
        int min = ((int)GameManager.instance.gameTime / 60);
        timeText.text = min.ToString("00") + " : " + sec.ToString("00");
    }
}
