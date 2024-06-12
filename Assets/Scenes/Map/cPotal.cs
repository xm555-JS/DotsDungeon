using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cPotal : MonoBehaviour
{
    public GameObject Fade;
    Vector3 playerPos;

    void Awake()
    {
        // 초기 플레이어 위치 설정
        playerPos = new Vector3(-9f, -0.75f, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.player.SetactionCameraOn(true);
            Fade.GetComponent<FadeInOut>().SetStart(true);
            StartCoroutine("TransScene");
        }
    }

    IEnumerator TransScene()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Map1");
        GameManager.instance.player.transform.position = playerPos;
    }
}
