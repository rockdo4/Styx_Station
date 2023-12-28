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
        if((ButtonList.mainButton & ButtonType.DiningRoom) != 0)
            ButtonList.mainButton &= ~ButtonType.DiningRoom;

        diningRoomUpgradeWindows.Close();
        infoWindow.Close();
        base.Close();
    }
}
