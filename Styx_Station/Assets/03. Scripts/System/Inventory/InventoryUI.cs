using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public EquipWindow equipWindow;
    public StateWindow stateWindow;
    public CustomWindow customWindow;

    public void Setting()
    {
        equipWindow.Setting();
        customWindow.Setting();
        stateWindow.Setting();
    }
}
