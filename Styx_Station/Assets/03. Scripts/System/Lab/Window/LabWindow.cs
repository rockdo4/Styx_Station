using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabWindow : Window
{
    public LabInfoWindow labInfoWindow;
    public LabCompleteWindwo labCompleteWindwo;
    public override void Open()
    {
        labInfoWindow.Close();
        if(LabSystem.Instance.isTimerZero) 
            labCompleteWindwo.Open();
        base.Open();
    }

    public override void Close()
    {
        labInfoWindow.Close();
        labCompleteWindwo.Close();
        base.Close();
    }

}
