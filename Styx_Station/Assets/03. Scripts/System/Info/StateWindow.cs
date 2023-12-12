using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWindow : SubWindow
{

    private void Start()
    {
    }
    public override void Open()
    {
        CurrencyManager.SetPlayerStatsAllRest();
        SharedPlayerStats.CheckLimitAll();
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }
}
