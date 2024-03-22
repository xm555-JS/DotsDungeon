using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSetting_Slot : MonoBehaviour
{
    public Button otherSlot;
    public Sprite emptySprite;
    public GameObject coolTime;

    Button skillSlot;

    Image slot_Image;
    Image selected_Image;
    Image otherSlot_Image;

    // 중복체크 중인지 확인용
    bool isDuplicateCheck;

    void Awake()
    {
        skillSlot = this.GetComponent<Button>();
        //coolTime.GetComponent<cSkillTime>().CoolTimeSetting(0);
    }

    public void OnSkillClick()
    {
        if (GameManager.instance.player.GetisSkillSetting())
            GameManager.instance.player.SetisSkillSetting(false);

        GameObject selected_Skill = GameObject.Find("Selected_Skill(Clone)");
        if (selected_Skill == null)
            return;

        selected_Image = selected_Skill.GetComponent<Image>();
        slot_Image = this.GetComponent<Image>();

        Setting_Slot();
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
            btn.onClick.AddListener(GameManager.instance.player.Fire_Shoot);
        else if (image.sprite.name == "Ice_Shoot")
            btn.onClick.AddListener(GameManager.instance.player.Ice_Shoot);
        else if (image.sprite.name == "Heal")
            btn.onClick.AddListener(GameManager.instance.player.Heal);
        else if (image.sprite.name == "Poision_Shoot")
            btn.onClick.AddListener(GameManager.instance.player.Poision_Shoot);
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

        /* ***추가적인 스킬이 생기면 이 곳에 코드를 추가해 주세요*** */
        if (image.sprite.name == "Fire_Shoot")
            btn.onClick.RemoveListener(GameManager.instance.player.Fire_Shoot);
        else if (image.sprite.name == "Ice_Shoot")
            btn.onClick.RemoveListener(GameManager.instance.player.Ice_Shoot);
        else if (image.sprite.name == "Heal")
            btn.onClick.RemoveListener(GameManager.instance.player.Heal);
        else if (image.sprite.name == "Poision_Shoot")
            btn.onClick.RemoveListener(GameManager.instance.player.Poision_Shoot);
    }
}

