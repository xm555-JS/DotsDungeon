using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cWindow_Skill : MonoBehaviour
{
    // �÷��̾ ���� ��ų �������� ������ â�� �߰�
    // �������� �߰��Ǹ� ��ųĭ�� Ȱ��ȭ
    // Ȱ��ȭ�� �Ǹ鼭 ��ų ��������Ʈ�� ��ų ������ ����

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

        // Ȱ��ȭ
        skills[count].SetActive(true);
        // ��������Ʈ
        skillImage[count].sprite = skillData.skillSprite;
        // �ؽ�Ʈ
        skillDesc[count].text = skillData.skillDesc;

        preSkillData = skillData;

        count++;

        // ���� �κ� ���� 4�� ������ �԰� ��ųâ ���� �������� �Ծ����� �ϳ��� �� �ֳ�.
        // �� ���� ��ų�� 4�� �̻�Ծ��� �� ���� �������� �Ѿ���ؾ���.
        // ���� �̹����� �� ��ư �߰��ؾ���.
    }
}
