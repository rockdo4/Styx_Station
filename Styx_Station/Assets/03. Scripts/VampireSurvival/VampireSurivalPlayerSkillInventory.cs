using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
