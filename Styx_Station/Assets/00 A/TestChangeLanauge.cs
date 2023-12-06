using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeLanauge : Singleton<TestChangeLanauge>
{
    
    public StringTable stringTable;
    private void Start()
    {
        if(stringTable == null) { }
            stringTable = new StringTable();
    }
}
