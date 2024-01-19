using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitWindow : Window
{
    private bool isAwkeSetting;

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
