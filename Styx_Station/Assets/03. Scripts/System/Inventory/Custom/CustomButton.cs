using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomButton : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public int itemIndex;
    public ItemType type;

    public void OnClickEquip(BaseCustomButton button)
    {
        if (itemIndex < 0)
            return;

        button.itemIndex = itemIndex;
        button.type = type;
        button.itemname.text = itemName.text;
    }
}
