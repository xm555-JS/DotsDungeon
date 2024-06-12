using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cOrc : cMonster
{
    public GameObject attackCollider;

    void Start()
    {
        speed = 2f;
        hp = 100f;
        maxHp = 100f;
        distance = 0.5f;

        attackCollider.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        SetAttackCollider();
        Attack(distance);
    }

    protected override void Attack(float distance)
    {
        if (isAttack)
            return;

        base.Attack(distance);
        if (isAttack)
        {
            Debug.Log("�ڽĿ��� Attack�Լ� ����");
            attackCollider.SetActive(true);
        }
        else
            attackCollider.SetActive(false);
    }

    void SetAttackCollider()
    {
        attackCollider.transform.position = this.transform.position + new Vector3(0f,0.3f,0f);
    }
}
