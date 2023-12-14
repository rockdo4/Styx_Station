using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystemUi : MonoBehaviour
{
    private Button questButton;
    private List<QuestTableDatas> questTableDatas;
    public int currentIndex;
    public QuestType currentQuestType;
    public StringTableData quesetNameStringTable;

    private int currentMonsterDeathCount;
    private int enemyMaxDeathCount;
    private bool isMaxEneyDeathCount;

    private int waveClearId;


    public DungeonType currentDungeonType;
    public GatchaType currentGatchaType;
    public UpgradeType currentUpgradeType;

    private void Start()
    {
        if(questButton == null)
            questButton = GetComponent<Button>();
        if (MakeTableData.Instance.questTable != null)
        {
            questTableDatas = MakeTableData.Instance.questTable.questList;
            currentIndex = MakeTableData.Instance.questTable.currentIndex;
        }
        else
        {
            MakeTableData.Instance.questTable = new QuestListTable();
            questTableDatas = MakeTableData.Instance.questTable.questList;
            currentIndex = MakeTableData.Instance.questTable.currentIndex;
        }
        CheckQuesetType();
    }


    private void CheckQuesetType()
    {
        currentQuestType = (QuestType)questTableDatas[currentIndex].quest_type;
        var data = questTableDatas[currentIndex];
        switch ((QuestType)questTableDatas[currentIndex].quest_type)
        {
            case QuestType.EneyDeathCount:
                enemyMaxDeathCount = data.clear_enemy;
                break;
            case QuestType.WaveClear:
                waveClearId = data.clear_wave;
                //스테이지 관련한거 알아야할듯...
                break;
            case QuestType.DungeonClear:
                currentDungeonType = (DungeonType)data.type_dungeon;
                switch(currentDungeonType)
                {
                    case DungeonType.SweepHomeBase:
                        break;
                    case DungeonType.CleaingUpTrainTracks:
                        break;
                    case DungeonType.BreakDownTrain:
                        break;
                }
                // 없어서 모르겠음...
                break;
            case QuestType.Gatcha:
                currentGatchaType = (GatchaType)data.type_gatcha;
                switch(currentGatchaType)
                {
                    case GatchaType.Weapon:
                        break;
                    case GatchaType.Armor:
                        break;
                    case GatchaType.Skill:
                        break;
                    case GatchaType.Pet:
                        break;
                }
                break;
            case QuestType.PlayerStatsUpgrade:
                currentUpgradeType = (UpgradeType)data.type_upgrade;
                switch(currentUpgradeType)
                {
                    case UpgradeType.Power:
                        break;
                    case UpgradeType.PowerBoost:
                        break;
                    case UpgradeType.AttackSpeed:
                        break;
                    case UpgradeType.Critical:
                        break;
                    case UpgradeType.CriticalPower:
                        break;
                    case UpgradeType.MonsterDamage:
                        break;
                    case UpgradeType.MaxHp:
                        break;
                    case UpgradeType.Healing:
                        break;
                    case UpgradeType.Weapon:
                        break;
                    case UpgradeType.Skill:
                        break;
                }
                break;
        }
    }
    
    public void OnclickQuest()
    {
        if(isMaxEneyDeathCount)
        {
            isMaxEneyDeathCount = false;
            currentMonsterDeathCount = 0;
            enemyMaxDeathCount = 0;
            currentIndex++;
        }

        CheckQuesetType();
    }

    public void DeathEnemyCounting()
    {
        if (isMaxEneyDeathCount)
            return;

        if(currentMonsterDeathCount >= enemyMaxDeathCount)
        {
            isMaxEneyDeathCount=true;
        }
        currentMonsterDeathCount++;
    }
}
