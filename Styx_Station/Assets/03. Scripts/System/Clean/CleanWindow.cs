using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CleanWindow : Window
{
    private UIManager uiManager;
    private WaveManager waveManager;
    private SkillManager skillManager;
    private StringTable stringTable;

    public TextMeshProUGUI contentName;
    public TextMeshProUGUI contentCount;

    public int bossRushIndex { get; private set; } = 0;

    private bool firstSetting = false;
    //yyl 0102
    public string bossRushSceneName;

    public int pageIndex = 0;
    
    public int openStage = 0;

    public int totalCount = 2;

    public int currentCount = 2;

    public List<GameObject> stageSlot = new List<GameObject>();

    public GameObject ClearInfo;

    public TextMeshProUGUI clear;
    public TextMeshProUGUI reward;

    public Button clearExit;
    public Button nextStage;

    public Image rewardItemIcon;

    public TextMeshProUGUI silver;
    public TextMeshProUGUI soul;

    public void TextUpdate()
    {
        if (Global.language == Language.KOR)
            contentName.text = $"{stringTable.GetStringTableData("Cleaning001").KOR}";
        else if (Global.language == Language.ENG)
            contentName.text = $"{stringTable.GetStringTableData("Cleaning001").ENG}";

        CountUpdate();
        SlotUpdate();
    }

    public void CountUpdate()
    {
        if (Global.language == Language.KOR)
        {
            string text = string.Format(stringTable.GetStringTableData("Cleaning003").KOR, currentCount, totalCount);
            contentCount.text = $"{text}";
        }
        else if (Global.language == Language.ENG)
        {
            string text = string.Format(stringTable.GetStringTableData("Cleaning003").ENG, currentCount, totalCount);
            contentCount.text = $"{text}";
        }
    }

    public void SlotUpdate()
    {
        for (int i = 0; i < stageSlot.Count; ++i)
        {
            int index = 6 * pageIndex + i + 1;
            Color color = new Color();
            if (Global.language == Language.KOR)
            {
                string text = string.Format(stringTable.GetStringTableData("Cleaning004").KOR, index);
                stageSlot[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{text}";

                stageSlot[i].transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Cleaning005").KOR}";
            }
            else if (Global.language == Language.ENG)
            {
                string text = string.Format(stringTable.GetStringTableData("Cleaning004").ENG, index);
                stageSlot[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{text}";

                stageSlot[i].transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Cleaning005").ENG}";
            }

            if(openStage >= index-1)
            {
                color = new Color(1f, 1f, 1f, 1f);
                stageSlot[i].GetComponent<Image>().color = color;
                stageSlot[i].transform.GetChild(0).GetComponent<Image>().color = color;
                stageSlot[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                stageSlot[i].transform.GetChild(2).GetComponent<Image>().color = color;
            }
            else if(openStage<index-1)
            {
                color = new Color(128f / 255f, 128f / 255f, 128f / 255f, 1f);
                stageSlot[i].GetComponent <Image>().color = color;
                stageSlot[i].transform.GetChild(0).GetComponent<Image>().color = color;
                stageSlot[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
                stageSlot[i].transform.GetChild(2).GetComponent<Image>().color = color;
            }
        }
    }

    public override void Open()
    {
        Setting();

        pageIndex = 0;

        TextUpdate();

        base.Open();
    }

    public override void Close()
    {
        pageIndex = 0;

        base.Close();
    }

    public void Setting()
    {
        if(!firstSetting)
        {
            uiManager = UIManager.Instance;
            waveManager = WaveManager.Instance;
            skillManager = SkillManager.Instance;

            if(MakeTableData.Instance.stringTable == null)
                MakeTableData.Instance.stringTable = new StringTable();

            stringTable = MakeTableData.Instance.stringTable;
        }
    }

    //yyl. 0102
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetBossRushIndex(int index)
    {
        bossRushIndex = index;
    }

    public void LoadBossRussh(int index)
    {
        GameData.stageData_WaveManager = waveManager.currStage.index;
        uiManager.OnClickClose();
        SetBossRushIndex(index);
        waveManager.isWaveInProgress = false;
        skillManager.ResetAllSkillCool();
        LoadScene(bossRushSceneName);
    }
    
    public void OnClickPrePage()
    {
        if(pageIndex > 0)
        {
            pageIndex--;

            SlotUpdate();
        }
    }

    public void OnClickNextPage()
    {
        if(pageIndex <9)
        {
            pageIndex++;

            SlotUpdate();
        }
    }

    public void OnClickSlot_1()
    {
        if (currentCount <= 0)
            return;

        LoadBossRussh(6 * pageIndex);
    }
    public void OnClickSlot_2()
    {
        if (currentCount <= 0)
            return;

        LoadBossRussh(6 * pageIndex + 1);
    }
    public void OnClickSlot_3()
    {
        if (currentCount <= 0)
            return;

        LoadBossRussh(6 * pageIndex + 2);
    }
    public void OnClickSlot_4()
    {
        if (currentCount <= 0)
            return;

        LoadBossRussh(6 * pageIndex + 3);
    }
    public void OnClickSlot_5()
    {
        if (currentCount <= 0)
            return;

        LoadBossRussh(6 * pageIndex + 4);
    }
    public void OnClickSlot_6()
    {
        if (currentCount <= 0)
            return;

        LoadBossRussh(6 * pageIndex + 5);
    }

    public void GetReward()
    {
        if(Global.language == Language.KOR)
        {
            clear.text = $"{stringTable.GetStringTableData("Cleaning006").KOR}";
            reward.text = $"{stringTable.GetStringTableData("Cleaning007").KOR}";
            clearExit.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Cleaning008").KOR}";
            nextStage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Cleaning009").KOR}";
        }
        else if(Global.language == Language.ENG)
        {
            clear.text = $"{stringTable.GetStringTableData("Cleaning006").ENG}";
            reward.text = $"{stringTable.GetStringTableData("Cleaning007").ENG}";
            clearExit.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Cleaning008").ENG}";
            nextStage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Cleaning009").ENG}";
        }

        var item = InventorySystem.Instance.customMaker.CreateCustomItem(bossRushIndex / 2);

        if (item == null)
            return;

        rewardItemIcon.sprite = item.itemIcon;

        if (currentCount > 0)
            nextStage.GetComponent<Button>().interactable = true;
        else if(currentCount<=0)
            nextStage.GetComponent<Button>().interactable = false;


        // 돈 추가 및 돈 텍스트 추가해야함

        ClearInfo.SetActive(true);
    }

    public void OnClickReward()
    {
        bossRushIndex = 0;
        ClearInfo.SetActive(false);
    }

    public void OnClickNextStage()
    {
        ClearInfo.SetActive(false);

        if(bossRushIndex<59)
            LoadBossRussh(bossRushIndex + 1);

        else if(bossRushIndex>=59)
            LoadBossRussh(59);
    }
}
