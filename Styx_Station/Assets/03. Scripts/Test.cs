using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InventorySystem.Instance.FixedInventoryImport();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
