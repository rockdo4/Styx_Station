using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LabInfoWindow : Window
{
    public LabCompleteWindwo labCompleteWindwo;
    private LabType labType;
    public string levelStringKey;
    public string increaseStringKey;
    public string needPomegranteStringKey;
    public string timerStringKey;
    public string finishResearchStringKey;

    private bool isOneLevelStringKeySetting;
    public LabTableDatas labTableData;
    public StringTableData labTypeNameStringDatas;
    public StringTableData labTypeBuffStringDatas;

    public StringTableData levelStringTableData;
    public StringTableData increaseStringTableData;
    public StringTableData needPomegranteStringTableData;
    public StringTableData timerStringDatas;
    public StringTableData buttonText;

    public int level;
    private int timer;
    public float seconds = 60f;

    public TextMeshProUGUI researchTypeText;
    public TextMeshProUGUI researchTypeBuffText;
    public TextMeshProUGUI researchTaxText;
    public TextMeshProUGUI researchTimer;
    public TextMeshProUGUI researchButtonText;


    public int buffPercent;
    private int price;

    public Button reasearchButton;
    private TimeSpan timerString;
    public void SetVertex(LabType labType,StringTableData labTypeNameStringDatas,StringTableData labTypeBuffStringDatas, LabTableDatas labTableData ,int level,bool open=true)
    {
       
        if (!isOneLevelStringKeySetting)
        {
            isOneLevelStringKeySetting = true;
            if (MakeTableData.Instance.stringTable != null)
            {
                levelStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(levelStringKey);
                increaseStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(increaseStringKey);
                needPomegranteStringTableData= MakeTableData.Instance.stringTable.GetStringTableData(needPomegranteStringKey);
                timerStringDatas = MakeTableData.Instance.stringTable.GetStringTableData(timerStringKey);
                buttonText = MakeTableData.Instance.stringTable.GetStringTableData(finishResearchStringKey);
            }
            else
            {
                MakeTableData.Instance.stringTable = new StringTable();
                levelStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(levelStringKey);
                increaseStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(increaseStringKey);
                needPomegranteStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(needPomegranteStringKey);
                timerStringDatas = MakeTableData.Instance.stringTable.GetStringTableData(timerStringKey);
                buttonText = MakeTableData.Instance.stringTable.GetStringTableData(finishResearchStringKey);
            }
        }
        this.labType = labType;
        this.labTypeNameStringDatas = labTypeNameStringDatas;
        this.labTypeBuffStringDatas = labTypeBuffStringDatas;
        this.labTableData = labTableData;
        this.level = level;
        

        switch(this.labType)
        {
            case LabType.LabPower1:
                buffPercent = labTableData.Re_ATK * (int)Math.Pow(labTableData.Re_ATKUP, this.level);
                break;

            case LabType.LabHp1:
                buffPercent = labTableData.Re_HP * (int)Math.Pow(labTableData.Re_HPUP, this.level);
                break;

            case LabType.LabCriticalPower:
                buffPercent = labTableData.Re_Cri * (int)Math.Pow(labTableData.Re_CriUP, this.level);
                break;

            case LabType.LabSliverUp:
                buffPercent = labTableData.Re_Sil * (int)Math.Pow(labTableData.Re_SilUP, this.level);
                break;

            case LabType.LabPower2:
                buffPercent = labTableData.Re_ATK * (int)Math.Pow(labTableData.Re_ATKUP, this.level);
                break;
            case LabType.LabHp2:
                buffPercent = labTableData.Re_HP * (int)Math.Pow(labTableData.Re_HPUP, this.level);
                break;
        }
        price = labTableData.Re_Pom * (int)Math.Pow(labTableData.Re_PomUp, this.level);
        timer = (int)(labTableData.Re_Time * Math.Pow(labTableData.Re_TimeUP, this.level) * seconds);
        reasearchButton.onClick.AddListener(() => LabSystem.Instance.StartResearching(timer, this.labType,this.level));
        if (LabSystem.Instance.isResearching ||CurrencyManager.money2 < price)
        {
            reasearchButton.interactable = false;
        }
        else
        {
            reasearchButton.interactable = true;
        }
        LabSystem.Instance.SaveDataSet(this.labTypeNameStringDatas, this.labTypeBuffStringDatas, this.labTableData);

        if(open)
            Open();
    }

    private void FixedUpdate()
    {
        if(LabSystem.Instance.isResearching)
        {
            var strTimer = string.Empty;
            timerString = TimeSpan.FromSeconds(LabSystem.Instance.timerTic / LabSystem.Instance.milSeconds);
            strTimer = timerString.ToString(@"dd\:hh\:mm\:ss");
            switch (Global.language)
            {
                case Language.KOR:

                    researchTimer.text = $"{timerStringDatas.KOR}{strTimer}";
                    break;
                case Language.ENG:
                    researchTimer.text = $"{timerStringDatas.ENG}{strTimer}";
                    break;
            }
        }
    }

    public override void Open()
    {
        labCompleteWindwo.Close();
        var strPrice =string.Empty;
        var strTimer =string.Empty;
  

        timerString = TimeSpan.FromSeconds(timer);
        strTimer = timerString.ToString(@"dd\:hh\:mm\:ss");
        switch (Global.language)
        {
            case Language.KOR:
                researchTypeText.text = $"{labTypeNameStringDatas.KOR} {level+1} {levelStringTableData.KOR}";
                researchTypeBuffText.text = $"{labTypeBuffStringDatas.KOR} {buffPercent}% {increaseStringTableData.KOR}";
                strPrice = string.Format(needPomegranteStringTableData.KOR, price);
                researchTaxText.text = $"{strPrice}";
                researchTimer.text = $"{timerStringDatas.KOR}{strTimer}";
                researchButtonText.text = $"{buttonText.KOR}";
                break;
            case Language.ENG:
                researchTypeText.text = $"{labTypeNameStringDatas.ENG} {level+1} {levelStringTableData.ENG}";
                researchTypeBuffText.text = $"{labTypeBuffStringDatas.ENG} {buffPercent}% {increaseStringTableData.ENG}";
                strPrice = string.Format(needPomegranteStringTableData.ENG, price);
                researchTaxText.text = $"{strPrice}";
                researchTimer.text = $"{timerStringDatas.ENG}{strTimer}";
                researchButtonText.text = $"{buttonText.ENG}";
                break;
        }
        base.Open();
    }

    public override void Close()
    {
       
        base.Close();
    }

    public void StartResarching()
    {
        CurrencyManager.money2 -= price;
        UIManager.Instance.PrintPommeMoney();
    }
}
