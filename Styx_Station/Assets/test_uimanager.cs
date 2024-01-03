using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_uimanager : MonoBehaviour
{
    public PlayerController test;
    public GameObject castZone;
    private void Awake()
    {
        UIManager.Instance.gameObject.SetActive(true);

        var state = StateSystem.Instance;
        state.TotalUpdate();
        SkillManager.Instance.player = test.gameObject;
        SkillManager.Instance.castZone = castZone;

    }
    private void OnEnable()
    {
        SkillManager.Instance.SetCaztZone(castZone);
    }
}
