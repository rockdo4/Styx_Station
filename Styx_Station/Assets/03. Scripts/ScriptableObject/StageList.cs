
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/StageList")]
public class StageList : ScriptableObject
{
    [SerializeField]
    private List<Stage> table = new List<Stage>();

    public int GetTableSize()
    {
        return table.Count;
    }

    public Stage GetStage(int index)
    {
        return table[index];
    }

    public Stage GetStageByStageIndex(int stageIndex)
    {
        for(int i = 0; i<table.Count;i++)
        {
            if (table[i].index == stageIndex)
            {
                return table[i];
            }
        }
        return null;
    }
}
