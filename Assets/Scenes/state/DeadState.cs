using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class DeadState : MonoBehaviour, IBossState
    {
        private BossController _bossController;
        private Animator anim;
        private CapsuleCollider2D capCollider;

        public void Handle(BossController bossController)
        {
            if (!_bossController)
                _bossController = bossController;

            Debug.Log("BossDeadState");
            this.gameObject.GetComponent<ChaseState>().SetSpeed(0f);
            anim.SetBool("isDead", true);
            capCollider.enabled = false;
            StartCoroutine("Dead");
        }

        void Awake()
        {
            anim = GetComponent<Animator>();
            capCollider = GetComponent<CapsuleCollider2D>();
        }

        IEnumerator Dead()
        {
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }
    }
}

