using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VamprieSurvialUiManager : Singleton<VamprieSurvialUiManager>
{
    public VamprieSurvialJoystick vamprieJoystick;
    private PointerEventData vampriePointerEventData;

    private void Awake()
    {

    }

    private void Start()
    {
        vamprieJoystick.gameObject.SetActive(false);
        if(vampriePointerEventData == null)
        {
            vampriePointerEventData = new PointerEventData(EventSystem.current);
        }
    }
    void Update()
    {
        DrawAndMoveJoystick();
    }

    private void DrawAndMoveJoystick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vamprieJoystick.gameObject.SetActive(true);
            vampriePointerEventData.position = Input.mousePosition;
            vamprieJoystick.OnPointerDown(vampriePointerEventData);
        }
        else if (Input.GetMouseButton(0))
        {
            vampriePointerEventData.position = Input.mousePosition;
            vamprieJoystick.OnDrag(vampriePointerEventData);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            vamprieJoystick.gameObject.SetActive(false);
            vampriePointerEventData.position = Input.mousePosition;
            vamprieJoystick.OnPointerUp(vampriePointerEventData);
        }
    }
    public void TestC()
    {
        Debug.Log("TTT");
    }
    public void TestCode()
    {
        SceneManager.LoadScene("LSW_Test_Scene 1");
        Destroy(gameObject);
    }
    public void TestCode123()
    {
        SceneManager.LoadScene("Test_VampireSurvival");
    }
}
