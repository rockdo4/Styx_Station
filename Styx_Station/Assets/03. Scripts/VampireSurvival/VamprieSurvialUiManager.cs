using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VamprieSurvialUiManager : MonoBehaviour
{
    private static VamprieSurvialUiManager instance;
    public static VamprieSurvialUiManager Instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                instance = FindObjectOfType<VamprieSurvialUiManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return instance;
        }
    }
    public VamprieSurvialJoystick vamprieJoystick;
    private PointerEventData vampriePointerEventData;



    public Slider playerExpSlider;
    public Slider gameTimerSlider;
    public TextMeshProUGUI gameTimerTextMeshProUGUI;
    public bool isGameOver; // �Լ� ������ �˾� ����


    private void Awake()
    {
        if (Instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }
        UIManager.Instance.gameObject.SetActive(false);


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
        if(!VampireSurvivalGameManager.Instance.isPause)
        {
            DrawAndMoveJoystick();

        }
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

    public void JoysitckDragUp()
    {
        vamprieJoystick.gameObject.SetActive(false);
        vampriePointerEventData.position = Input.mousePosition;
        vamprieJoystick.OnPointerUp(vampriePointerEventData);
    }    


    public void TestC()
    {
        Debug.Log("TTT");
    }
    public void TestCode()
    {
        SceneManager.LoadScene("Table_2 LswPlayerStats");
        Destroy(gameObject);
        UIManager.Instance.gameObject.SetActive(true);
    }
    public void TestCode123()
    {
        SceneManager.LoadScene("Test_VampireSurvival");
    }
}
