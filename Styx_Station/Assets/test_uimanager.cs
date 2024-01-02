using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_uimanager : MonoBehaviour
{
    public PlayerController test;
    private void Awake()
    {
        UIManager.Instance.gameObject.SetActive(true);

        var state = StateSystem.Instance;
        state.TotalUpdate();
        SkillManager.Instance.player = test.gameObject;
    }
}
