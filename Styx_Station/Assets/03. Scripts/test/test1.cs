using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test1 : MonoBehaviour
{
    public PlayerController playerController;
    public Camera uiCamera;
    public GameObject castZone;
    private void Awake()
    {
        Debug.Log(DateTime.Now.ToString("hh:mm::ss:ff"));
        UIManager.Instance.gameObject.SetActive(true);
        // Áö¿ï°Í 
        UIManager.Instance.gameObject.GetComponent<Canvas>().worldCamera = uiCamera;    
        UIManager.Instance.HpGauge = playerController.hpBar;
        //

        //WaveManager.Instance.SetStageByIndexStage(GameData.stageData_WaveManager);
        //WaveManager.Instance.SetTileMap();
        //WaveManager.Instance.SetRepeat(GameData.isRepeatData_WaveManager); 

        //YL 0102
        SkillManager.Instance.player = playerController.gameObject;
        
        
    }
    private void OnEnable()
    {
        var state = StateSystem.Instance;

        state.TotalUpdate();
        //WaveManager.Instance.SetWavePanel(); 
        UIManager.Instance.BangchiOpen();
        UIManager.Instance.OpenPlayerBuffInfo();

        MakeTableData.Instance.gameSaveLoad.waveManager = WaveManager.Instance;
        MakeTableData.Instance.gameSaveLoad.skillManager = SkillManager.Instance;
        if(GameData.isLoad)
        {
            GameData.EquipItemDataSetting();
            GameData.SkillDataSetting();
            GameData.EquipSkillDataSetting();
            GameData.PetDataSetting();
            GameData.EquipPetDataSetting();
            UIManager.Instance.SetAutoSkillButton(GameData.isAutoData);
        }

        //yyl 0102
        SkillManager.Instance.ResetAllSkillCool();


        var button = UIManager.Instance.RepeatButton.transform.GetChild(3).GetComponent<Button>();
        button.onClick.AddListener(() => WaveManager.Instance.SetRepeat(false));

        SkillManager.Instance.SetCaztZone(castZone);
    }
}
