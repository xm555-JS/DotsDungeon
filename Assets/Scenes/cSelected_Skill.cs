using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSelected_Skill : MonoBehaviour
{
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
