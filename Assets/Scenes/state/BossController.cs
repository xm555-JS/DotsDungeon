using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class BossController : MonoBehaviour
    {
        [Header("Normal_Skill")]
        public GameObject normalSkill;

        [Header("Sword_Skill")]
        public GameObject swordSkill;

        [Header("Magic_Skill")]
        public GameObject magicSkill;

        private IBossState
            _normalAttackState, _swordSkillState, _magicSkillState, _chaseState;

        private BossStateContext _bossStateContext;

        private Animator anim;

        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        void Start()
        {
            _bossStateContext = new BossStateContext(this);

            _normalAttackState = gameObject.AddComponent<NormalAttackState>();
            _swordSkillState = gameObject.AddComponent<SwordSkillState>();
            _magicSkillState = gameObject.AddComponent<MagicSkillState>();
            _chaseState = gameObject.AddComponent<ChaseState>();
        }

        #region Boss_State
        public void NormalAttackState()
        {
            _bossStateContext.Transition(_normalAttackState);
        }

        public void SwordSkillState()
        {
            _bossStateContext.Transition(_swordSkillState);
        }

        public void MagicSkillState()
        {
            _bossStateContext.Transition(_magicSkillState);
        }

        public void ChaseState()
        {
            _bossStateContext.Transition(_chaseState);
        }

        public void SetState(IBossState state)
        {
            _bossStateContext.Transition(state);
        }
        #endregion

        #region Boss_Effects

        public void Normal_Boss_Effect()
        {
            SetEffectPos(normalSkill, new Vector2(-0.8f, 0.5f));
        }

        public void Sword_Boss_Effect()
        {
            SetEffectPos(swordSkill, new Vector2(-0.8f, 0f));
        }

        public void Magic_Boss_Effect()
        {
            SetEffectPos(magicSkill, new Vector2(-0.8f, 0.5f));
        }

        void SetEffectPos(GameObject skill, Vector2 pos)
        {
            float posX = pos.x;

            GameObject obj = Instantiate(skill);
            obj.transform.SetParent(this.transform);

            float scaleX = obj.transform.localScale.x;
            if (this.transform.localScale.x < 0f)
            {
                posX *= -1f;
                scaleX *= -1f;
            }
            
            obj.transform.position = this.transform.position + new Vector3(posX, pos.y, 0f);
            obj.transform.localScale = new Vector3(scaleX, 1f, 1f);
        }
        #endregion

        #region Boss_AnimEnd
        private void BossAnimEnd_isSwordSKill()
        {
            anim.SetTrigger("isBossSwordSKill");
        }

        private void BossAnimEnd_isMagicSKill()
        {
            anim.SetTrigger("isBossMagicSkill");
        }

        private void BossAnimEnd_isNormalAttack()
        {
            anim.SetTrigger("isBossNormalAttack");
        }
        #endregion
    }
}

