using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabMainEdge : MonoBehaviour
{
    public List<LabMainVertex> prevVertex=new List<LabMainVertex>();
    private bool isPrevVertexClear;
    public List<LabMainVertex> nextVertex=new List<LabMainVertex>();

    protected Color noneActive = new Color(1, 1, 1, 0.3f);
    protected Color assignedActive = Color.white;

    public void Start()
    {
        VertexClearCheck();
    }

    public void VertexClearCheck()
    {
       
        int counting = 0;
        for(int i=0;i<prevVertex.Count; i++) 
        {
            if (prevVertex[i]!=null && prevVertex[i].isClear) 
            {
                counting++;
            }
        }
        if(counting >=prevVertex.Count)
            isPrevVertexClear = true;
        else
            isPrevVertexClear = false;

        if (!isPrevVertexClear)
        {
            foreach (var vertex in nextVertex)
            {
                NextVertexNoneSetting(vertex);
                vertex.SetNoneActive();
            }
        }
        else
        {
            foreach (var vertex in nextVertex)
            {
                NextVertexAssignedSetting(vertex);
            }
        }
    }

    private void NextVertexNoneSetting(LabMainVertex vertex)
    {
        var image = vertex.GetComponent<Image>();
        image.color = noneActive;
        var button = vertex.GetComponent<Button>();
        button.interactable = false;
    }

    private void NextVertexAssignedSetting(LabMainVertex vertex)
    {

        var image = vertex.GetComponent<Image>();
        image.color = assignedActive;
        var button = vertex.GetComponent<Button>();
        button.interactable = true;
    }
}
