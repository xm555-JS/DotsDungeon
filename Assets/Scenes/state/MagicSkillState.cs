using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class MagicSkillState : MonoBehaviour, IBossState
    {
        private BossController _bossController;
        private Animator anim;

        private float defaultDamage;

        public float GetDamege() { return defaultDamage; }

        public void Handle(BossController bossController)
        {
            if (!_bossController)
                _bossController = bossController;

            Debug.Log("MagicSkillState");
            anim.SetTrigger("isBossMagicSkill");
            this.gameObject.GetComponent<ChaseState>().SetSpeed(0f);
        }

        void Awake()
        {
            anim = GetComponent<Animator>();
            defaultDamage = 25f;
        }
    }
}
