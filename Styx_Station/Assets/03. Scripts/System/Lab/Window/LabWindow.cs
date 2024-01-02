using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LabWindow : Window
{
    public LabInfoWindow labInfoWindow;
    public LabCompleteWindwo labCompleteWindwo;
    private bool isLoad;
    private bool isFristSet;
    public string labNameStringKey;
    public TextMeshProUGUI labNameTextMeshProUGUI;
    private StringTableData labNameStringTableData;
    public override void Open()
    {
        if(!isFristSet)
        {
            isFristSet = true;
            labNameStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(labNameStringKey);
        }
        labInfoWindow.Close();
        if (LabSystem.Instance.isTimerZero)
            labCompleteWindwo.Open();
        switch (Global.language)
        {
            case Language.KOR:
                labNameTextMeshProUGUI.text = labNameStringTableData.KOR;
                break;
            case Language.ENG:
                labNameTextMeshProUGUI.text = labNameStringTableData.ENG;
                break;
        }
        base.Open();
        if (!isLoad)
        {
            LabSystem.Instance.LoadRe1(GameData.Re_AtkSaveDataList);
            LabSystem.Instance.LoadRe2(GameData.Re_HPSaveDataList);
            LabSystem.Instance.LoadRe3(GameData.Re_CriSaveDataList);
            LabSystem.Instance.LoadRe4(GameData.Re_SilupSaveDataList);
            LabSystem.Instance.LoadRe5(GameData.Re_MidAtkSaveDataList);
            LabSystem.Instance.LoadRe6(GameData.Re_MidHPSaveDataList);

            if (LabSystem.Instance.isResearching)
            {
                labInfoWindow.SetVertex(LabSystem.Instance.labType, LabSystem.Instance.labStringTableName, LabSystem.Instance.labBuffStringTable, LabSystem.Instance.labTalbeData, LabSystem.Instance.level,false);
                LabSystem.Instance.Load();
                var prevData = DateTime.ParseExact(GameData.exitTime.ToString(), GameData.datetimeString, null);
                var now = DateTime.Now.ToString(GameData.datetimeString);
                var date = DateTime.ParseExact(now, GameData.datetimeString, null);
                TimeSpan timeDifference = date.Subtract(prevData);
                LabSystem.Instance.timerTic -= (int)timeDifference.TotalSeconds * LabSystem.Instance.milSeconds;
                if (LabSystem.Instance.timerTic < 0)
                {
                    LabSystem.Instance.timerTic = 1;
                }
            }
            isLoad = true;
        }
    }

    public override void Close()
    {
        if ((ButtonList.mainButton & ButtonType.Lab) != 0)
            ButtonList.mainButton &= ~ButtonType.Lab;

        labInfoWindow.Close();
        labCompleteWindwo.Close();
        base.Close();
    }

}
