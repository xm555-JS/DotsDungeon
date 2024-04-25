using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonsterChase : MonoBehaviour
{
    bool isChase;

    public bool GetisChase() { return isChase; }

    void Awake()
    {
        isChase = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isChase = true;
    }
}
