using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventoryWindow[] windows;

    public InventoryType currentWindow;

    public Button inventory;
    public Button custom;
    public Button State;
    public Button skill;

    public void Open(InventoryType inventoryType)
    {
        if (windows[(int)currentWindow].gameObject.activeSelf)
            windows[(int)currentWindow].Close();

        currentWindow = inventoryType;

        windows[(int)inventoryType].Open();
    }

    public void OnClickInventory()
    {
        Open(InventoryType.Inventory);
    }

    public void OnClickCustom()
    {
        Open(InventoryType.Custom);
    }

    public void OnClickState()
    {
        Open(InventoryType.State);
    }

    public void OnClickSkill()
    {
        Open(InventoryType.Skill);
    }
}
