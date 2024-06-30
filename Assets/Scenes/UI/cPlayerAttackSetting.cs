using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cPlayerAttackSetting : MonoBehaviour
{
    public Button attackBtn;

    private void Awake()
    {
        GameObject obj = GameObject.Find("Player");
        attackBtn.onClick.AddListener(obj.GetComponentInChildren<cPlayer>().Attack);
    }
}
