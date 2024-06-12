using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItem : MonoBehaviour
{
    public cSkillData skillData;

    Rigidbody2D rigid;
    BoxCollider2D boxCol;

    float power = 4f;
    float dropY;
    float time;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        boxCol.enabled = false;
        dropY = this.transform.position.y;
        rigid.AddForce(Vector2.up * power, ForceMode2D.Impulse);
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 0.3f)
        {
            if (this.transform.position.y < dropY)
            {
                rigid.velocity = Vector3.zero;
                rigid.gravityScale = 0f;
                boxCol.enabled = true;
            }
                
                
        }
    }
}
