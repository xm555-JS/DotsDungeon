using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSkillTime : MonoBehaviour
{
    Color color;

    void Awake()
    {
        color = this.gameObject.GetComponent<Image>().color;
    }

    public void CoolTimeSetting(Image image) // float coolTime
    {
        Debug.Log(image.sprite + "½ºÅ³ ÄðÅ¸ÀÓ Àû¿ëµÊ");
        //SetAlphaColor(0.8f);
        //StartCoroutine(StartCoolTime(coolTime));
    }

    IEnumerator StartCoolTime(float coolTime)
    {
        this.gameObject.GetComponent<Image>().raycastTarget = true;

        float elapsedTime = 0f;
        while (elapsedTime < coolTime)
        {
            elapsedTime += Time.deltaTime;

            float fillAmount = 1f - (elapsedTime / coolTime);
            this.gameObject.GetComponent<Image>().fillAmount = fillAmount;
            yield return null;
        }

        SetAlphaColor(0f);
        this.gameObject.GetComponent<Image>().fillAmount = 1f;
        this.gameObject.GetComponent<Image>().raycastTarget = false;
    }

    void SetAlphaColor(float alpha)
    {
        color.a = alpha;
        this.gameObject.GetComponent<Image>().color = color;
    }
}