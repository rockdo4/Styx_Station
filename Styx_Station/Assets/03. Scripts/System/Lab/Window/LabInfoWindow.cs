using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LabInfoWindow : Window
{
    private LabType labType;
    public string levelStringKey;
    public string increaseStringKey;
    public string needPomegranteStringKey;
    public string timerStringKey;

    private bool isOneLevelStringKeySetting;
    private LabTableDatas labTableData;
    private StringTableData labTypeNameStringDatas;
    private StringTableData labTypeBuffStringDatas;

    private StringTableData levelStringTableData;
    private StringTableData increaseStringTableData;
    private StringTableData needPomegranteStringTableData;
    private StringTableData timerStringDatas;

    private int level;
    private int timer;
    public float seconds = 60f;

    public TextMeshProUGUI researchTypeText;
    public TextMeshProUGUI researchTypeBuffText;
    public TextMeshProUGUI researchTaxText;
    public TextMeshProUGUI researchTimer;

    private int buffPercent;
    private int price;

    public Button reasearchButton;
    private TimeSpan timerString;
    public void SetVertex(LabType labType,StringTableData labTypeNameStringDatas,StringTableData labTypeBuffStringDatas, LabTableDatas labTableData ,int level)
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
            }
            else
            {
                MakeTableData.Instance.stringTable = new StringTable();
                levelStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(levelStringKey);
                increaseStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(increaseStringKey);
                needPomegranteStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(needPomegranteStringKey);
                timerStringDatas = MakeTableData.Instance.stringTable.GetStringTableData(timerStringKey);
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
                break;
            case Language.ENG:
                researchTypeText.text = $"{labTypeNameStringDatas.ENG} {level+1} {levelStringTableData.ENG}";
                researchTypeBuffText.text = $"{labTypeBuffStringDatas.ENG} {buffPercent}% {increaseStringTableData.ENG}";
                strPrice = string.Format(needPomegranteStringTableData.ENG, price);
                researchTaxText.text = $"{strPrice}";
                researchTimer.text = $"{timerStringDatas.ENG}{strTimer}";
                break;
        }
        base.Open();
    }

    public override void Close()
    {

        base.Close();
    }

}
