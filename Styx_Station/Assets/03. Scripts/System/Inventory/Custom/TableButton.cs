using TMPro;
using UnityEngine;

public class TableButton : MonoBehaviour
{
    public CustomWindow customWindow;

    public TextMeshProUGUI tableName;
    
    public void OnClickTalbe()
    {
        if (customWindow == null)
            return;

        var table = customWindow.GetComponent<CustomWindow>().tableName;

        if (table == null)
            return;

        table.text = tableName.text; 
    }
}
