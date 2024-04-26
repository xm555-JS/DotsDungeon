using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cPlayer : MonoBehaviour
{
    cSkillData skillData;
    cSkill Effect;

    public GameObject skill;

    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    CapsuleCollider2D capCollider;
    Animator anim;

    Sprite skillSprite;

    float hp;
    float maxHp;

    bool isSkillSetting;
    bool isAttack;

    float defaultAttackSpeed = 1f;
    bool isDefaultAttack;

    public cSkillData returnData() { return skillData; }
    public void SetisSkillSetting(bool _isSkillSetting) { isSkillSetting = _isSkillSetting; }
    public bool GetisSkillSetting() { return isSkillSetting; }
    public void PlayerHpDown(float damege) { hp -= damege; }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        maxHp = 100f;
        hp = maxHp;
    }

    void Start()
    {
        Effect = skill.GetComponent<cSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == true)
            return;

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
        if (inputVec == null || isAttack == true)
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
            skillData = collision.gameObject.GetComponent<cItem>().skillData;
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Skill"))
        {
            hp -= 20f + Random.Range(10, 20);
            Debug.Log("플레이어 HP : " + hp);
        }
    }

    public void Attack()
    {
        if (isDefaultAttack)
            return;

        isAttack = true;
        isDefaultAttack = true;
        anim.SetTrigger("isAttack");

        StartCoroutine("DefaultAttack");
    }

    IEnumerator DefaultAttack()
    {
        yield return new WaitForSeconds(defaultAttackSpeed);

        isDefaultAttack = false;
    }

    public void Fire_Shoot()
    {
        if (isSkillSetting)
            return;

        Active_Skill("Fire_Shoot");
    }

    public void Ice_Shoot()
    {
        if (isSkillSetting)
            return;

        Active_Skill("Ice_Shoot");

    }

    public void Poision_Shoot()
    {
        if (isSkillSetting)
            return;

        Active_Skill("Poision_Shoot");
    }

    public void Heal()
    {
        if (isSkillSetting)
            return;

        Passive_Skill("Heal");
    }

    public void OnAttackAnimEnd()
    {
        isAttack = false;

        anim.SetTrigger("isAttack");
    }

    void Active_Skill(string _skillName)
    {
        isAttack = true;
        Effect.SkillInstantiate(_skillName);
        anim.SetTrigger("isAttack");
    }

    void Passive_Skill(string _skillName)
    {
        isAttack = true;
        Effect.SkillInstantiate(_skillName);
        StartCoroutine("animTrriger");
    }

    IEnumerator animTrriger()
    {
        yield return null;
        isAttack = false;
    }
}