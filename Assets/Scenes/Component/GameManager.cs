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
    public GameObject DeadScene;
    bool isActive = false;

    [Header("GameInfo")]
    public GameObject potal;
    public int monsterCount;
    public bool isEnd;
    public int money;
    public float str;
    public float spell;
    public float defen;
    public float hp;
    public float coolTime;

    void Awake()
    {
        instance = this;

        // 저장된 데이터 Load
        //money = PlayerPrefs.GetInt("money");
        //str = PlayerPrefs.GetFloat("str");
        //spell = PlayerPrefs.GetFloat("spell");
        //defen = PlayerPrefs.GetFloat("defen");
        //hp = PlayerPrefs.GetFloat("hp");
        //coolTime = PlayerPrefs.GetFloat("coolTime");
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
            player.transform.position = new Vector3(-9f, -0.75f, player.transform.position.z);
            player.transform.parent.gameObject.SetActive(false);
            player.transform.parent.gameObject.SetActive(true);
            isActive = true;

            potal = Resources.Load<GameObject>("Prefabs/Potal");
            potal = Instantiate(potal);
            potal.SetActive(false);

            DeadScene = GameObject.Find("DeadScene");
        }
    }

    void Maximum()
    {
        if (money >= 1000000)
            money = 1000000;
    }
}
