using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cPotal : MonoBehaviour
{
    GameObject Fade;
    Vector3 playerPos;

    void Awake()
    {
        // �ʱ� �÷��̾� ��ġ ����
        playerPos = new Vector3(-9f, -0.75f, 0f);
        Fade = GameObject.Find("TransScene");
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

        SceneManager.LoadScene("Level_Boss");
        GameManager.instance.player.transform.position = playerPos;
    }
}
