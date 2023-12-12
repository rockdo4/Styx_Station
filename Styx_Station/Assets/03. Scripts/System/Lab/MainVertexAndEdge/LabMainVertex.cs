using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabMainVertex : MonoBehaviour
{
    public List<LabMainEdge> edges = new List<LabMainEdge>();
    public string iD;
    public bool isClear;

    protected Color noneActive = new Color(1, 1, 1, 0.3f);
    protected Color assignedActive = Color.white;


    public void SetEdgesImageColor()
    {
        if(isClear)
        {
            SetAssignedAcitve();
        }
        else
        {
            SetNoneActive();
        }
    }

    protected void SetNoneActive()
    {
        foreach (var v in edges)
        {
            if (!isClear)
            {
                var image = v.GetComponent<Image>();
                image.color = noneActive;
            }
        }
    }

    protected void SetAssignedAcitve()
    {
        foreach (var v in edges)
        {
            if (isClear)
            {
                var image = v.GetComponent<Image>();
                image.color = assignedActive;
            }
        }
    }
}
