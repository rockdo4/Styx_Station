using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Skills/Skill")]
public class Skill : ScriptableObject
{
    public string Skill_Name;

    public SkillType Skill_Type;

    public Tier Skill_Tier;

    public float Skill_ATK;

    public float Skill_Cool;

    public int Skill_MAX_LV;

    public float Skill_ATK_LVUP;

    public int Skill_ATK_NUM;

    public List<int> Skill_LVUP_NU = new List<int>();

    public List<Skill_RE> Skill_Res = new List<Skill_RE>();

    [System.Serializable]
    public class Skill_RE
    {
        public AddOptionString Skill_RE_Option;

        public float Skill_RE_EFF;

        public float Skill_RE_LVUP;
    }

    public int Skill_Start_Pos;

    public float Skill_Range;

    public float Skill_Speed;

    public float Skill_EXT;

    public float Skill_Du;

    public float Skill_Stun;

    public Sprite image;
}
