using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabMainVertex : MonoBehaviour
{
    public string vertexID;
    private bool isAwakeTime;
    public Button vertexButton { get; private set; }
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

    private void Awake()
    {       
        AwakeSetting();
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
            if(copLabManager.maxTimerTic >0f)
            {
                var maxTimerTic = (float)(copLabManager.maxTimerTic / copLabManager.milSeconds);
                coolTime.fillAmount = (timerTic / maxTimerTic);
            }
            else
            {
                coolTime.fillAmount = 0f;
            }
           
        }
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    GetClear(true);
        //}
    }
    private void AwakeSetting()
    {
        if (!isAwakeTime)
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
                labTypeBuffStringDatas= MakeTableData.Instance.stringTable.GetStringTableData(labTypeBuffStringTableKey);
            }
            else if (MakeTableData.Instance.stringTable == null)
            {
                MakeTableData.Instance.stringTable = new StringTable();
                labTypeNameStringDatas = MakeTableData.Instance.stringTable.GetStringTableData(labTableDatas.Re_Name_ID);
                labTypeBuffStringDatas = MakeTableData.Instance.stringTable.GetStringTableData(labTypeBuffStringTableKey);
            }
            if (LabSystem.Instance != null)
            {
                copLabManager = LabSystem.Instance;

            }
            coolTime.gameObject.SetActive(false);
            isAwakeTime = true;

            if (isClear)
                SetAssignedAcitve();
            else
                SetNoneActive();
        }

        if (vertexButton == null)
        {
            vertexButton = GetComponent<Button>();
            var labInfowindow = popUpLabInfoObject.gameObject.GetComponent<LabInfoWindow>();

            vertexButton.onClick.AddListener(() => labInfowindow.SetVertex(labType, labTypeNameStringDatas, labTypeBuffStringDatas, labTableDatas, LabTypeLevel));
        }
    }
    public void GetClear(bool clear)
    {
        isClear = clear;
        AwakeSetting();
        foreach (var edge in edges) 
        {
            edge.VertexClearCheck();
        }
        SetAssignedAcitve();
        vertexButton.interactable = false;
        vertexButton.onClick.RemoveAllListeners();
        //
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
        if (vertexButton == null)
        {
            AwakeSetting();
        }

        return vertexButton;
    }
}
