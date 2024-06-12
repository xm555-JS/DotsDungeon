using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cJobSelcet : MonoBehaviour, IPointerEnterHandler
{
    public Text text;

    void Awake()
    {
        text = text.GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        text.text = "Warrior";
    }
}
