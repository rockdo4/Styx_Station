using System;
using UnityEngine;

public class MakeTableData : Singleton<MakeTableData>
{
    [HideInInspector] public  SaveLoad gameSaveLoad;
    [HideInInspector] public StringTable stringTable;
    [HideInInspector] public DiningTable diningRoomTable;
    [HideInInspector] public StageTable stageTable;
    [HideInInspector] public LabTable labTable;
    [HideInInspector] public QuestListTable questTable;
    public int currentQuestIndex = 0;
    public int loppCurrentQuestIndex = 0;

    private void Awake()
    {
        gameSaveLoad = gameObject.AddComponent<SaveLoad>();
        
        Debug.Log(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ff"));
        UIManager.Instance.gameObject.SetActive(false);
        gameSaveLoad.Load();
        Debug.Log(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ff"));
    }
    private void Start()
    {
        if(stringTable ==null)
            stringTable = new StringTable();
        if(diningRoomTable == null)
            diningRoomTable = new DiningTable();
        if(stageTable == null)
            stageTable = new StageTable();
        if(labTable == null)
            labTable = new LabTable();
        if(questTable == null)
            questTable = new QuestListTable();  

        UnitConverter.InitUnitConverter();
    }
    private void Update()
    {
      
    }
    private void OnApplicationQuit() => gameSaveLoad.Save();


    private void OnApplicationPause(bool pause)
    {
        
    }

#if UNITY_ANDROID
    private void OnApplicationFocus(bool pauseStatus)
    {
        if (!pauseStatus && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "LswGameScene")
        {
            gameSaveLoad.Save();
        }
    }
#endif
}
