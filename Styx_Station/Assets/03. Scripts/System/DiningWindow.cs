using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningWindow : Window
{
    public DingingRoomUpgradeWindow diningRoomUpgradeWindows;
    public DingingRoomInfoPossibilityWindow infoWindow;

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        diningRoomUpgradeWindows.Close();
        infoWindow.Close();
        base.Close();
    }
}
