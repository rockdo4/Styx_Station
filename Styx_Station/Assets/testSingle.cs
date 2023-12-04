using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSingle : Singleton<testSingle>
{
    public GameObject obj;
    private void Start()
    {
        Debug.Log("A");
    }
}
