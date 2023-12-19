using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class LabCompleteWindwo : Window
{
    public LabInfoWindow labInfoWindow;

    public string labCompletetStringKey;
    public string labCompleteButtonStringKey;

    public TextMeshProUGUI labNameText;
    public TextMeshProUGUI labCompleteText;
    public TextMeshProUGUI labBuffText;
    public TextMeshProUGUI labCompleteButtonText;

    private StringTableData labCompleteStringTable;
    private StringTableData labCompleteButtonStringTable;

    private bool isOneStringTableSetting;

    public override void Open()
    {
        labInfoWindow.Close();
        if(!isOneStringTableSetting)
        {
            isOneStringTableSetting = true;
            labCompleteStringTable = MakeTableData.Instance.stringTable.GetStringTableData(labCompleteButtonStringKey);
            labCompleteButtonStringTable = MakeTableData.Instance.stringTable.GetStringTableData(labCompletetStringKey);
        }

        switch (Global.language)
        {
            case Language.KOR:
                labNameText.text = $"{labInfoWindow.labTypeNameStringDatas.KOR} {labInfoWindow.level + 1} {labInfoWindow.levelStringTableData.KOR}";
                labCompleteText.text = $"{labCompleteButtonStringTable.KOR}"; 
                labBuffText.text = $"{labInfoWindow.labTypeBuffStringDatas.KOR} {labInfoWindow.buffPercent}% {labInfoWindow.increaseStringTableData.KOR}";
                labCompleteButtonText.text = $"{labCompleteStringTable.KOR}";
                break;
            case Language.ENG:
                labNameText.text = $"{labInfoWindow.labTypeNameStringDatas.ENG} {labInfoWindow.level + 1} {labInfoWindow.levelStringTableData.ENG}";
                labCompleteText.text = $"{labCompleteButtonStringTable.ENG}";
                labBuffText.text = $"{labInfoWindow.labTypeBuffStringDatas.ENG} {labInfoWindow.buffPercent}% {labInfoWindow.increaseStringTableData.ENG}";
                labCompleteButtonText.text = $"{labCompleteStringTable.ENG}"; 
                break;
        }
        base.Open();
    }

    public override void Close()
    {
        
        base.Close();
    }

    public void DoneResearching()
    {
        if(LabSystem.Instance.isResearching)
        {
            LabSystem.Instance.isResearching = false;
            LabSystem.Instance.IsDoneTime();
            LabSystem.Instance.isTimerZero = false;
        }
    }
}
