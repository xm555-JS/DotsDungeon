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
    //해당 스킬의 이름, 스프라이트, 쿨타임 스프라이트, 쿨타임 시간, 설명, 속성 등

}
