using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSkillTime : MonoBehaviour
{
    public GameObject[] skills;
    public Image[] skillImages;

    //bool[] isCoolTimes = {false, false };
    float[] coolTime = { 3f, 7f };

    void Awake()
    {
        // �ʹ� ��ų ��Ÿ��ǥ�� �̹����� ��Ȱ��ȭ
        for (int i = 0; i < skills.Length; i++)
        {
            skillImages[i].gameObject.SetActive(false);
        }
    }

    public void CoolTimeSetting(Image skill)
    {
        /* ***�߰����� ��ų�� ����� �� ���� �ڵ带 �߰��� �ּ���*** */

        //if (skill.sprite.name == "Fire_Shoot")

        //if (!isCoolTimes[skillNum])
        //{
        //    skillImages[skillNum].gameObject.SetActive(true);
        //    isCoolTimes[skillNum] = true;
        //    StartCoroutine(StartCoolTime(skillNum));
        //}
    }

    IEnumerator StartCoolTime(int skillNum)
    {
        float elapsedTime = 0f;
        while (elapsedTime < coolTime[skillNum])
        {
            elapsedTime += Time.deltaTime;

            float fillAmount = 1f - (elapsedTime / coolTime[skillNum]);
            skillImages[skillNum].fillAmount = fillAmount;
            yield return null;
        }
        //isCoolTimes[skillNum] = false;
        skillImages[skillNum].fillAmount = 1f;
        skillImages[skillNum].gameObject.SetActive(false);
    }
}