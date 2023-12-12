using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabAtk1Vertex : LabMainVertex
{
    

    private void OnEnable()
    {
        SetEdgesImageColor();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isClear = true;
            SetEdgesImageColor();
        }
    }
}
