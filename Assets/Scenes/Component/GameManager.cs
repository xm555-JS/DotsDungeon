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
    public float gameTime;

    Scene preScene;
    private GameObject reward;

    void Awake()
    {
        instance = this;

        // 저장된 데이터 로드
        LoadData();
    }

    void Update()
    {
        SceneSetting();
        GameTime();
        PotalOpen();
        Maximum_Money();
        GameReward();

        if (player == null)
            Debug.Log("player가 없습니다");

        //// test_code
        //if (Input.GetKeyDown(KeyCode.F))
        //    money += 10000;
    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey("Key"))
        {
            // 저장된 데이터 Load
            money = PlayerPrefs.GetInt("money");
            str = PlayerPrefs.GetFloat("str");
            spell = PlayerPrefs.GetFloat("spell");
            defen = PlayerPrefs.GetFloat("defen");
            hp = PlayerPrefs.GetFloat("hp");
            coolTime = PlayerPrefs.GetFloat("coolTime");
        }
    }

    void PotalOpen()
    {
        if (!potal)
            return;

        if (monsterCount >= 1)
            potal.SetActive(true);
    }

    void SceneSetting()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (preScene != scene)
            isActive = false;

        if (isActive)
            return;

        instance = this;

        preScene = SceneManager.GetActiveScene();

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

            // bgm
            AudioManager.instance.PlayBgm(AudioManager.Bgm.STAGE);
        }
        else if (scene.name == "Level_Boss")
        {
            reward = Resources.Load<GameObject>("UI_Prefabs/Reward");
            reward = Instantiate(reward);
            GameObject parent = GameObject.Find("Canvas");
            reward.transform.SetParent(parent.transform);
            RectTransform recttransfom = reward.GetComponent<RectTransform>();
            recttransfom.anchoredPosition = new Vector3(0f, 0f, 0f);
            isActive = true;

            // bgm
            AudioManager.instance.PlayBgm(AudioManager.Bgm.BOSS);
        }
        else if (scene.name == "Level_Lobby")
        {
            player.transform.position = new Vector3(-7.5f, -1f, player.transform.position.z);
            player.transform.localScale = new Vector3(-1f, 1f, 1f);
            player.transform.parent.gameObject.SetActive(false);
            player.transform.parent.gameObject.SetActive(true);
            gameTime = 0f;
            isActive = true;

            // bgm
            AudioManager.instance.PlayBgm(AudioManager.Bgm.LOBBY);
        }
    }

    void Maximum_Money()
    {
        if (money >= 1000000)
            money = 1000000;
    }

    void GameTime()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level_Stage1" || scene.name == "Level_Boss")
        {
            gameTime += Time.deltaTime;
        }
    }

    void GameReward()
    {
        if (isEnd)
        {
            StartCoroutine("ShowReward");
            isEnd = false;
        }
    }

    IEnumerator ShowReward()
    {
        yield return new WaitForSeconds(2f);

        Transform[] obj = reward.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].gameObject.SetActive(true);
        }

    }
}
