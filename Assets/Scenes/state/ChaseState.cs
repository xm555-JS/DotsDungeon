using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class ChaseState : MonoBehaviour, IBossState
    {
        private BossController _bossController;
        private Animator anim;
        private Rigidbody2D rigid;

        private Vector2 dir;
        private float speed = 1f;
        private bool isChase;
        private float stepTime;

        public void SetSpeed(float _speed) { speed = _speed; }

        public void Handle(BossController bossController)
        {
            if (!_bossController)
                _bossController = bossController;

            if (speed <= 0f)
                speed = 1f;

            isChase = true;
        }

        void Update()
        {
            Montser_Dir();
        }

        void Montser_Dir()
        {
            if (!isChase)
                return;

            dir = GameManager.instance.player.transform.position - transform.position;

            anim.SetFloat("isRun", dir.magnitude);

            if (dir.x < 0f)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }

        void FixedUpdate()
        {
            if (!isChase)
                return;

            Vector2 monsterDir = dir.normalized * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + monsterDir);
            StepAudio();
        }

        void StepAudio()
        {
            if (speed < 0.5f)
                return;

            stepTime += Time.deltaTime;
            if (stepTime >= 0.3f)
            {
                AudioManager.instance.PlayerSfx(AudioManager.Sfx.STEP);
                stepTime = 0;
            }
        }

        void Awake()
        {
            anim = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            isChase = false;
        }
    }
}