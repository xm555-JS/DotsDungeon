using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class cSetting_Slot : MonoBehaviour
{
    public Button otherSlot;
    public Sprite emptySprite;
    public GameObject coolTime;
    public cSkillData[] skillData;

    Button skillSlot;

    Image slot_Image;
    Image selected_Image;
    Image otherSlot_Image;

    // 중복체크 중인지 확인용
    bool isDuplicateCheck;

    void Awake()
    {
        skillSlot = this.GetComponent<Button>();
    }

    public void OnSkillClick()
    {
        if (GameManager.instance.player.GetisSkillSetting())
            GameManager.instance.player.SetisSkillSetting(false);

        GameObject selected_Skill = GameObject.Find("Selected_Skill(Clone)");
        if (selected_Skill == null)
        {
            Debug.Log("cSetting_Slot - selected_Skill가 없습니다.");
            return;
        }

        if (SlotCheck())
            return;

        selected_Image = selected_Skill.GetComponent<Image>();
        slot_Image = this.GetComponent<Image>();

        Setting_Slot();

        Destroy(selected_Skill.gameObject);
    }

    void Setting_Slot()
    {
        // 슬롯에 같은 스킬을 설정했을 때.
        if (slot_Image.sprite == selected_Image.sprite)
            return;

        // 다른 슬롯에 있는 스킬이랑 선택한 스킬이 같을 때.(슬롯 체인지)
        otherSlot_Image = otherSlot.GetComponent<Image>();
        if (selected_Image.sprite == otherSlot_Image.sprite)
        {
            isDuplicateCheck = true;

            GameManager.instance.player.SetisSkillSetting(true);
            Remove_Skill();

            if (slot_Image.sprite.name == "Board_20x20")
                otherSlot_Image.sprite = emptySprite;
            else
            {
                otherSlot_Image.sprite = slot_Image.sprite;
                Add_Skill();
            }

            isDuplicateCheck = false;
            Change_Slot();
        }
        else
        {
            // 스킬 바꿀 때.
            GameManager.instance.player.SetisSkillSetting(true);
            Change_Slot();
        }
    }

    void Change_Slot()
    {
        Remove_Skill();
        slot_Image.sprite = selected_Image.sprite;
        Add_Skill();
    }

    void Add_Skill()
    {
        Button btn = null;
        Image image = null;

        if (isDuplicateCheck)
        {
            btn = otherSlot;
            image = otherSlot_Image;
        }
        else
        {
            btn = skillSlot;
            image = slot_Image;
        }

        /* ***추가적인 스킬이 생기면 이 곳에 코드를 추가해 주세요*** */
        if (image.sprite.name == "Fire_Shoot")
        {
            float returnCoolTime = SetCoolTime(image);
            GameObject btnCoolTIme = btn.GetComponent<cSetting_Slot>().coolTime;

            btn.onClick.AddListener(GameManager.instance.player.Fire_Shoot);
            btn.onClick.AddListener(() => btnCoolTIme.GetComponent<cSkillTime>().CoolTimeSetting(returnCoolTime));
        }
        else if (image.sprite.name == "Ice_Shoot")
        {
            float returnCoolTime = SetCoolTime(image);
            GameObject btnCoolTIme = btn.GetComponent<cSetting_Slot>().coolTime;

            btn.onClick.AddListener(GameManager.instance.player.Ice_Shoot);
            btn.onClick.AddListener(() => btnCoolTIme.GetComponent<cSkillTime>().CoolTimeSetting(returnCoolTime));
        }
        else if (image.sprite.name == "Heal")
        {
            float returnCoolTime = SetCoolTime(image);
            GameObject btnCoolTIme = btn.GetComponent<cSetting_Slot>().coolTime;

            btn.onClick.AddListener(GameManager.instance.player.Heal);
            btn.onClick.AddListener(() => btnCoolTIme.GetComponent<cSkillTime>().CoolTimeSetting(returnCoolTime));
        }
        else if (image.sprite.name == "Poision_Shoot")
        {
            float returnCoolTime = SetCoolTime(image);
            GameObject btnCoolTIme = btn.GetComponent<cSetting_Slot>().coolTime;

            btn.onClick.AddListener(GameManager.instance.player.Poision_Shoot);
            btn.onClick.AddListener(() => btnCoolTIme.GetComponent<cSkillTime>().CoolTimeSetting(returnCoolTime));
        }
    }

    void Remove_Skill()
    {
        Button btn = null;
        Image image = null;

        if (isDuplicateCheck)
        {
            btn = otherSlot;
            image = otherSlot_Image;
        }
        else
        {
            btn = skillSlot;
            image = slot_Image;
        }

        btn.onClick.RemoveAllListeners();
    }

    float SetCoolTime(Image skillImage)
    {
        float coolTime = 0f;
        bool isFound = false;
        foreach (cSkillData skill in skillData)
        {
            if (skillImage.sprite == skill.skillSprite)
            {
                if (GameManager.instance.coolTime > 0)
                {
                    Debug.Log("CoolTime : " + skill.skillCoolTime);
                    coolTime = skill.skillCoolTime - (skill.skillCoolTime * (GameManager.instance.coolTime / 100f));
                    Debug.Log("CoolTime : " + coolTime);
                }
                    
                else
                    coolTime = skill.skillCoolTime;

                isFound = true;
            }
        }

        if (!isFound)
            Debug.Log("스킬 쿨타임 설정 오류!");

        return coolTime;
    }

    bool SlotCheck()
    {
        // 쿨타임일 때 스킬을 바꾸지 못하게 check
        Image image = otherSlot.GetComponent<Image>();
        Image Select_Image = GameObject.Find("Selected_Skill(Clone)").GetComponent<Image>();
        GameObject obj = otherSlot.GetComponent<cSetting_Slot>().coolTime;

        bool chk = false;
        if (image.sprite == Select_Image.sprite)
            chk = obj.GetComponent<cSkillTime>().isPlaying;

        return chk;
    }
}
