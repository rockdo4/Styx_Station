using System.Collections.Generic;
using UnityEngine;


public class VampireSurivalPlayerSkill : MonoBehaviour
{
    [SerializeField]
    public List<VampireSkillInfoDataType> vampireSurivalSkillInfos = new List<VampireSkillInfoDataType>();

    public List<VampireSkillInfoDataType> playerInventorySkillInfos = new List<VampireSkillInfoDataType>();
    public VamprieSurivalPlayerController player;
    private List<float> skillTimer = new List<float>();
    private VampireSurivalSkillInfo skillInfoAction;
    private void Awake()
    {
        skillInfoAction = GetComponent<VampireSurivalSkillInfo>();
        GetPlayerSkill(vampireSurivalSkillInfos[0]);
    }

    private void Update()
    {
        for (int i = 0; i < skillTimer.Count; i++)
        {
            skillTimer[i] += Time.deltaTime;
            if (skillTimer[i] >playerInventorySkillInfos[i].coolTime)
            {
                skillTimer[i] = 0f;
                playerInventorySkillInfos[i].skillEvent();
            }
        }
    }

    public void GetPlayerSkill(VampireSkillInfoDataType skillData)
    {
        var skill = skillData;
        if (playerInventorySkillInfos.Count == 0)
        {
            SkillACtionAdd(skill);
            VamprieSurvialUiManager.Instance.GetSkillImage(skill.skillImage);
            return;
        }
        foreach (var inventory in playerInventorySkillInfos)
        {
            if (inventory.skillName != skill.skillName)
            {
                SkillACtionAdd(skill);
                VamprieSurvialUiManager.Instance.GetSkillImage(skill.skillImage);
                return;
            }
            else if (inventory.isUnLock && inventory.skillName == skill.skillName)
            {
                var updateSkill = inventory;
                SkillUpgrade(updateSkill,inventory);
            }
        }
    }

    private void SkillUpgrade(VampireSkillInfoDataType skillData, VampireSkillInfoDataType currentSkill)
    {
        var skill = skillData;
        skill.damage += skill.levelUpBuffDebuffDamage;
        skill.speed = skill.levelUpBuffSpeed;
        skill.coolTime -= skill.levelUpBuffTimer;
        skill.range += skill.levelUpBuffRange;
        skill.debuffDamage += skill.levelUpBuffDebuffDamage;
        currentSkill = skillData;
    }

    private void SkillACtionAdd(VampireSkillInfoDataType skillData)
    {
        skillInfoAction.skillData = skillData;
        skillData.isUnLock = true;
        switch (skillData.currentSkillType)
        {
            case VampireSkillType.TripleArrowShot:
                skillData.skillEvent = skillInfoAction.TripleArrowShotAction;
                break;
        }
        playerInventorySkillInfos.Add(skillData);
        skillTimer.Add(0f);
    }
}
