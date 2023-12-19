using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabWindow : Window
{
    public LabInfoWindow labInfoWindow;
    public LabCompleteWindwo labCompleteWindwo;
    private bool isLoad;
    public override void Open()
    {



        labInfoWindow.Close();
        if (LabSystem.Instance.isTimerZero)
            labCompleteWindwo.Open();
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
            }
            isLoad = true;
        }
    }

    public override void Close()
    {
        labInfoWindow.Close();
        labCompleteWindwo.Close();
        base.Close();
    }

}
