using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabMainVertex : MonoBehaviour
{
    private bool isAwakeTime;
    private Button vertexButton;
    public List<LabMainEdge> edges = new List<LabMainEdge>();
    public int LabTypeLevel;
    public bool isClear;
    [SerializeField] private bool isResearching;


    public LabType labType;

    public string labTableName;
    private LabTableDatas labTableDatas;

    private StringTableData labTypeNameStringDatas;

    public string labTypeBuffStringTableKey;
    private StringTableData labTypeBuffStringDatas;


    public LabInfoWindow popUpLabInfoObject;
    //public GameObject popUpLabSucceedObject;

    protected Color noneActive = new Color(1, 1, 1, 0.3f);
    protected Color assignedActive = Color.white;

    public Image coolTime;
    private LabSystem copLabManager;

    private void Start()
    {
        if(!isAwakeTime)
        {
            if (MakeTableData.Instance != null)
            {
                labTableDatas = MakeTableData.Instance.labTable.GetLabTableData(labTableName);
            }
            else if (MakeTableData.Instance == null)
            {
                MakeTableData.Instance.labTable = new LabTable();
                labTableDatas = MakeTableData.Instance.labTable.GetLabTableData(labTableName);
            }
            if (MakeTableData.Instance.stringTable != null)
            {
                labTypeNameStringDatas = MakeTableData.Instance.stringTable.GetStringTableData(labTableDatas.Re_Name_ID);
            }
            else if (MakeTableData.Instance.stringTable == null)
            {
                MakeTableData.Instance.stringTable = new StringTable();
                labTypeNameStringDatas = MakeTableData.Instance.stringTable.GetStringTableData(labTableDatas.Re_Name_ID);
            }
            if (LabSystem.Instance != null)
            {
                copLabManager = LabSystem.Instance;

            }
            coolTime.gameObject.SetActive(false);
            isAwakeTime = true; 

        }
       


        if (vertexButton == null)
        {
            vertexButton = GetComponent<Button>();
            var labInfowindow = popUpLabInfoObject.gameObject.GetComponent<LabInfoWindow>();
            
            vertexButton.onClick.AddListener(() => labInfowindow.SetVertex(labType,labTypeNameStringDatas, labTypeBuffStringDatas, labTableDatas, LabTypeLevel));
        }
       

        if (isClear)
            SetAssignedAcitve();
        else
            SetNoneActive();
    }
    private void Update()
    {
        if(copLabManager != null && copLabManager.isResearching && copLabManager.level == LabTypeLevel && labType == copLabManager.labType)
        {
            if (!coolTime.gameObject.activeSelf)
            {
                coolTime.gameObject.SetActive(true);
            }
            var timerTic = (float)(copLabManager.timerTic / copLabManager.milSeconds);
            var maxTimerTic = (float)(copLabManager.maxTimerTic / copLabManager.milSeconds);
            coolTime.fillAmount = (timerTic / maxTimerTic);

        }
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    GetClear(true);
        //}
    }

    public void GetClear(bool clear)
    {
        isClear = clear;
        
        foreach(var edge in edges) 
        {
            edge.VertexClearCheck();
        }
        SetAssignedAcitve();
        vertexButton.interactable = false;
        vertexButton.onClick.RemoveAllListeners();
    }



    public void SetNoneActive()
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

    public void SetAssignedAcitve()
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

    public Button GetButton()
    {
        return vertexButton;
    }
}
