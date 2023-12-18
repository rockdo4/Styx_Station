using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabManager : MonoBehaviour
{
    public List<LabMainVertex> Re001_Vertex;
    public List<LabMainEdge> Re001_Edge;
    public GameObject Re001_Clear;
    public float maxTimer;
    public float timer;
    public bool isResearching;
    private int level = -1;
    private LabType labType;

    private void Start()
    {
        IsClearRe001();
    }

    private void Update()
    {
        if(isResearching)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                isResearching = false;
                maxTimer = 0f;
                timer = 0f;
                IsDoneTime();
            }
        }
    }
    private void IsDoneTime()
    {
        switch (labType)
        {
            case LabType.LabPower1:
                Re001_Vertex[level].GetClear(true);
                break;
            case LabType.LabHp1:
                break;
            case LabType.LabCriticalPower:
                break;
            case LabType.LabSliverUp:
                break;
            case LabType.LabPower2:
                break;
            case LabType.LabHp2:
                break;
        }
        level = -1;
        labType = LabType.None;
        foreach (var edge in Re001_Edge)
        {
            edge.VertexClearCheck();
        }
    }
    public void StartResearching(float timer , LabType labType,int index)
    {
        this.labType = labType;
        maxTimer =timer;
        this.timer= timer;
        isResearching = true;
        level = index;
        foreach (var vertex in Re001_Vertex)
        {
            vertex.GetButton().interactable =false;
        }

        switch(labType)
        {
            case LabType.LabPower1:
                Re001_Vertex[level].GetButton().interactable = true;
                break;
            case LabType.LabHp1:
                break;
            case LabType.LabCriticalPower:
                break;
            case LabType.LabSliverUp:
                break;
            case LabType.LabPower2:
                break;
            case LabType.LabHp2:
                break;
        }
    }

    public void IsClearRe001()
    {
        int Re001_Counting = 0;
        for (int i = 0; i < Re001_Vertex.Count; i++)
        {
            if (Re001_Vertex[i].isClear)
            {
                Re001_Counting++;
            }
        }

        if (Re001_Counting >= Re001_Vertex.Count)
            Re001_Clear.SetActive(true);
        else
            Re001_Clear.SetActive(false);
    }
}
