using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFire_Ball : MonoBehaviour
{  
    private Rigidbody2D rigid;
    private GameObject owner;

    private Vector2 dirVec;
    private float maxScale;
    private float currentScale;
    private float speed;
    private bool readyForShoot;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        maxScale = 1f;
        currentScale = 0.1f;
        speed = 3f;
        readyForShoot = false;
    }

    void Start()
    {
        // 방향 결정
        owner = GameObject.Find("Unit009");
        Transform[] ownerTrans = owner.GetComponentsInChildren<Transform>();
        dirVec = new Vector2(-ownerTrans[1].localScale.x, 0).normalized;
    }

    void Update()
    {
        GrowScale();
        Shoot();
    }

    void GrowScale()
    {
        currentScale += Time.deltaTime;
        if (currentScale >= maxScale)
        {
            currentScale = maxScale;
            readyForShoot = true;
        }

        this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    void Shoot()
    {
        if (readyForShoot)
            rigid.velocity = dirVec * speed;
    }
}
