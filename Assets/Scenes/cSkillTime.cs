using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSkillTime : MonoBehaviour
{
    public GameObject[] skills;
    public Image[] skillImages;

    bool[] isCoolTimes = {false, false };
    float[] coolTime = { 3f, 7f };

    void Awake()
    {
        // 초반 스킬 쿨타임표시 이미지는 비활성화
        for (int i = 0; i < skills.Length; i++)
        {
            skillImages[i].gameObject.SetActive(false);
        }
    }

    public void CoolTimeSetting(int skillNum)
    {
        if (!isCoolTimes[skillNum])
        {
            skillImages[skillNum].gameObject.SetActive(true);
            isCoolTimes[skillNum] = true;
            StartCoroutine(StartCoolTime(skillNum));
        }
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
        isCoolTimes[skillNum] = false;
        skillImages[skillNum].fillAmount = 1f;
        skillImages[skillNum].gameObject.SetActive(false);
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class SkillTime : MonoBehaviour
//{
//    public List<GameObject> skills = new List<GameObject>();
//    public List<Image> skillImages = new List<Image>();
//    public List<bool> isCoolTimes = new List<bool>();
//    public List<float> coolTime = new List<float>();

//    void Awake()
//    {
//        // 초반 스킬 쿨타임표시 이미지는 비활성화
//        for (int i = 0; i < skills.Count; i++)
//        {
//            skillImages[i].gameObject.SetActive(false);
//            isCoolTimes[i] = false;
//        }
//    }

//    public void AddSkill(GameObject skill, Image skillImage, bool isCoolTime, float coolDown)
//    {
//        skills.Add(skill);
//        skillImages.Add(skillImage);
//        isCoolTimes.Add(isCoolTime);
//        coolTime.Add(coolDown);
//    }

//    public void CoolTimeSetting(int skillNum)
//    {
//        if (!isCoolTimes[skillNum])
//        {
//            skillImages[skillNum].gameObject.SetActive(true);
//            isCoolTimes[skillNum] = true;
//            StartCoroutine(StartCoolTime(skillNum));
//        }
//    }

//    IEnumerator StartCoolTime(int skillNum)
//    {
//        float elapsedTime = 0f;
//        while (elapsedTime < coolTime[skillNum])
//        {
//            elapsedTime += Time.deltaTime;

//            float fillAmount = 1f - (elapsedTime / coolTime[skillNum]);
//            skillImages[skillNum].fillAmount = fillAmount;
//            yield return null;
//        }
//        isCoolTimes[skillNum] = false;
//        skillImages[skillNum].fillAmount = 1f;
//        skillImages[skillNum].gameObject.SetActive(false);
//    }
//}