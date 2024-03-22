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

    // �ߺ�üũ ������ Ȯ�ο�
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
        // ���Կ� ���� ��ų�� �������� ��.
        if (slot_Image.sprite == selected_Image.sprite)
            return;

        // �ٸ� ���Կ� �ִ� ��ų�̶� ������ ��ų�� ���� ��.(���� ü����)
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
            // ��ų �ٲ� ��.
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

        /* ***�߰����� ��ų�� ����� �� ���� �ڵ带 �߰��� �ּ���*** */
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

        /* ***�߰����� ��ų�� ����� �� ���� �ڵ带 �߰��� �ּ���*** */
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

