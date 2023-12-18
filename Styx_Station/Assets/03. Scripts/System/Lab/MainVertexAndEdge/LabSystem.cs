using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabSystem : Singleton<LabSystem>
{
    public List<LabMainVertex> Re001_Vertex;
    public List<LabMainEdge> Re001_Edge;
    public GameObject Re001_Clear;

    public int maxTimerTic;
    public int timerTic;
    [HideInInspector] public int milSeconds = 1000;
    public bool isResearching;
    [HideInInspector]public int level = -1;
    [HideInInspector]public LabType labType;

    private void Start()
    {
        IsClearRe001();
    }

    private void Update()
    {
        if(isResearching)
        {
            timerTic -= (int)(Time.deltaTime* milSeconds);
            if (timerTic <= 0)
            {
                isResearching = false;
                maxTimerTic = 0;
                timerTic = 0;
                IsDoneTime();
            }
            if(level ==9)
            {
                Debug.Log(timerTic);
            }
        }
    }
    private void IsDoneTime()
    {
        switch (labType)
        {
            case LabType.LabPower1:
                Re001_Vertex[level].GetClear(true);
                if(level ==9)
                    IsClearRe001(); 
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
    public void StartResearching(int timer , LabType labType,int index)
    {
        this.labType = labType;
        timer *= milSeconds;
        maxTimerTic =timer;
        this.timerTic= maxTimerTic;
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
                Re001_Vertex[level].coolTime.transform.position = Re001_Vertex[level].transform.position;
             
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
