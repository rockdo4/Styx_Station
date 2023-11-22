using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InventorySystem.Instance.Setting();


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            InventorySystem.Instance.test();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventorySystem.Instance.test1();
        }
    }
}
