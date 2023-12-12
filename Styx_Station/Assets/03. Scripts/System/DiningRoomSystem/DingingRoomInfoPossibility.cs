using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DingingRoomInfoPossibilityWindow : Window
{
    public Button infoPossibilityButton;
    public DingingRoomUpgradeWindow window;
    public override void Open()
    {
        infoPossibilityButton.interactable = false;
        window.Close();
        base.Open();
    }

    public override void Close()
    {
        infoPossibilityButton.interactable = true;
        base.Close();
    }
    //aa

}
