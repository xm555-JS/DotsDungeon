using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSelected_Skill : MonoBehaviour
{
    BoxCollider2D boxColider;

    void Awake()
    {
        boxColider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        // ���콺 -> ��ũ����ǥ
        // UI -> ��ũ����ǥ
        Vector3 mousePos = Input.mousePosition;
        transform.position = mousePos;

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
            Debug.Log("������");
        }
    }
}
