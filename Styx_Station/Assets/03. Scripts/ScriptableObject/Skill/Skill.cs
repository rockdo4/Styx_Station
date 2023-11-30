using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Skills/Skill")]
public class Skill : ScriptableObject
{
    [Tooltip("스킬의 이름")]
    public string Skill_Name;

    [Tooltip("스킬 타입")]
    public SkillType Skill_Type;

    [Tooltip("체인 스킬 여부")]
    public SkillType_2 Skill_Type_2;

    [Tooltip("스킬 등급")]
    public Tier Skill_Tier;

    [Tooltip("공격력 증가 배수")]
    public float Skill_ATK;

    [Tooltip("스킬 재사용 대기시간")]
    public float Skill_Cool;

    [Tooltip("최대 레벨")]
    public int Skill_MAX_LV;

    [Tooltip("레벨 당 공격력 배수 증가치")]
    public float Skill_ATK_LVUP;

    [Tooltip("공격 타수")]
    public int Skill_ATK_NUM;

    [Tooltip("레벨업 필요 스킬 갯수")]
    public List<int> Skill_LVUP_NU = new List<int>();

    [Tooltip("보유 효과 수치")]
    public float Skill_RE_EFF;

    [Tooltip("레벨 당 보유 효과 증가치")]
    public float Skill_RE_LVUP;

    [Tooltip("스킬 시작 위치")]
    public SkillStartPos Skill_Start_Type;

    [Tooltip("스킬 사정거리")]
    public float Skill_Range;

    [Tooltip("투사체 스킬 속도")]
    public float Skill_Speed;

    [Tooltip("스킬 범위")]
    public float Skill_EXT;

    [Tooltip("지속 시간")]
    public float Skill_Du;
}
