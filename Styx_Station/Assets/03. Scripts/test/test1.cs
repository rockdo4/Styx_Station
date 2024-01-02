using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test1 : MonoBehaviour
{
    public PlayerController playerController;
    private void Awake()
    {
        Debug.Log(DateTime.Now.ToString("hh:mm::ss:ff"));
        UIManager.Instance.gameObject.SetActive(true);
        UIManager.Instance.HpGauge = playerController.hpBar;

        //WaveManager.Instance.SetStageByIndexStage(GameData.stageData_WaveManager);
        //WaveManager.Instance.SetTileMap();
        //WaveManager.Instance.SetRepeat(GameData.isRepeatData_WaveManager);

        var state = StateSystem.Instance;

        state.TotalUpdate();
        //WaveManager.Instance.SetWavePanel(); 
        UIManager.Instance.BangchiOpen();
        UIManager.Instance.OpenPlayerBuffInfo();

        MakeTableData.Instance.gameSaveLoad.waveManager = WaveManager.Instance;
        MakeTableData.Instance.gameSaveLoad.skillManager = SkillManager.Instance;

        GameData.EquipItemDataSetting();
        GameData.SkillDataSetting();
        GameData.EquipSkillDataSetting();
        GameData.PetDataSetting();
        GameData.EquipPetDataSetting();
        UIManager.Instance.SetAutoSkillButton(GameData.isAutoData);
    }
}
