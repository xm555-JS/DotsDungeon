using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Object/SkillData")]
public class cSkillData : ScriptableObject
{
    [Header("Skill Info")]
    public string skillType;
    public string skillName;
    public string skillDesc;
    public Sprite skillSprite;
    public float skillCoolTime;
    public float[] skillDamage = new float[10]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public float[] skillDot = new float[10] { 2f, 2.5f, 3f, 3.5f, 4f, 4.5f, 5f, 5.5f, 6f, 7f };
}
