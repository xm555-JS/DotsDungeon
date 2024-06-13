using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Common")]
    public static GameManager instance;

    [Header("Player")]
    public cPlayer player;
    bool isActive = false;

    [Header("GameInfo")]
    public GameObject potal;
    public int monsterCount;
    public int money;
    public float str;
    public float spell;
    public float defen;
    public float hp;
    public float coolTime;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        PotalOpen();
        PlayerActiveOn();
        Maximum();
        // test_code
        if (Input.GetKeyDown(KeyCode.F))
            money += 1000;
    }

    void PotalOpen()
    {
        if (!potal)
            return;

        if (monsterCount >= 1)
            potal.SetActive(true);
    }

    void PlayerActiveOn()
    {
        if (isActive)
            return;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level_Stage1")
        {
            player.transform.parent.gameObject.SetActive(true);
            isActive = true;
        }
    }

    void Maximum()
    {
        if (money >= 1000000)
            money = 1000000;
    }
}
