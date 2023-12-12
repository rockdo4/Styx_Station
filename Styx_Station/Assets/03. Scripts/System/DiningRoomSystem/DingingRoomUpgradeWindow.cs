using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DingingRoomUpgradeWindow : Window
{
    public Button upgradeButton;
    public DingingRoomInfoPossibilityWindow windwo;
    public override void Open()
    {
        upgradeButton.interactable = false;
        windwo.Close();
        base.Open();
    }

    public override void Close()
    {
        upgradeButton.interactable = true;
        base.Close();
    }
    //ddd

}
