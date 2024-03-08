using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonster : MonoBehaviour
{
    protected CircleCollider2D targetCollider;
    protected Rigidbody2D rigid;
    protected Animator anim;

    Vector3 dir;
    float speed;
    bool chase;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        targetCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (chase == false)
            return;

        dir = GameManager.instance.player.transform.position - transform.position;

        anim.SetFloat("Run", dir.magnitude);

        if (dir.x < 0f)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    protected void FixedUpdate()
    {
        if (chase == false)
            return;

        Vector2 monsterDir = dir.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + monsterDir);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            chase = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("데미지를 입었다.");
        }
    }
}
