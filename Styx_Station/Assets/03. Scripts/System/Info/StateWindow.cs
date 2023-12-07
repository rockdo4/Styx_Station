using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWindow : SubWindow
{
    public override void Open()
    {
        CurrencyManager.SetPlayerStatsAllRest();
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }
}
