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
    [SerializeField]private float saveTimer;
    private float saveTimerDuration = 180f;
    private void Awake()
    {
        gameSaveLoad = gameObject.AddComponent<SaveLoad>();
        GameObject serverTime = new GameObject();
        serverTime.name = "ServerTime";
        serverTime.AddComponent<TestServerTime>();
        //StartCoroutine(TestServerTime.Instance.GetRealDateTimeFromAPI());
        //Debug.Log(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ff"));
        UIManager.Instance.panel.gameObject.SetActive(false);
        
        //Debug.Log(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ff"));
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
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == UIManager.Instance.SceneName)
        {
            saveTimer += Time.deltaTime;
        }
        if(saveTimer > saveTimerDuration )
        {
            saveTimer = 0f;
            gameSaveLoad.Save();
            Debug.Log("Save. Succese Timer");
        }
    }
    public void GameSave()
    {
        saveTimer = 0f;
        gameSaveLoad.Save();
        Debug.Log("Save. Succese Method");
    }
    private void OnApplicationQuit()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Build_01.03 Game")
            gameSaveLoad.Save();
    }
#if UNITY_ANDROID
    private void OnApplicationFocus(bool pauseStatus)
    {
        if (!pauseStatus && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Build_01.03 Game")
        {
            if (SavePower.onOff)
                SavePower.OnScreenBrightness();

            gameSaveLoad.Save();
        }
    }
#endif
}
