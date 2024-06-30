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
    protected bool isAttack;

    Vector3 dir;
    bool chase;
    bool isStatus;
    bool isDead;

    int skillLevel = 1;
    float skillDot;

    public GameObject[] skillItems;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        chase = this.gameObject.GetComponentInChildren<cMonsterChase>().GetisChase();
        if (chase == false)
            return;

        Montser_Dir();
    }

    void FixedUpdate()
    {
        if (!chase || isAttack || isDead)
            return;

        Vector2 monsterDir = dir.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + monsterDir);
    }
    protected virtual void Attack(float distance)
    {
        Vector2 dir = new Vector2(-transform.localScale.x, 0).normalized;
        int layerMast = 1 << LayerMask.NameToLayer("Player");

        Debug.DrawRay(transform.position, new Vector2(-transform.localScale.x, 0) / 2f, Color.yellow);
        if (Physics2D.Raycast(transform.position, dir, distance, layerMast))
            anim.SetTrigger("isMonAttack");
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Skill"))
        {
            if (!this.gameObject.GetComponentInChildren<cMonsterChase>().GetisChase())
                this.gameObject.GetComponentInChildren<cMonsterChase>().SetisChase(true);

            hp -= collision.gameObject.GetComponent<cBullet>().skillData.skillDamage[skillLevel]
                                                                                + GameManager.instance.spell
                                                                                + Random.Range(10, 15);
            Debug.Log("몬스터 HP : " + hp);

            if (hp <= 0)
                Dead();

            dotdamage(collision, "Fire");
            dotdamage(collision, "Ice");
            dotdamage(collision, "Poision");

            Destroy(collision.gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player_Normal"))
        {
            //데미지 얼마인지
            hp -= GameManager.instance.str + Random.Range(5, 10);
            Debug.Log("몬스터 HP : " + hp);

            AttackReaction();

            if (hp <= 0)
                Dead();
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
                Debug.Log("몬스터 HP : " + hp);
                dotTick++;
            }
            yield return null;
        }
        ResetStatus();
        isStatus = false;
    }

    void Montser_Dir()
    {
        if (!chase || isAttack || isDead)
            return;

        dir = GameManager.instance.player.transform.position - transform.position;

        anim.SetFloat("isRun", dir.magnitude);

        if (dir.x < 0f)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void StartAttack()
    {
        rigid.velocity = Vector3.zero;
        isAttack = true;
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
            else if (skillName == "Normal")
                renderers[i].color = new Color(0.5f, 0.5f, 0.5f);
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

    void AttackReaction()
    {
        // 코루틴으로 피격시 색상 변경하고 일정 시간 후 다시 되돌아옴
        StartCoroutine("Reaction");

    }

    IEnumerator Reaction()
    {
        ApplyStatus("Normal");

        Vector3 knockbackDir = this.transform.position - GameManager.instance.player.transform.position;
        rigid.AddForce(knockbackDir.normalized * 3f, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);

        ResetStatus();
    }

    void Dead()
    {
        if (isDead)
            return;

        rigid.velocity = Vector3.zero;
        isDead = true;
        ++GameManager.instance.monsterCount;
        DropItem();
        speed = 0f;
        isStatus = true;
        anim.SetBool("isDead", true);
        capCollider.enabled = false;
        StartCoroutine("Dead_Coroutine");
    }

    IEnumerator Dead_Coroutine()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void DropItem()
    {
        int itemNum = Random.Range(0, skillItems.Length);
        Debug.Log("아이템이 떨어졌습니다.");
        GameObject itemObj = Instantiate(skillItems[itemNum]);
        itemObj.transform.position = this.transform.position;
    }
}