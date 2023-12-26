using System.Collections.Generic;
using UnityEngine;

public class VampireSurivalPlayerSkillInventory : MonoBehaviour
{

    public List<VampireSkillInfoDataType> playerInventory = new List<VampireSkillInfoDataType>();

    public void GetSkill(VampireSkillInfoDataType skillInfo)
    {
        
        playerInventory.Add(skillInfo);
    }

    private void Update()
    {
    }
}
