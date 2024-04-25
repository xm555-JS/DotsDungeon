using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class ClientState : MonoBehaviour
    {
        private BossController _bossController;
        private GameObject BossArea;

        private float time = 0.5f;

        void Start()
        {
            _bossController = (BossController)FindObjectOfType(typeof(BossController));
            BossArea = GameObject.Find("Boss_Area");
        }

        void Update()
        {
            if (!BossArea.GetComponent<cMonsterChase>().GetisChase())
                return;

            int randomNum = 0;

            Vector2 dir = new Vector2(-transform.localScale.x, 0).normalized;
            int layerMast = 1 << LayerMask.NameToLayer("Player");

            Debug.DrawRay(transform.position, new Vector2(-transform.localScale.x, 0) / 2f, Color.yellow);
            if (Physics2D.Raycast(transform.position, dir, 1f, layerMast))
            {
                time += Time.deltaTime;

                if (time >= 0.5f)
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
                _bossController.ChaseState();
        }

        // 요기서 해당 애니메이션이 끝났을 때 다음 state로 넘어갈 수 있도록 해준다면?
    }
}

