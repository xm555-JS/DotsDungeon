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

    public void CoolTimeSetting(float coolTime)
    {
        isPlaying = true;
        SetAlphaColor(0.8f);
        StartCoroutine(StartCoolTime(coolTime));
    }

    IEnumerator StartCoolTime(float coolTime)
    {
        // ��Ÿ�� �̹����� raycastTarget�� true�� ����� ��ų�� ������� ���ϰ���.
        this.gameObject.GetComponent<Image>().raycastTarget = true;

        // ��Ÿ�� �̹����� fillAmount ����
        float fillingTime = 0f;
        while (fillingTime < coolTime)
        {
            fillingTime += Time.deltaTime;

            float fillAmount = 1f - (fillingTime / coolTime);
            this.gameObject.GetComponent<Image>().fillAmount = fillAmount;
            yield return null;
        }

        // ���� ���·� ����
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