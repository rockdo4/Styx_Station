using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testYY : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            UIManager.Instance.SetBossRushIndex(1);
            SceneManager.LoadScene("YYL_BossRush");
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)) 
        {
            SceneManager.LoadScene("YYL_BossRush 1");
        }
    }
}
