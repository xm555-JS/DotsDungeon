using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cPlayer : MonoBehaviour
{
    cSkillData skillData;
    cSkill Effect;

    public GameObject skill;
    public GameObject AttackCollider;

    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    CapsuleCollider2D capCollider;
    Animator anim;

    GameObject cam;

    Sprite skillSprite;

    float hp;
    float maxHp;
    float preMaxHp;

    bool isSkillSetting;
    bool isAttack;

    bool isDead = false;

    float defaultAttackSpeed = 1f;
    bool isDefaultAttack;

    bool actionCameraOn = false;

    float stepTime;

    public cSkillData returnData() { return skillData; }
    public void SetisSkillSetting(bool _isSkillSetting) { isSkillSetting = _isSkillSetting; }
    public bool GetisSkillSetting() { return isSkillSetting; }
    public void PlayerHpDown(float damege) { hp -= damege; }
    public void SetactionCameraOn(bool _actionCameraOn) { actionCameraOn = _actionCameraOn; }

    void Awake()
    {
        PlayerInitialize();
    }

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        cam.GetComponent<cCamControl>().TargetSetting(gameObject);
        Effect = skill.GetComponent<cSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHp();

        if (isDead || isAttack)
            return;

        anim.SetFloat("isRun", inputVec.magnitude);

        if (inputVec.x != 0f)
        {
            // audio
            stepTime += Time.deltaTime;
            if (stepTime >= 0.3f)
            {
                AudioManager.instance.PlayerSfx(AudioManager.Sfx.STEP);
                stepTime = 0;
            }
                
            if (inputVec.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void FixedUpdate()
    {
        if (isDead || inputVec == null || isAttack || actionCameraOn)
            return;

        Vector2 moveVec = inputVec * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);
    }

    void PlayerInitialize()
    {
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        maxHp = 100f;
        hp = maxHp;
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

            // audio 획득
            AudioManager.instance.PlayerSfx(AudioManager.Sfx.CONFIRM);
        }

        if (collision.gameObject.CompareTag("Skill"))
        {
            float damage = collision.gameObject.GetComponent<cSKillInfo>().damege;
            ApplyDamage(damage);
            Debug.Log("플레이어 HP : " + hp);
        }

        if (collision.gameObject.CompareTag("Monster_Attack"))
        {
            float damage = collision.gameObject.GetComponent<cSKillInfo>().damege;
            ApplyDamage(damage);
            Debug.Log("플레이어 HP : " + hp);

            //GameObject AttackCol = collision.gameObject.GetComponent<cOrc>().attackCollider;
            //AttackCol.SetActive(false);
        }
    }

    void SetHp()
    {
        float curHp = maxHp + GameManager.instance.hp;

        if (curHp != preMaxHp)
        {
            maxHp += GameManager.instance.hp;
            hp = maxHp;
            Debug.Log(maxHp);
        }

        preMaxHp = maxHp + GameManager.instance.hp;
    }

    void ApplyDamage(float damege)
    {
        // audio 피격
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.Hit);

        float defense = GameManager.instance.defen * 0.3f;
        float applyDamage = damege - (defense);
        if (damege <= defense)
            applyDamage = 1;

        hp -= applyDamage;
        if (hp <= 0)
        {
            isDead = true;
            Dead();
        }
    }

    void Dead()
    {
        if (isDead)
        {
            // audio 피격
            AudioManager.instance.PlayerSfx(AudioManager.Sfx.DEAD);

            anim.SetBool("isDead", true);
            capCollider.enabled = false;
            Debug.Log("죽었습니다.");
            StartCoroutine("DeadScene");
        }
    }

    IEnumerator DeadScene()
    {
        yield return new WaitForSeconds(2f);

        Time.timeScale = 0f;
        GameManager.instance.DeadScene.transform.localScale = new Vector3(1f, 1f, 1f);

        // 플레이어 reset
        isDead = false;
        hp = maxHp;
        capCollider.enabled = true;
        anim.SetBool("isDead", false);
    }

    public void Attack()
    {
        if (isDefaultAttack)
            return;
        rigid.velocity = Vector3.zero;
        isAttack = true;
        isDefaultAttack = true;
        anim.SetTrigger("isAttack");

        StartCoroutine("AttackColliderActive");
        StartCoroutine("DefaultAttack");

        // audio 공격
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.ATTACK);
    }

    IEnumerator AttackColliderActive()
    {
        // 공격 콜라이더 active On or Off
        AttackCollider.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        AttackCollider.SetActive(false);
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

        // audio fire shoot
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.FIRE);
    }

    public void Ice_Shoot()
    {
        if (isSkillSetting)
            return;

        Active_Skill("Ice_Shoot");

        // audio ice shoot
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.ICE);
    }

    public void Poision_Shoot()
    {
        if (isSkillSetting)
            return;

        Active_Skill("Poision_Shoot");

        // audio posion shoot
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.POISION);
    }

    public void Heal()
    {
        if (isSkillSetting)
            return;

        Passive_Skill("Heal");
        hp += 20f;
        Debug.Log("플레이어 HP 회복! HP : " + hp);

        // audio heal
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.HEAL);
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