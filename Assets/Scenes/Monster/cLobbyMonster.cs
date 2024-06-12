using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cLobbyMonster : MonoBehaviour
{
    CapsuleCollider2D capCollider;
    Rigidbody2D rigid;
    Animator anim;

    float speed = 3f;
    float distance;
    float time;
    float changeTime = 3f;
    Vector3 dir;
    Vector3 randVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        randVec = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        Montser_Dir();
    }

    void FixedUpdate()
    {
        Vector2 monsterDir = dir.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + monsterDir);
    }

    void Montser_Dir()
    {
        RandDir();

        //if (this.gameObject.GetComponentInChildren<cMonsterChase>().GetisChase())
        //{
        //}

        dir = randVec;

        anim.SetFloat("isRun", dir.magnitude);

        if (dir.x < 0f)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void RandDir()
    {
        time += Time.deltaTime;
        if (changeTime <= time)
        {
            //int randX = Random.Range(-10, 11);
            //int randY = Random.Range(-10, 11);
            //float posX = (1f / (float)randX);
            //float posY = (1f / (float)randY);

            //randVec = new Vector3(posX, posY, 0f);
            randVec = Random.insideUnitCircle.normalized;

            //if (randVec.x == 0)
            //    randVec.x += 1;
            //if (randVec.y == 0)
            //    randVec.y += 1;

            time = 0f;
        }
    }
}
