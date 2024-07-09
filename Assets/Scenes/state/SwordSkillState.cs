using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class SwordSkillState : MonoBehaviour, IBossState
    {
        private BossController _bossController;
        private Animator anim;

        public void Handle(BossController bossController)
        {
            if (!_bossController)
                _bossController = bossController;

            Debug.Log("SwordSKillState");
            anim.SetTrigger("isBossSwordSKill");
            this.gameObject.GetComponent<ChaseState>().SetSpeed(0f);

            // audio
            AudioManager.instance.PlayerSfx(AudioManager.Sfx.ATTACK);
        }

        void Awake()
        {
            anim = GetComponent<Animator>();
        }
    }
}

