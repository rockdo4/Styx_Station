using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class VampireSurivalPlayerSkill : MonoBehaviour
{
    [SerializeField]
    public List<VampireSkillInfoDataType> vampireSurivalSkillInfos = new List<VampireSkillInfoDataType>();

    public List<VampireSkillInfoDataType> playerInventorySkillInfos = new List<VampireSkillInfoDataType>();
    public VamprieSurivalPlayerController player;
    private List<float> skillTimer= new List<float>();  
    private void Awake()
    {
        GetPlayerSkill(vampireSurivalSkillInfos[0]);
    }

    private void Update()
    {
       for(int i= 0;i<skillTimer.Count;i++)
        {
            skillTimer[i] += Time.time;
            if (skillTimer[i] >= playerInventorySkillInfos[0].coolTime)
            {
                skillTimer[i] = 0f;
                playerInventorySkillInfos[i].skillEvent?.Invoke();
            }
        }
    }

    public void GetPlayerSkill(VampireSkillInfoDataType skillData)
    {
        var skill = skillData;
        if(playerInventorySkillInfos.Count ==0)
        {
            skill.isUnLock = true;
            playerInventorySkillInfos.Add(skill);
            skillTimer.Add(0f);
            VamprieSurvialUiManager.Instance.GetSkillImage(skill.skillImage);
            return;
        }
        foreach(var inventory in playerInventorySkillInfos)
        {
            if(inventory.skillName != skill.skillName)
            {
                skill.isUnLock = true;
                playerInventorySkillInfos.Add(skill);
                skillTimer.Add(0f);
                VamprieSurvialUiManager.Instance.GetSkillImage(skill.skillImage);
                return;
            }
            else if(inventory.isUnLock)
            {
                var updateSkill = inventory;
                SkillUpgrade(updateSkill);
            }
        }

    }

    private void SkillUpgrade(VampireSkillInfoDataType skillData)
    {
        var skill = skillData;
        skill.damage += skill.levelUpBuffDebuffDamage;
        skill.speed = skill.levelUpBuffSpeed;
        skill.coolTime -= skill.levelUpBuffTimer;
        skill.range += skill.levelUpBuffRange;
        skill.debuffDamage +=skill.levelUpBuffDebuffDamage;
    }
}
