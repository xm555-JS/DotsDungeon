using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cPlayer : MonoBehaviour
{
    cSkillData skillData;

    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    CapsuleCollider2D capCollider;
    Animator anim;

    Sprite skillSprite;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("isRun", inputVec.magnitude);

        if (inputVec.x != 0f)
        { 
            if (inputVec.x > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        if (inputVec == null)
            return;

        Vector2 moveVec = inputVec * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skill_Item"))
        {
            // ��ųâ�� �浹�� ��ų�� ������.
            skillData = collision.gameObject.GetComponent<cItem>().skillData;

            collision.gameObject.SetActive(false);
        }
    }

    public cSkillData returnData() { return skillData; }

    public void Attack()
    {
        Debug.Log("�⺻ ��ų ���");
    }
    public void Fire_Skill()
    {
        Debug.Log("�� ��ų ���!");
    }

    public void Heal()
    {
        Debug.Log("�� ��ų ���!");
    }
}