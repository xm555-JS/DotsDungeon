using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSkillTime : MonoBehaviour
{
    public bool isPlaying;
    Color color;

    void Awake()
    {
        color = this.gameObject.GetComponent<Image>().color;
    }

    public void CoolTimeSetting(float coolTime) // float coolTime
    {
        GameObject selected_Imaget = GameObject.Find("Selected_Skill(Clone)");
        if (selected_Imaget)
            return;

        Debug.Log(coolTime + "½ºÅ³ ÄðÅ¸ÀÓ Àû¿ëµÊ");
        isPlaying = true;
        SetAlphaColor(0.8f);
        StartCoroutine(StartCoolTime(coolTime));
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
        isPlaying = false;
    }

    void SetAlphaColor(float alpha)
    {
        color.a = alpha;
        this.gameObject.GetComponent<Image>().color = color;
    }
}