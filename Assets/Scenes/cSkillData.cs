using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Object/SkillData")]
public class cSkillData : ScriptableObject
{
    public enum SkillType { NORMAL, FIRE, ICE, POISION, HEAL }

    [Header("Skill Info")]
    public SkillType skillType;
    public string skillName;
    public string skillDesc;
    public Sprite skillSprite;
    public float skillCoolTime;
    //�ش� ��ų�� �̸�, ��������Ʈ, ��Ÿ�� ��������Ʈ, ��Ÿ�� �ð�, ����, �Ӽ� ��

}
