using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBullet : MonoBehaviour
{
    public GameObject obj;
    public cSkillData skillData;

    Rigidbody2D rigid;
    float speed;
    Vector2 playerVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        speed = 5f;
    }
    void Start()
    {
        playerVec = new Vector2(-GameManager.instance.player.transform.localScale.x, 0);
    }

    void FixedUpdate()
    {
        rigid.velocity = playerVec * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(obj);
        }
    }
}
