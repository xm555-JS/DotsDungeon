using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cOrc : cMonster
{
    public GameObject attackCollider;

    void Start()
    {
        Initiallize();
        attackCollider.SetActive(false);
    }

    void Initiallize()
    {
        // 몬스터 기본 Info
        speed = 2f;
        hp = 100f;
        maxHp = 100f;
        distance = 0.5f;
    }

    protected override void Update()
    {
        base.Update();
        SetAttackCollider();
        Attack(distance);
    }

    protected override void Attack(float distance)
    {
        base.Attack(distance);
        if (isAttack)
            attackCollider.SetActive(true);
        else
            attackCollider.SetActive(false);
    }

    void SetAttackCollider()
    {
        attackCollider.transform.position = this.transform.position + new Vector3(0f,0.3f,0f);
    }
}
