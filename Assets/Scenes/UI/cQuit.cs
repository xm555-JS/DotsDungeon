using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cQuit : MonoBehaviour
{
    public void Game_Quit()
    {
        PlayerPrefs.SetInt("money", GameManager.instance.money);
        PlayerPrefs.SetFloat("str", GameManager.instance.str);
        PlayerPrefs.SetFloat("spell", GameManager.instance.spell);
        PlayerPrefs.SetFloat("defen", GameManager.instance.defen);
        PlayerPrefs.SetFloat("hp", GameManager.instance.hp);
        PlayerPrefs.SetFloat("coolTime", GameManager.instance.coolTime);
        PlayerPrefs.SetInt("Key", 1);

        Application.Quit();
    }
}
