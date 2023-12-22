using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystemUi : MonoBehaviour
{
    private Button questButton;
    private List<QuestTableDatas> questTableDatas;


    public QuestType currentQuestType;

    private bool isAwkeSettingQuestStringTableData;
    public Dictionary<string, StringTableData> questNameDic = new Dictionary<string, StringTableData>();

    //private int waveClearId;


    //public DungeonType currentDungeonType;
    //public GatchaType currentGatchaType;
    //public UpgradeType currentUpgradeType;


    //public Dictionary<bool, Dictionary<QuestType, QuestTableDatas>> questDicitonary = new Dictionary<bool, Dictionary<QuestType, QuestTableDatas>>();

    private Language language;
    private bool isAwkeQuestDataSetting;
    public QuestSystemData questData;
    private StringTableData questNameStringTableData;

    public TextMeshProUGUI questNameTextMeshProUGUI;
    public TextMeshProUGUI questLevel;
    public TextMeshProUGUI questCountText;
    public TextMeshProUGUI questRewardText;

    public GameObject rewardImage;
    public Sprite[] rewardSprite = new Sprite[3];

    private void Awake()
    {
       
    }

    private void Start()
    {
        language = Global.language;
        if (questButton == null)
        {
            questButton = GetComponent<Button>();
            questButton.onClick.AddListener(GetReward);
        }
        if (MakeTableData.Instance.questTable != null)
        {
            questTableDatas = MakeTableData.Instance.questTable.questList;
        }
        else
        {
            MakeTableData.Instance.questTable = new QuestListTable();
            questTableDatas = MakeTableData.Instance.questTable.questList;
        }
        if (MakeTableData.Instance.stringTable != null)
        {
            if (!isAwkeSettingQuestStringTableData)
            {
                foreach (var data in questTableDatas)
                {
                    var strdata = MakeTableData.Instance.stringTable.GetStringTableData(data.quest_name);
                    questNameDic.Add(data.quest_name, strdata);
                }
               
            }
            isAwkeSettingQuestStringTableData = true;
        }
        else
        {
            MakeTableData.Instance.stringTable = new StringTable();
            foreach (var data in questTableDatas)
            {
                var strdata = MakeTableData.Instance.stringTable.GetStringTableData(data.quest_name);
                questNameDic.Add(strdata.ID, strdata);
            }
            if (!isAwkeSettingQuestStringTableData)
            {
                
            }
                isAwkeSettingQuestStringTableData = true;
        }
        if(!isAwkeQuestDataSetting)
        {
            ResetType();
            CheckQuesetType();
            isAwkeQuestDataSetting = true;
        }
    }

    private void Update()
    {
        if(language != Global.language)
        {
            language= Global.language;
            SetNameLanguage();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DeathEnemyCounting();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ClearWave();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayDungeon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayDungeon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayDungeon(3);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetGatchCount(1, 10);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetGatchCount(1, 35);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetGatchCount(1, 55);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GetGatchCount(2, 10);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetGatchCount(2, 35);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetGatchCount(2, 55);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetGatchCount(5, 10);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetGatchCount(5, 35);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetGatchCount(5, 55);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GetGatchCount(6, 10);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            GetGatchCount(6, 35);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetGatchCount(6, 55);
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            UpgradeQuestSet(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            UpgradeQuestSet(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            UpgradeQuestSet(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            UpgradeQuestSet(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            UpgradeQuestSet(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            UpgradeQuestSet(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            UpgradeQuestSet(6);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            UpgradeQuestSet(7);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            UpgradeQuestSet(8);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            UpgradeQuestSet(9);
        }
    }

    private void ResetType()
    {
        questData.dungeonType = DungeonType.None;
        questData.gatchaType = GatchaType.None;
        questData.upgradeType = UpgradeType.None;
    }

    private void GetReward()
    {
        CurrencyManager.GetSilver(questTableDatas[MakeTableData.Instance.currentIndex].currency_special01,questTableDatas[MakeTableData.Instance.currentIndex].reward_special01);
        var t = UIManager.Instance.GetComponent<MoneyTest>();
        t.PrintText();

        MakeTableData.Instance.currentIndex++;

        ResetType();
        CheckQuesetType();
    }
    private void CheckQuesetType()
    {
        var index = MakeTableData.Instance.currentIndex;
        currentQuestType = (QuestType)questTableDatas[index].quest_type;
        var data = questTableDatas[index];
        questButton.interactable = false;
        SetQuestTextMeshProUGUI(data);
        questLevel.text = $"Quest {index+1:D2}";
        switch (currentQuestType)
        {
            case QuestType.EneyDeathCount:
                SetDeathEnemyCount(data);
                break;
            case QuestType.WaveClear:
                SetWaveClearId(data);
                break;
            case QuestType.DungeonClear:
                SetDungeonClear(data);
                break;
            case QuestType.Gatcha:
                SetGatcha(data);
                break;
            case QuestType.PlayerStatsUpgrade:
                SetUpgradeQeustType(data);
                break;
        }
        if(data.reward_special01 == (int)RewardType.Sliver)
        {
            var sprite = rewardImage.GetComponent<Image>();
            sprite.sprite = rewardSprite[(int)RewardType.Sliver];
        }
        else if (data.reward_special01 == (int)RewardType.StyxPomegranate)
        {
            var sprite = rewardImage.GetComponent<Image>();
            sprite.sprite = rewardSprite[(int)RewardType.StyxPomegranate];
        }
        else if (data.reward_special01 == (int)RewardType.SoulStone)
        {
            var sprite = rewardImage.GetComponent<Image>();
            sprite.sprite = rewardSprite[(int)RewardType.SoulStone];
        }
        questRewardText.text = $"{data.currency_special01}";
    }

    private void SetQuestTextMeshProUGUI(QuestTableDatas data)
    {
        questNameStringTableData = questNameDic[data.quest_name];
        SetNameLanguage();
    }
    private void SetNameLanguage()
    {
        switch (language)
        {
            case Language.KOR:
                questNameTextMeshProUGUI.text = questNameStringTableData.KOR;
                break;
            case Language.ENG:
                questNameTextMeshProUGUI.text = questNameStringTableData.ENG;
                break;
        }
    }

    public void DeathEnemyCounting()
    {
        if (questData.isMaxEneyDeathCount || currentQuestType != QuestType.EneyDeathCount)
            return;

        questData.currentMonsterDeathCount++;
      
        if (questData.currentMonsterDeathCount >= questData.enemyMaxDeathCount)
        {
            questData.isMaxEneyDeathCount = true;
            questButton.interactable = true;
        }
          questCountText.text = $"{questData.currentMonsterDeathCount} / {questData.enemyMaxDeathCount}";
    }

    private void SetDeathEnemyCount(QuestTableDatas data)
    {
        questData.enemyMaxDeathCount = data.clear_enemy;
        questData.currentMonsterDeathCount = 0;
        questData.isMaxEneyDeathCount = false;

        questCountText.text = $"{questData.currentMonsterDeathCount} / {questData.enemyMaxDeathCount}";
    }


    public void ClearWave()
    {
        if (!questData.isClearWave&& questData.waveClearId <=WaveManager.Instance.GetCurrentIndex()) //조건검사도 같이 넣을 예정
        {
            questData.isClearWave = true;
            questButton.interactable = true;
        }

    }

    private void SetWaveClearId(QuestTableDatas data)
    {
        questData.waveClearId = data.clear_wave;
        questData.isClearWave= false;
        questCountText.text ="";
        if (questData.waveClearId < WaveManager.Instance.GetCurrentIndex())
        {
            ClearWave();
        }
    }

    public void PlayDungeon(int index)
    {
        if (questData.dungeonType != (DungeonType)index || questData.isDungeonClear)
            return;

        questData.currentPlayDungeonType++;

        if (questData.currentPlayDungeonType >= questData.maxPlaydungeonType)
        {
            questData.isDungeonClear = true;
            questButton.interactable = true;
            questCountText.text = $"{questData.currentPlayDungeonType} / {questData.maxPlaydungeonType}";
        }
    }
    private void SetDungeonClear(QuestTableDatas data)
    {
        questData.dungeonType = (DungeonType)data.type_dungeon;
        questData.maxPlaydungeonType = 1;
        questData.currentPlayDungeonType = 0;
        questData.isDungeonClear = false;

        questCountText.text = $"{questData.currentPlayDungeonType} / {questData.maxPlaydungeonType}";
    }
    
    public void GetGatchCount(int index,int count)
    {
        if (questData.gatchaType != (GatchaType)index || questData.isMaxGathcacount)
            return;

        questData.currentGatchaCount += count;

        if(questData.currentGatchaCount >= questData.maxGatchaCount)
        {
            questData.currentGatchaCount = questData.maxGatchaCount;
            questData.isMaxGathcacount = true;
            questButton.interactable = true;
        }
        questCountText.text = $"{questData.currentGatchaCount} / {questData.maxGatchaCount}";
    }


    private void SetGatcha(QuestTableDatas data)
    {
        questData.gatchaType = (GatchaType)data.type_gatcha;
        questData.currentGatchaCount =0;
        questData.maxGatchaCount = data.clear_gatcha;
        questData.isMaxGathcacount = false;

        questCountText.text = $"{questData.currentGatchaCount} / {questData.maxGatchaCount}";
    }

    public void UpgradeQuestSet(int index)
    {
        if (questData.upgradeType != (UpgradeType)index || questData.isMaxUpgrade)
        {
            return;
        }

        questData.currentUpgradeCount++;
        questCountText.text = $"{questData.currentUpgradeCount} / {questData.upgradeMaxCount}";
        if (questData.currentUpgradeCount >= questData.upgradeMaxCount)
        {
            questData.isMaxUpgrade = true;
            questButton.interactable = true;
        }
    }

    private void SetUpgradeQeustType(QuestTableDatas data)
    {
        var type = (UpgradeType)data.type_upgrade;
        questData.upgradeType= type;
        questData.upgradeMaxCount = data.clear_upgrade;
        questData.currentUpgradeCount = 0;
        questData.isMaxUpgrade = false;

        questCountText.text = $"{questData.currentUpgradeCount} / {questData.upgradeMaxCount}";
    }
}

[Serializable]
public struct QuestSystemData
{
    public int currentMonsterDeathCount;
    public int enemyMaxDeathCount;
    public bool isMaxEneyDeathCount;

    public int waveClearId;
    public bool isClearWave;

    public DungeonType dungeonType;
    public int currentPlayDungeonType;
    public int maxPlaydungeonType;
    public bool isDungeonClear;

    public GatchaType gatchaType;
    public int currentGatchaCount;
    public int maxGatchaCount;
    public bool isMaxGathcacount;

    public UpgradeType upgradeType;
    public int currentUpgradeCount;
    public int upgradeMaxCount;
    public bool isMaxUpgrade;
}