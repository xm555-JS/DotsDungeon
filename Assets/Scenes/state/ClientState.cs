using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class ClientState : MonoBehaviour
    {
        private BossController _bossController;
        private GameObject BossArea;
        private Rigidbody2D rigid;

        private GameObject cam;

        private float time;
        private int skillLevel;
        private float skillDot;
        private bool isStatus;
        private bool isDead;

        private bool isSkip = false;

        public float hp;


        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();

            time = 0.5f;
            skillLevel = 1;
            skillDot = 0;
            isStatus = false;
            isDead = false;

            hp = 200f;
        }
        void Start()
        {
            _bossController = (BossController)FindObjectOfType(typeof(BossController));
            BossArea = GameObject.Find("Boss_Area");
            cam = GameObject.Find("Main Camera");
            cam.GetComponent<cCamControl>().TargetSetting(gameObject);
            StartCoroutine("FoucusOnPlayer");
        }

        IEnumerator FoucusOnPlayer()
        {
            yield return new WaitForSeconds(5f);
            cam.GetComponent<cCamControl>().FocusOnPlayer();
            GameManager.instance.player.SetactionCameraOn(false);
        }

        void Update()
        {
            // 임시 playerprefs로 isSkip을 저장하고 이 변수로 카메라 이동 및 연출을 스킵할 수 있도록 할거임.
            Appearance();

            if (isDead)
                return;
            if (!BossArea.GetComponent<cMonsterChase>().GetisChase())
                return;

            int randomNum = 0;

            Vector2 dir = new Vector2(-transform.localScale.x, 0).normalized;
            int layerMast = 1 << LayerMask.NameToLayer("Player");

            Vector3 bossPos = transform.position + new Vector3(0f, 0.5f, 0f);
            Debug.DrawRay(bossPos, new Vector2(-transform.localScale.x, 0) / 2f, Color.yellow);
            if (Physics2D.Raycast(transform.position, dir, 2f, layerMast))
            {
                time += Time.deltaTime;

                if (time >= 1f)
                {
                    randomNum = Random.Range(1, 4);
                    if (randomNum == 1)
                        _bossController.SwordSkillState();
                    else if (randomNum == 2)
                        _bossController.MagicSkillState();
                    else if (randomNum == 3)
                        _bossController.NormalAttackState();

                    time = 0f;
                }
            }
            else
            {
                time += Time.deltaTime;

                if (time >= 1f)
                {
                    _bossController.ChaseState();
                    time = 0f;
                }
            }

        }

        void Appearance()
        {
            isSkip = true;
            if (isSkip)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    StopCoroutine("FoucusOnPlayer");
                    cam.GetComponent<cCamControl>().FocusOnPlayer();
                    GameManager.instance.player.SetactionCameraOn(false);
                }
            }
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Skill"))
            {
                if (!BossArea.GetComponent<cMonsterChase>().GetisChase())
                    BossArea.GetComponent<cMonsterChase>().SetisChase(true);

                hp -= collision.gameObject.GetComponent<cBullet>().skillData.skillDamage[skillLevel] + Random.Range(10, 25);
                Debug.Log(hp + "이정도 남았다.");

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
                hp -= GameManager.instance.player.attackPower + Random.Range(3, 8);
                Debug.Log(hp + "이정도 남았다.");

                AttackReaction();

                if (hp <= 0)
                    Dead();
            }

            if (collision.gameObject.CompareTag("Player"))
                Debug.Log("플레이어와의 충돌");
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
                    {
                        isDead = true;
                        _bossController.DeadState();
                    }

                    time = 0f;
                    Debug.Log(hp + "이정도 남았다.");
                    dotTick++;
                }
                yield return null;
            }
            ResetStatus();
            isStatus = false;
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
            isDead = true;
            _bossController.DeadState();
        }
    }
}

