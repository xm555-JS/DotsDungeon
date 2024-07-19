using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSelected_Skill : MonoBehaviour
{
    void Update()
    {
        // 마우스 -> 스크린좌표
        // UI -> 스크린좌표
        //Vector3 mousePos = Input.mousePosition;
        //transform.position = mousePos;

        //if (Input.GetMouseButtonUp(0))
        //{
        //    Destroy(this.gameObject);
        //    Debug.Log("삭제됨");
        //}

#if UNITY_EDITOR
        // 마우스 -> 스크린좌표 (에디터에서만 사용)
        Vector3 inputPos = Input.mousePosition;
#else
        // 터치 -> 스크린좌표 (모바일에서 사용)
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
