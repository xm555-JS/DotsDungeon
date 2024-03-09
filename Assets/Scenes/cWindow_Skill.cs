using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cWindow_Skill : MonoBehaviour
{
    public GameObject[] skills;
    private List<Sprite> skillSprite = new List<Sprite>();
    private List<string> skillDesc = new List<string>();

    cSkillData skillData;
    cSkillData preSkillData;
    int count = 0;

    void Update()
    {
        ActivateSkill();
    }

    private void ActivateSkill()
    {
        skillData = GameManager.instance.player.returnData();

        if (skillData == null)
            return;
        if (preSkillData == skillData)
            return;

        // 활성화
        skills[count].SetActive(true);
        // 스프라이트
        skillSprite.Add(skillData.skillSprite);
        // 텍스트
        skillDesc.Add(skillData.skillDesc);

        count++;
        for (int i = 0; i < count; i++)
        {
            Image skilSprite = skills[i].GetComponentInChildren<Image>();
            if (!skilSprite)
                return;
            skilSprite.sprite = skillSprite[i];


            Text skillText = skills[i].GetComponentInChildren<Text>();
            if (!skillText)
                return;
            skillText.text = skillDesc[i];
        }

        preSkillData = skillData;
    }
}
