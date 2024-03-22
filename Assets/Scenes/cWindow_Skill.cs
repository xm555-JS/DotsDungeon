using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cWindow_Skill : MonoBehaviour
{
    public GameObject[] skills;
    public GameObject emptySlot;

    public GameObject selected_SkillIcon;

    private List<List<Sprite>> skillSprite = new List<List<Sprite>>();
    private List<List<string>> skillDesc = new List<List<string>>();

    private Image currentImage;

    cSkillData skillData;
    cSkillData preSkillData;

    int count = 0;
    int slotCount = 0;

    int page = 0;
    int currentPage = 0;

    static int fullSlot = 4;

    void Update()
    {
        Activate_Skill();
    }

    private void Activate_Skill()
    {
        skillData = GameManager.instance.player.returnData();

        if (skillData == null)
            return;
        if (preSkillData == skillData)
            return;

        preSkillData = skillData;

        count++;

        if (skillSprite.Count <= page)
            skillSprite.Add(new List<Sprite>());
        if (skillDesc.Count <= page)
            skillDesc.Add(new List<string>());

        if (skillSprite[page].Count >= 4)
        {
            page++;

            if (skillSprite.Count <= page)
                skillSprite.Add(new List<Sprite>());
            if (skillDesc.Count <= page)
                skillDesc.Add(new List<string>());
        }
            

        skillSprite[page].Add(skillData.skillSprite);
        skillDesc[page].Add(skillData.skillDesc);

        slotCount = count - (page * fullSlot);

        if (currentPage != page)
            return;
        for (int i = 0; i < slotCount; i++)
        {
            Image skillImage = skills[i].GetComponentInChildren<Image>();
            if (!skillImage)
                return;
            skillImage.sprite = skillSprite[page][i];

            Text skillText = skills[i].GetComponentInChildren<Text>();
            if (!skillText)
                return;
            skillText.text = skillDesc[page][i];
        }
    }

    public void Next_Window_Skill()
    {
        // ���� �������� �Ѿ �� �ִ��� Ȯ��
        if (currentPage >= page)
            return;

        currentPage++;

        Init_SkillSlot();
        
        // ���� �������� ��������Ʈ�� �ٲٱ�
        for (int i = 0; i < skillSprite[currentPage].Count; i++)
        {
            Image skillImage = skills[i].GetComponentInChildren<Image>();
            if (!skillImage)
                return;
            skillImage.sprite = skillSprite[currentPage][i];

            Text skillText = skills[i].GetComponentInChildren<Text>();
            if (!skillText)
                return;
            skillText.text = skillDesc[currentPage][i];
        }
    }

    public void Pre_Window_Skill()
    {
        if (currentPage <= 0)
            return;

        currentPage--;

        Init_SkillSlot();

        // ���� �������� ��������Ʈ�� �ٲٱ�
        for (int i = 0; i < skillSprite[currentPage].Count; i++)
        {
            Image skillImage = skills[i].GetComponentInChildren<Image>();
            if (!skillImage)
                return;
            skillImage.sprite = skillSprite[currentPage][i];

            Text skillText = skills[i].GetComponentInChildren<Text>();
            if (!skillText)
                return;
            skillText.text = skillDesc[currentPage][i];
        }
    }

    void Init_SkillSlot()
    {
        // �⺻�̹����� ��ų ���� �ٲٱ�
        for (int i = 0; i < fullSlot; i++)
        {
            Image skillImage = skills[i].GetComponentInChildren<Image>();
            skillImage.sprite = emptySlot.GetComponentInChildren<Image>().sprite;// �⺻ �̹�����

            Text skillText = skills[i].GetComponentInChildren<Text>();
            skillText.text = emptySlot.GetComponentInChildren<Text>().text;// �⺻ �̹�����
        }
    }

    public void Selected_SkillIcon(GameObject slot)
    {
        GameObject obj = Instantiate(selected_SkillIcon);

        Image selected_Skill_Image = obj.GetComponent<Image>();
        selected_Skill_Image.sprite = slot.GetComponent<Image>().sprite;
        obj.transform.SetParent(this.transform);
        obj.transform.localPosition = Vector3.zero;
        Debug.Log("��ų ���õ�");
    }
}
