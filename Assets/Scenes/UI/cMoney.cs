using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cMoney : MonoBehaviour
{
    public RectTransform rectTransform;

    private Text text;
    private int money;
    private float preMoney;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        money = GameManager.instance.money;
        text.text = money.ToString();
        SetPos();
        preMoney = money;
    }

    void Update()
    {
        money = GameManager.instance.money;

        if (preMoney != money)
        {
            text.text = money.ToString();
            SetPos();
        }
        preMoney = money;
    }

    void SetPos()
    {
        int log10 = (int)Mathf.Log10(money);
        Debug.Log(log10);
        float posX = 81f - (6f * (float)log10);

        Vector3 curentPos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector3(posX, curentPos.y, curentPos.z);
    }
}
