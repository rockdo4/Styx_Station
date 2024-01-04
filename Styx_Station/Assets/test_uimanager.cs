using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_uimanager : MonoBehaviour
{
    public PlayerController test;
    public GameObject castZone;
    public Camera uiCamera;
    private void Awake()
    {
        UIManager.Instance.gameObject.SetActive(true);

        var state = StateSystem.Instance;
        state.TotalUpdate();
        SkillManager.Instance.player = test.gameObject;
        SkillManager.Instance.castZone = castZone;
        UIManager.Instance.gameObject.GetComponent<Canvas>().worldCamera = uiCamera;
        UIManager.Instance.questSystemUi.PlayDungeon((int)DungeonType.SweepHomeBaseAndClean);
    }
    private void OnEnable()
    {
        SkillManager.Instance.SetCaztZone(castZone);
    }
}
