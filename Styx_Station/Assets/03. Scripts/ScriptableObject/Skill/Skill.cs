using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Skills/Skill")]
public class Skill : ScriptableObject
{
    [Tooltip("��ų�� �̸�")]
    public string Skill_Name;

    [Tooltip("��ų Ÿ��")]
    public SkillType Skill_Type;

    [Tooltip("ü�� ��ų ����")]
    public SkillType_2 Skill_Type_2;

    [Tooltip("��ų ���")]
    public Tier Skill_Tier;

    [Tooltip("���ݷ� ���� ���")]
    public float Skill_ATK;

    [Tooltip("��ų ���� ���ð�")]
    public float Skill_Cool;

    [Tooltip("�ִ� ����")]
    public int Skill_MAX_LV;

    [Tooltip("���� �� ���ݷ� ��� ����ġ")]
    public float Skill_ATK_LVUP;

    [Tooltip("���� Ÿ��")]
    public int Skill_ATK_NUM;

    [Tooltip("������ �ʿ� ��ų ����")]
    public List<int> Skill_LVUP_NU = new List<int>();

    [Tooltip("���� ȿ�� ��ġ")]
    public float Skill_RE_EFF;

    [Tooltip("���� �� ���� ȿ�� ����ġ")]
    public float Skill_RE_LVUP;

    [Tooltip("��ų ���� ��ġ")]
    public SkillStartPos Skill_Start_Type;

    [Tooltip("��ų �����Ÿ�")]
    public float Skill_Range;

    [Tooltip("����ü ��ų �ӵ�")]
    public float Skill_Speed;

    [Tooltip("��ų ����")]
    public float Skill_EXT;

    [Tooltip("���� �ð�")]
    public float Skill_Du;
}
