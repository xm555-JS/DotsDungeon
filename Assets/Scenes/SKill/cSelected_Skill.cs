using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSelected_Skill : MonoBehaviour
{
    void Update()
    {
        // ���콺 -> ��ũ����ǥ
        // UI -> ��ũ����ǥ
        //Vector3 mousePos = Input.mousePosition;
        //transform.position = mousePos;

        //if (Input.GetMouseButtonUp(0))
        //{
        //    Destroy(this.gameObject);
        //    Debug.Log("������");
        //}

#if UNITY_EDITOR
        // ���콺 -> ��ũ����ǥ (�����Ϳ����� ���)
        Vector3 inputPos = Input.mousePosition;
#else
        // ��ġ -> ��ũ����ǥ (����Ͽ��� ���)
        Vector3 inputPos = Vector3.zero;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPos = touch.position;
        }
#endif

        transform.position = inputPos;
    }
}
