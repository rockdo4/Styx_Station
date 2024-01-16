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
        UIManager.Instance.panel.gameObject.SetActive(true);
        UIManager.Instance.gameObject.GetComponent<Canvas>().worldCamera = uiCamera;
        UIManager.Instance.HpGauge = playerController.hpBar;

        //WaveManager.Instance.SetStageByIndexStage(GameData.stageData_WaveManager);
        //WaveManager.Instance.SetTileMap();
        //WaveManager.Instance.SetRepeat(GameData.isRepeatData_WaveManager); 

        //YL 0102
        SkillManager.Instance.player = playerController.gameObject;


        var button = UIManager.Instance.RepeatButton.transform.GetChild(3).GetComponent<Button>();
        button.onClick.AddListener(() => WaveManager.Instance.SetRepeat(false));

        UIManager.Instance.BangchiOpen();
    }
    private void OnEnable()
    {
        var state = StateSystem.Instance;

        state.TotalUpdate();
        //WaveManager.Instance.SetWavePanel(); 
        //if(!UIManager.Instance.tutorial.GetComponent<TutorialSystem>().loadTutorial)
        //    UIManager.Instance.BangchiOpen();
        UIManager.Instance.OpenPlayerBuffInfo();

        MakeTableData.Instance.gameSaveLoad.waveManager = WaveManager.Instance;
        MakeTableData.Instance.gameSaveLoad.skillManager = SkillManager.Instance;

        //yyl 0102

        

        SkillManager.Instance.SetCaztZone(castZone);
    }
}
