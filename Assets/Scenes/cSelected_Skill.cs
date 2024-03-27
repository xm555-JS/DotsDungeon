using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSelected_Skill : MonoBehaviour
{
    void Update()
    {
        // ¸¶¿ì½º -> ½ºÅ©¸°ÁÂÇ¥
        // UI -> ½ºÅ©¸°ÁÂÇ¥
        Vector3 mousePos = Input.mousePosition;
        transform.position = mousePos;

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
            Debug.Log("»èÁ¦µÊ");
        }
    }
}
