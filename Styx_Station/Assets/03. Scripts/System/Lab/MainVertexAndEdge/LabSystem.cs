using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabSystem : Singleton<LabSystem>
{
    public List<LabMainVertex> Re001_Vertex;
    public List<LabMainEdge> Re001_Edge;
    public GameObject Re001_Clear;

    public List<LabMainVertex> Re002_Vertex;
    public List<LabMainEdge> Re002_Edge;
    public GameObject Re002_Clear;

    public List<LabMainVertex> Re003_Vertex;
    public List<LabMainEdge> Re003_Edge;
    public GameObject Re003_Clear;

    public List<LabMainVertex> Re004_Vertex;
    public List<LabMainEdge> Re004_Edge;
    public GameObject Re004_Clear;

    public List<LabMainVertex> Re005_Vertex;
    public List<LabMainEdge> Re005_Edge;
    public GameObject Re005_Clear;

    public List<LabMainVertex> Re006_Vertex;
    public List<LabMainEdge> Re006_Edge;
    public GameObject Re006_Clear;

    public int maxTimerTic;
    public int timerTic;
    [HideInInspector] public int milSeconds = 1000;
    public bool isResearching;
    [HideInInspector] public int level = -1;
    [HideInInspector] public LabType labType;

    public StringTableData labStringTableName;
    public StringTableData labBuffStringTable;
    public LabTableDatas labTalbeData;

    public bool isTimerZero;
    private void Start()
    {
        IsClearRe001();
        IsClearRe002();
        IsClearRe003();
        IsClearRe004();
        IsClearRe005();
        IsClearRe006();
        AllCheckEdges();
    }

    private void Update()
    {
        if (isResearching && timerTic > 0)
        {
            timerTic -= (int)(Time.deltaTime * milSeconds);
            if (timerTic <= 0)
            {
                maxTimerTic = 0;
                timerTic = 0;
                var labComplete = UIManager.Instance.windows[(int)WindowType.Lab].GetComponent<LabWindow>();
                labComplete.labCompleteWindwo.Open();
                isTimerZero=true;
                switch (labType)
                {
                    case LabType.LabPower1:
                        Re001_Vertex[level].GetButton().interactable = false;
                        break;
                    case LabType.LabHp1:
                        Re002_Vertex[level].GetButton().interactable = false;
                        break;
                    case LabType.LabCriticalPower:
                        break;
                    case LabType.LabSliverUp:
                        break;
                    case LabType.LabPower2:
                        Re005_Vertex[level].GetButton().interactable = false;
                        break;
                    case LabType.LabHp2:
                        Re006_Vertex[level].GetButton().interactable = false;
                        break;
                }
            }
            if (level == 9)
            {
                Debug.Log(timerTic);
            }
        }
    }
    public void IsDoneTime()
    {
        switch (labType)
        {
            case LabType.LabPower1:
                Re001_Vertex[level].GetClear(true);
                if (level == 9)
                    IsClearRe001();
                break;
            case LabType.LabHp1:
                Re002_Vertex[level].GetClear(true);
                if (level == 9)
                    IsClearRe002();
                break;
            case LabType.LabCriticalPower:
                Re003_Vertex[level].GetClear(true);
                if (level == 9)
                    IsClearRe003();
                break;
            case LabType.LabSliverUp:
                Re004_Vertex[level].GetClear(true);
                if (level == 9)
                    IsClearRe004();
                break;
            case LabType.LabPower2:
                Re005_Vertex[level].GetClear(true);
                if (level == 9)
                    IsClearRe005();
                break;
            case LabType.LabHp2:
                Re006_Vertex[level].GetClear(true);
                if (level == 9)
                    IsClearRe006();
                break;
        }
        level = -1;
        labType = LabType.None;
        AllCheckEdges();
    }
    public void StartResearching(int timer, LabType labType, int index)
    {
        this.labType = labType;
        timer *= milSeconds;
        maxTimerTic = timer;
        this.timerTic = maxTimerTic;
        isResearching = true;
        level = index;
        AllButtonNull();
        switch (labType)
        {
            case LabType.LabPower1:
                Re001_Vertex[level].GetButton().interactable = true;
                Re001_Vertex[level].coolTime.transform.position = Re001_Vertex[level].transform.position;
                break;
            case LabType.LabHp1:
                Re002_Vertex[level].GetButton().interactable = true;
                Re002_Vertex[level].coolTime.transform.position = Re002_Vertex[level].transform.position;
                break;
            case LabType.LabCriticalPower:
                Re003_Vertex[level].GetButton().interactable = true;
                Re003_Vertex[level].coolTime.transform.position = Re002_Vertex[level].transform.position;
                break;
            case LabType.LabSliverUp:
                Re004_Vertex[level].GetButton().interactable = true;
                Re004_Vertex[level].coolTime.transform.position = Re004_Vertex[level].transform.position;
                break;
            case LabType.LabPower2:
                Re005_Vertex[level].GetButton().interactable = true;
                Re005_Vertex[level].coolTime.transform.position = Re005_Vertex[level].transform.position;
                break;
            case LabType.LabHp2:
                Re006_Vertex[level].GetButton().interactable = true;
                Re006_Vertex[level].coolTime.transform.position = Re006_Vertex[level].transform.position;
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
    public void IsClearRe002()
    {
        int Re002_Counting = 0;
        for (int i = 0; i < Re002_Vertex.Count; i++)
        {
            if (Re002_Vertex[i].isClear)
            {
                Re002_Counting++;
            }
        }

        if (Re002_Counting >= Re002_Vertex.Count)
            Re002_Clear.SetActive(true);
        else
            Re002_Clear.SetActive(false);
    }
    public void IsClearRe003()
    {
        int Re003_Counting = 0;
        for (int i = 0; i < Re003_Vertex.Count; i++)
        {
            if (Re003_Vertex[i].isClear)
            {
                Re003_Counting++;
            }
        }

        if (Re003_Counting >= Re003_Vertex.Count)
            Re003_Clear.SetActive(true);
        else
            Re003_Clear.SetActive(false);
    }
    public void IsClearRe004()
    {
        int Re004_Counting = 0;
        for (int i = 0; i < Re004_Vertex.Count; i++)
        {
            if (Re004_Vertex[i].isClear)
            {
                Re004_Counting++;
            }
        }

        if (Re004_Counting >= Re004_Vertex.Count)
            Re004_Clear.SetActive(true);
        else
            Re004_Clear.SetActive(false);
    }
    public void IsClearRe005()
    {
        int Re005_Counting = 0;
        for (int i = 0; i < Re005_Vertex.Count; i++)
        {
            if (Re005_Vertex[i].isClear)
            {
                Re005_Counting++;
            }
        }

        if (Re005_Counting >= Re005_Vertex.Count)
            Re005_Clear.SetActive(true);
        else
            Re005_Clear.SetActive(false);
    }
    public void IsClearRe006()
    {
        int Re006_Counting = 0;
        for (int i = 0; i < Re006_Vertex.Count; i++)
        {
            if (Re005_Vertex[i].isClear)
            {
                Re006_Counting++;
            }
        }

        if (Re006_Counting >= Re006_Vertex.Count)
            Re006_Clear.SetActive(true);
        else
            Re006_Clear.SetActive(false);
    }

    private void AllButtonNull()
    {
        foreach (var vertex in Re001_Vertex)
        {
            vertex.GetButton().interactable = false;
        }
        foreach (var vertex in Re002_Vertex)
        {
            vertex.GetButton().interactable = false;
        }
        foreach (var vertex in Re003_Vertex)
        {
            vertex.GetButton().interactable = false;
        }
        foreach (var vertex in Re004_Vertex)
        {
            vertex.GetButton().interactable = false;
        }
        foreach (var vertex in Re005_Vertex)
        {
            vertex.GetButton().interactable = false;
        }
        foreach (var vertex in Re006_Vertex)
        {
            vertex.GetButton().interactable = false;
        }
    }

    private void AllCheckEdges()
    {
        foreach (var edge in Re001_Edge)
        {
            edge.VertexClearCheck();
        }
        foreach (var edge in Re002_Edge)
        {
            edge.VertexClearCheck();
        }
        foreach (var edge in Re003_Edge)
        {
            edge.VertexClearCheck();
        }
        foreach (var edge in Re004_Edge)
        {
            edge.VertexClearCheck();
        }
        foreach (var edge in Re005_Edge)
        {
            edge.VertexClearCheck();
        }
        foreach (var edge in Re006_Edge)
        {
            edge.VertexClearCheck();
        }
    }

    public void LoadRe1(List<LabSaveData> savedatas)
    {
        foreach (var data in savedatas)
        {
            foreach (var re01 in Re001_Vertex)
            {
                if (re01.vertexID == data.Id)
                {
                    re01.isClear = data.isClear;
                    break;
                }
            }
        }
    }
    public void LoadRe2(List<LabSaveData> savedatas)
    {
        foreach (var data in savedatas)
        {
            foreach (var re in Re002_Vertex)
            {
                if (re.vertexID == data.Id)
                {
                    re.isClear = data.isClear;
                    break;
                }
            }
        }
    }
    public void LoadRe3(List<LabSaveData> savedatas)
    {
        foreach (var data in savedatas)
        {
            foreach (var re in Re003_Vertex)
            {
                if (re.vertexID == data.Id)
                {
                    re.isClear = data.isClear;
                    break;
                }
            }
        }
    }
    public void LoadRe4(List<LabSaveData> savedatas)
    {
        foreach (var data in savedatas)
        {
            foreach (var re in Re004_Vertex)
            {
                if (re.vertexID == data.Id)
                {
                    re.isClear = data.isClear;
                    break;
                }
            }
        }
    }
    public void LoadRe5(List<LabSaveData> savedatas)
    {
        foreach (var data in savedatas)
        {
            foreach (var re in Re005_Vertex)
            {
                if (re.vertexID == data.Id)
                {
                    re.isClear = data.isClear;
                    break;
                }
            }
        }
    }
    public void LoadRe6(List<LabSaveData> savedatas)
    {
        foreach (var data in savedatas)
        {
            foreach (var re in Re006_Vertex)
            {
                if (re.vertexID == data.Id)
                {
                    re.isClear = data.isClear;
                    break;
                }
            }
        }
    }

    public void SaveDataSet(StringTableData name, StringTableData buff, LabTableDatas table)
    {
        labStringTableName = name;
        labBuffStringTable = buff;
        labTalbeData = table;
    }
}