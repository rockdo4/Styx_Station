using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseCustomButton : MonoBehaviour
{
    public TextMeshProUGUI itemname;
    public int itemIndex;
    public ItemType type;

    public void OnClickDequip()
    {
        if (itemIndex < 0)
            return;

        itemIndex = -1;
        itemname.text = "None";
    }
}
