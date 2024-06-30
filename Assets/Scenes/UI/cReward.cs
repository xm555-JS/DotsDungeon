using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cReward : MonoBehaviour
{
    private Text reward_Text;
    private int gameTime;
    private int reward;

    void Awake()
    {
        reward_Text = GetComponent<Text>();
    }

    void OnEnable()
    {
        Reward();
        TextSetting();
        Time.timeScale = 0f;
    }

    void TextSetting()
    {
        gameTime = (int)GameManager.instance.gameTime;
        reward_Text.text = "Reward : " + reward.ToString() + "G";
    }

    void Reward()
    {
        if (gameTime >= 0 || gameTime < 30)
        {
            reward = 5000;
            GameManager.instance.money += reward;
        }
        else if (gameTime >= 30 || gameTime < 60)
        {
            reward = 2000;
            GameManager.instance.money += reward;
        }
        else if (gameTime <= 60 || gameTime > 120)
        {
            reward = 1000;
            GameManager.instance.money += reward;
        }
        else
        {
            reward = 500;
            GameManager.instance.money += reward;
        }
    }
}
