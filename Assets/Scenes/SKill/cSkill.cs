using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSkill : MonoBehaviour
{
    public GameObject[] skills;
    List<string> nameList = new List<string>();
    GameObject useSkill;

    public void SkillInstantiate(string skillName)
    {
        Debug.Log("스킬 이펙트 사용");

        GameObject _skill = FindSkill(skillName);
        if (!_skill)
        {
            Debug.Log("스킬을 발견하지 못하였습니다.");
            return;
        }

        useSkill = Instantiate(_skill,
                                        GameManager.instance.player.transform.position,
                                        Quaternion.Euler(0, 0, 0),
                                        this.transform);
        nameList.Add(skillName);
        SetPosition(skillName);
    }

    GameObject FindSkill(string skillName)
    {
        foreach (GameObject skill in skills)
        {
            if (skill.name == skillName)
                return skill;
        }
        return null;
    }

    void SetPosition(string skillName)
    {
        if (skillName == "Heal")
        {
            useSkill.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            StartCoroutine("ChasePlayer");
        }
        else
        {
            if (GameManager.instance.player.transform.localScale.x <= 0f)
            {
                useSkill.transform.localScale = new Vector3(1f, 1f, 1f);
                useSkill.transform.position += new Vector3(0.45f, 0.3f, 0f);
            }
            else
            {
                useSkill.transform.localScale = new Vector3(-1f, 1f, 1f);
                useSkill.transform.position += new Vector3(-0.45f, 0.3f, 0f);
            }
        }
    }

    public float GetEffectEndTime()
    {
        return useSkill.GetComponent<ParticleSystem>().duration;
    }

    IEnumerator ChasePlayer()
    {
        float time = 0f;
        float endTime = GetEffectEndTime();
        Debug.Log(endTime);
        while (time < endTime)
        {
            if (!useSkill)
                yield break;

            useSkill.transform.position = GameManager.instance.player.transform.position + new Vector3(0f,0.5f,0f);

            yield return null;

            time += Time.deltaTime;
        }
        Debug.Log("코루틴 끝");
    }
}

//bool isFinish = false;
//public bool GetisFinish() { return isFinish; }
//public void SetisFinish(bool _isFinish) { isFinish = _isFinish; }

//public void SkillActiveOff()
//{
//    StartCoroutine("skillDuration");
//}
//IEnumerator skillDuration()
//{
//    yield return new WaitForSeconds(useSkill.GetComponent<ParticleSystem>().startLifetime % 4f);
//    isFinish = false;
//    useSkill.SetActive(false);
//}

//foreach (string name in nameList)
//{
//    if (name == skillName)
//    {
//        useSkill = GameObject.Find(_skill.name + "(Clone)");
//        if (!useSkill)
//        {
//            Debug.Log("useSkill가 null입니다.");
//            return;
//        }
//        useSkill.transform.position = GameManager.instance.player.transform.position;
//        SetPosition();
//        useSkill.SetActive(true);
//        return;
//    }
//}