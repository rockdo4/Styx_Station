using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabWindow : Window
{
    public LabInfoWindow labInfoWindow;
    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        labInfoWindow.Close();
        base.Close();
    }

}
