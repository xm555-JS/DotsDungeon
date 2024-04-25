using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonster : MonoBehaviour
{
    protected CapsuleCollider2D capCollider;
    protected Rigidbody2D rigid;
    protected Animator anim;

    protected float speed;
    protected float hp;
    protected float maxHp;
    protected float distance;

    Vector3 dir;
    bool isAttack;
    bool chase;
    bool isStatus;
    bool isDead;

    int skillLevel = 1;
    float skillDot;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        chase = this.gameObject.GetComponentInChildren<cMonsterChase>().GetisChase();
        if (chase == false)
            return;

        Montser_Dir();
        Attack(distance);
    }

    void FixedUpdate()
    {
        if (!chase || isAttack || isStatus || isDead)
            return;

        Vector2 monsterDir = dir.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + monsterDir);
    }

    protected void Attack(float distance)
    {
        Vector2 dir = new Vector2(-transform.localScale.x, 0).normalized;
        int layerMast = 1 << LayerMask.NameToLayer("Player");

        Debug.DrawRay(transform.position, new Vector2(-transform.localScale.x, 0) / 2f, Color.yellow);
        if (Physics2D.Raycast(transform.position, dir, distance, layerMast))
        {
            isAttack = true;
            anim.SetTrigger("isMonAttack");
        }
    }

    void returnMove()
    { 
        // 콜라이더에 일정 시간 충돌이 되지 않으면 원래자리로 return;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Skill"))
        {
            hp -= collision.gameObject.GetComponent<cBullet>().skillData.skillDamage[skillLevel] + Random.Range(-20, 20);
            Debug.Log(hp + "이정도 남았다.");

            if (hp <= 0)
                Dead();

            dotdamage(collision, "Fire");
            dotdamage(collision, "Ice");
            dotdamage(collision, "Poision");

            Destroy(collision.gameObject);
        }
    }

    IEnumerator DamegeOnTime()
    {
        float time = 0f;
        int dotTick = 0;

        while (dotTick <= 5)
        {
            time += Time.deltaTime;

            if (time >= 1)
            {
                hp -= skillDot;

                if (hp <= 0)
                    Dead();

                time = 0f;
                Debug.Log(hp + "이정도 남았다.");
                dotTick++;
            }
            yield return null;
        }
        ResetStatus();
        isStatus = false;
    }

    void Montser_Dir()
    {
        if (!chase || isAttack || isStatus || isDead)
            return;

        dir = GameManager.instance.player.transform.position - transform.position;

        anim.SetFloat("isRun", dir.magnitude);

        if (dir.x < 0f)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void EndAttack()
    {
        isAttack = false;
        anim.SetTrigger("isMonAttack");
    }

    void dotdamage(Collision2D collision, string skillName)
    {
        if (skillName == collision.gameObject.GetComponent<cBullet>().skillData.skillType)
        {
            skillDot = collision.gameObject.GetComponent<cBullet>().skillData.skillDot[skillLevel];
            StartCoroutine("DamegeOnTime");
            ApplyStatus(skillName);
        }
    }

    void ApplyStatus(string skillName)
    {
        isStatus = true;

        SpriteRenderer[] renderers = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        if (renderers == null)
            return;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].name == "Shadow")
                return;

            if (skillName == "Fire")
                renderers[i].color = new Color(1f, 0f, 0f);
            else if (skillName == "Ice")
                renderers[i].color = new Color(0f, 0f, 1f);
            else if (skillName == "Poision")
                renderers[i].color = new Color(0.5f, 0f, 0.5f);
        }
    }

    void ResetStatus()
    {
        SpriteRenderer[] renderers = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        if (renderers == null)
            return;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].name == "Shadow")
                return;

            if (renderers[i].name == "front")
                renderers[i].color = new Color(0.4433962f, 0.1484959f, 0.1484959f);
            else
                renderers[i].color = new Color(1f, 1f, 1f);
        }
    }

    void Dead()
    {
        isStatus = true;
        anim.SetBool("isDead", true);
        capCollider.enabled = false;
    }
}