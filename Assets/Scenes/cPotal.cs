using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cPotal : MonoBehaviour
{
    void Awake()
    {
        // 현제 scene의 이름을 가져온 뒤
        // Map까지만 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // 씬 변경
            // 플레이어 이동
            SceneManager.LoadScene("Map1");
        }

    }
}
