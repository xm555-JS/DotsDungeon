using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cWindow_Skill : MonoBehaviour
{
    // 플레이어가 먹은 스킬 아이템을 원도우 창에 추가
    // 아이템이 추가되면 스킬칸이 활성화
    // 활성화가 되면서 스킬 스프라이트와 스킬 설명이 나옴

    public GameObject[] skills;
    public Image[] skillImage;
    public Text[] skillDesc;

    cSkillData skillData;
    cSkillData preSkillData;
    int count = 0;

    void Start()
    {
        
    }

    void Update()
    {
        skillData = GameManager.instance.player.returnData();

        if (skillData == null)
            return;

        if (preSkillData == skillData)
            return;

        // 활성화
        skills[count].SetActive(true);
        // 스프라이트
        skillImage[count].sprite = skillData.skillSprite;
        // 텍스트
        skillDesc[count].text = skillData.skillDesc;

        preSkillData = skillData;

        count++;

        // 이쪽 부분 수정 4개 따라라락 먹고 스킬창 열면 마지막에 먹었던거 하나만 띡 있네.
        // 그 다음 스킬을 4개 이상먹었을 때 다음 페이지로 넘어가게해야함.
        // 지금 이미지들 다 버튼 추가해야함.
    }
}
