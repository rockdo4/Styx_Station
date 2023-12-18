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
    private LabManager copLabManager;

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
            if (UIManager.Instance.labManager != null)
            {
                copLabManager = UIManager.Instance.labManager;

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
        if(copLabManager != null && copLabManager.isResearching)
        {
            if(!coolTime.gameObject.activeSelf)
                coolTime.gameObject.SetActive(true);

            coolTime.fillAmount = (copLabManager.timer / copLabManager.maxTimer);

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
