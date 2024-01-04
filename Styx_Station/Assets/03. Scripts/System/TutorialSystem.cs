using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialSystem : MonoBehaviour
{
    private StringTable stringTable;

    public bool loadTutorial = false;

    public bool shop = false;
    public bool dining = false;
    public bool lab = false;
    public bool clean = false;
    public bool failrue = false;

    public int tutorialIndex = 0;
    //연구 끝날 때 55번

    private bool message = false;
    private bool next = false;

    public GameObject tutorial;

    public bool playTutorial = false;
    public bool stop = false;

    public List<GameObject> mask = new List<GameObject>();
    public List<GameObject> finger = new List<GameObject>();

    private Coroutine text;

    private WaitForSeconds timer = new WaitForSeconds(0.05f);

    public GameObject textBox;

    private string str;

    public TextMeshProUGUI crewName;
    public TextMeshProUGUI crewText;

    public void StartTutorial()
    {
        if (stringTable == null)
        {
            if (MakeTableData.Instance.stringTable == null)
                MakeTableData.Instance.stringTable = new StringTable();

            stringTable = MakeTableData.Instance.stringTable;
        }

        UIManager.Instance.OnClickClose();

        tutorial.SetActive(true);

        mask[26].SetActive(false);
        mask[0].SetActive(true);

        FindTutorialIndex();
    }

    public void FindTutorialIndex()
    {
        loadTutorial = true;

        switch (tutorialIndex)
        {
            case 0:
                Log_1();
                break;

            case 1:
                Log_2();
                break;

            case 2:
                Quest_1();
                break;

            case 3:
                Log_3();
                break;

            case 4:
                Log_4();
                break;

            case 5:
                Log_5();
                break;

            case 6:
                Log_6();
                break;

            case 7:
                Log_7();
                break;

            case 8:
                Log_8();
                break;

            case 9:
                Info_1();
                break;

            case 10:
                Info_2();
                break;

            case 11:
                Log_9();
                break;

            case 12:
                Log_10();
                break;

            case 13:
                tutorialIndex++;
                Quest_2();
                break;

            case 14:
                Quest_2();
                break;

            case 15:
                Log_11();
                break;

            case 16:
                Log_12();
                break;

            case 17:
                Log_13();
                break;

            case 18:
                Quest_3();
                break;

            case 19:
                Log_14();
                break;

            case 20:
                Log_15();
                break;

            case 21:
                Shop_1();
                break;

            case 22:
                Shop_2();
                break;

            case 23:
            case 24:
                tutorialIndex = 25;
                Log_16();
                break;

            case 25:
                Log_16();
                break;

            case 26:
                Info_1();
                break;

            case 27:
                Info_3();
                break;

            case 28:
                Info_4();
                break;

            case 29:
                Info_5();
                break;

            case 30:
                Log_17();
                break;

            case 31:
            case 32:
            case 33:
                tutorialIndex = 34;

                tutorial.SetActive(false);
                stop = false;
                playTutorial = false;
                loadTutorial = false;

                WaveManager.Instance.StartWave();
                break;
            case 34:
                Log_18();
                break;

            case 35:
                Log_19();
                break;

            case 36:
            case 37:
                tutorialIndex = 38;
                Dining();
                break;
            case 38:
                Log_20();
                break;

            case 39:
                Log_21();
                break;

            case 40:
                Log_22();
                break;

            case 41:
                Log_23();
                break;

            case 42:
                Log_24();
                break;

            case 43:
            case 44:
                tutorialIndex = 38;
                Dining();
                break;
            
            case 45:
                Log_25();
                break;

            case 46:
                Log_26();
                break;

            case 47:
                Log_27();
                break;

            case 50:
                Log_28();
                break;

            case 51:
                Log_29();
                break;

            case 52:
                Log_30();
                break;

            case 48:
            case 49:
            case 53:
                tutorialIndex = 46;
                Log_26();
                break;

            default:
                break;
        }
    }

    private void Log_1()
    {
        message = true;

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log01").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log01").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_2()
    {
        message = true;

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log02").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log02").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_3()
    {
        message = true;

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log03").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log03").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_4()
    {
        message = true;

        mask[2].SetActive(true);

        if (mask[0].activeSelf)
            mask[0].SetActive(false);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log04").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log04").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_5()
    {
        message = true;

        if (!mask[2].activeSelf)
            mask[2].SetActive(true);

        if (mask[0].activeSelf)
            mask[0].SetActive(false);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log05").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log05").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_6()
    {
        message = true;

        mask[3].SetActive(true);

        if (mask[2].activeSelf)
            mask[2].SetActive(false);

        if (mask[0].activeSelf)
            mask[0].SetActive(false);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log06").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log06").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_7()
    {
        message = true;

        mask[4].SetActive(true);

        if (mask[0].activeSelf)
            mask[0].SetActive(false);

        if (mask[3].activeSelf)
            mask[3].SetActive(false);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log07").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log07").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_8()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log08").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log08").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_9()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log09").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log09").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_10()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log10").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log10").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_11()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log11").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log11").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_12()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log12").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log12").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_13()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log13").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log13").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_14()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        shop = true;

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log14").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log14").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_15()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log15").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log15").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_16()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log16").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log16").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_17()
    {
        message = true;

        if (!mask[25].activeSelf)
            mask[25].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log17").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log17").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_18()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if(!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        dining = true;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log18").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log18").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_19()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log19").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log19").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_20()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log20").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log20").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_21()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log21").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log21").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_22()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log22").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log22").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_23()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log23").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log23").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_24()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log24").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log24").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_25()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log25").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log25").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_26()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        lab = true;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log26").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log26").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_27()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log27").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log27").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_28()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log28").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log28").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_29()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log29").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log29").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Log_30()
    {
        message = true;

        if (!mask[0].activeSelf)
            mask[0].SetActive(true);

        if (!textBox.activeSelf)
            textBox.SetActive(true);

        str = string.Empty;

        if (Global.language == Language.KOR)
            str = stringTable.GetStringTableData("Tutorial_Log30").KOR;
        else if (Global.language == Language.ENG)
            str = stringTable.GetStringTableData("Tutorial_Log30").ENG;

        if (text != null)
        {
            StopCoroutine(text);
            text = null;
        }

        text = StartCoroutine(TextCoroutine(str));
    }

    private void Quest_1()
    {
        mask[0].SetActive(false);
        mask[1].SetActive(true);
        finger[1].SetActive(true);
    }

    private void Quest_2()
    {
        mask[1].SetActive(true);
        finger[1].SetActive(true);
    }

    private void Quest_3()
    {
        mask[1].SetActive(true);
        finger[1].SetActive(true);
    }

    private void Info_1()
    {
        mask[0].SetActive(false);
        finger[2].SetActive(true);
        mask[6].SetActive(true);
    }

    private void Info_2()
    {
        UIManager.Instance.OnClickInfo();
        finger[3].SetActive(true);
        mask[7].SetActive(true);
    }

    private void Info_3()
    {
        UIManager.Instance.OnClickInfo();
        finger[9].SetActive(true);
        mask[13].SetActive(true);
    }

    private void Info_4()
    {
        if (!InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].acquire)
            InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].acquire = true;

        if (InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].stock < 2)
            InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].stock = 2;

        UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().OnClickInventory();

        mask[14].SetActive(true);
        finger[10].SetActive(true);
    }

    private void Info_5()
    {
        UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().OnClickInventory();

        var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[0].GetComponent<WeaponType>();

        ui.weaponButtons[0].GetComponent<ItemButton>().OnClickWeaponOpenInfo(ui);

        mask[15].SetActive(true);
        finger[11].SetActive(true);
    }

    private void Shop_1()
    {
        mask[9].SetActive(true);
        finger[5].SetActive(true);
    }

    private void Shop_2()
    {
        UIManager.Instance.OnClickShop();

        mask[10].SetActive(true);
        finger[6].SetActive(true);
    }

    private void Dining()
    {
        UIManager.Instance.OnClickDiningRoom();

        FindTutorialIndex();
    }

    private IEnumerator TextCoroutine(string text)
    {
        for(int i = 0; i< text.Length; i++)
        {
            crewText.text = text.Substring(0, i);
            yield return timer;
        }

        switch(tutorialIndex)
        {
            case 0:
            case 1:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 11:
            case 12:
            case 15:
            case 16:
            case 17:
            case 19:
            case 20:
            case 25:
            case 30:
            case 34:
            case 35:
            case 38:
            case 39:
            case 40:
            case 41:
            case 42:
            case 45:
            case 46:
            case 50:
            case 51:
            case 52:
                finger[0].SetActive(true); 
                break;

        }

        message = false;
        next = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown && playTutorial)
        {
            switch (tutorialIndex)
            {
                case 0:
                    if (!message && next)
                    {
                        next = false;
                        tutorialIndex++;

                        finger[0].SetActive(false);

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 1:
                    if (!message && next)
                    {
                        next = false;

                        mask[0].SetActive(false);
                        textBox.SetActive(false);

                        finger[0].SetActive(false);

                        mask[26].SetActive(true);

                        WaveManager.Instance.StartWave();

                        stop = true;

                        tutorialIndex++;
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 3:
                    if (!message && next)
                    {
                        next = false;

                        tutorialIndex++;

                        finger[0].SetActive(false);

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 4:
                    if (!message && next)
                    {
                        next = false;

                        tutorialIndex++;


                        finger[0].SetActive(false);


                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 5:
                    if (!message && next)
                    {
                        next = false;

                        mask[2].SetActive(false);
                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 6:
                    if (!message && next)
                    {
                        next = false;

                        mask[3].SetActive(false);
                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 7:
                    if (!message && next)
                    {
                        next = false;

                        mask[4].SetActive(false);
                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 8:
                    if (!message && next)
                    {
                        next = false;

                        mask[0].SetActive(false);

                        finger[0].SetActive(false);
                        finger[2].SetActive(true);

                        mask[6].SetActive(true);

                        tutorialIndex++;
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 11:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 12:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);
                        mask[0].SetActive(false);
                        textBox.SetActive(false);

                        tutorialIndex++;

                        mask[8].SetActive(true);
                        finger[4].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 15:
                    if (!message && next)
                    {
                        next = false;

                        tutorialIndex++;
                        
                        finger[0].SetActive(false);

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 16:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);
                        mask[0].SetActive(false);
                        textBox.SetActive(false);

                        mask[26].SetActive(true);

                        WaveManager.Instance.StartWave();

                        stop = true;

                        tutorialIndex++;
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 17:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);
                        textBox.SetActive(false);
                        mask[0].SetActive(false);

                        tutorialIndex++;

                        mask[1].SetActive(true);
                        finger[1].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 19:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 20:
                    if (!message && next)
                    {
                        next = false;

                        textBox.SetActive(false);
                        mask[0].SetActive(false);

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        finger[5].SetActive(true);
                        mask[9].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        finger[0].SetActive(true);

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 25:
                    if (!message && next)
                    {
                        next = false;

                        mask[0].SetActive(false);
                        finger[0].SetActive(false);

                        tutorialIndex++;

                        mask[6].SetActive(true);
                        finger[2].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        finger[0].SetActive(true);

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 30:
                    if (!message && next)
                    {
                        next = false;

                        mask[25].SetActive(false);
                        textBox.SetActive(false);
                        finger[0].SetActive(false);

                        tutorialIndex++;

                        mask[16].SetActive(true);
                        finger[12].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        finger[0].SetActive(true);

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 34:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 35:
                    if (!message && next)
                    {
                        next = false;

                        mask[0].SetActive(false);
                        textBox.SetActive(false);
                        finger[0].SetActive(false);

                        tutorialIndex++;

                        finger[14].SetActive(true);
                        mask[18].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 38:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 39:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 40:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 41:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        DiningRoomSystem.Instance.counting++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 42:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);
                        mask[0].SetActive(false);
                        textBox.SetActive(false);

                        tutorialIndex++;

                        mask[20].SetActive(true);
                        finger[16].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 45:
                    //3차 튜토리얼 종료
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);
                        mask[0].SetActive(false);
                        textBox.SetActive(false);

                        tutorialIndex++;

                        tutorial.SetActive(false);
                        stop = false;
                        playTutorial = false;
                        loadTutorial = false;

                        WaveManager.Instance.StartWave();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        finger[0].SetActive(true);

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 46:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 47:
                    if (!message && next)
                    {
                        next = false;
                        finger[0].SetActive(false);
                        mask[0].SetActive(false);
                        textBox.SetActive(false);

                        tutorialIndex++;

                        finger[14].SetActive(true);
                        mask[18].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 50:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 51:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;

                case 52:
                    if (!message && next)
                    {
                        next = false;

                        finger[0].SetActive(false);
                        mask[0].SetActive(false);
                        textBox.SetActive(false);

                        tutorialIndex++;

                        mask[23].SetActive(true);
                        finger[19].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        finger[0].SetActive(true);

                        message = false;
                        next = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void OnClickQuest()
    {
        if (tutorialIndex == 2 || tutorialIndex == 14)
        {
            mask[1].SetActive(false);

            finger[1].SetActive(false);

            UIManager.Instance.questSystemUi.GetComponent<QuestSystemUi>().questButton.onClick.Invoke();

            tutorialIndex++;

            mask[0].SetActive(true);

            FindTutorialIndex();
        }
        // 1차 튜토리얼 종료
        else if(tutorialIndex == 18)
        {
            mask[1].SetActive(false);
            finger[1].SetActive(false);

            UIManager.Instance.questSystemUi.GetComponent<QuestSystemUi>().questButton.onClick.Invoke();

            tutorialIndex++;

            tutorial.SetActive(false);
            stop = false;
            playTutorial = false;
            loadTutorial = false;

            WaveManager.Instance.StartWave();
        }
    }

    public void OnClickInfo()
    {
        if(tutorialIndex == 9)
        {
            mask[6].SetActive(false);
            finger[2].SetActive(false);

            UIManager.Instance.OnClickInfo();

            tutorialIndex++;

            textBox.SetActive(false);
            finger[3].SetActive(true);
            mask[7].SetActive(true);

            FindTutorialIndex();
        }
        else if(tutorialIndex == 26)
        {
            finger[2].SetActive(false);
            mask[6].SetActive(false);

            UIManager.Instance.OnClickInfo();

            tutorialIndex++;

            textBox.SetActive(false);

            mask[13].SetActive(true);
            finger[9].SetActive(true);
        }
    }

    public void OnClickStateUpgrade()
    {
        if(tutorialIndex == 10)
        {
            mask[7].SetActive(false);
            finger[3].SetActive(false);

            var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[0].GetComponent<PlayerStatsUIManager>();

            ui.playerStatsUiDatas[0].Tutorial();

            tutorialIndex++;

            mask[0].SetActive(true);

            FindTutorialIndex();
        }
    }

    public void OnClickExitInfo()
    {
        if (tutorialIndex == 13)
        {
            mask[8].SetActive(false);
            textBox.SetActive(false);
            finger[4].SetActive(false);

            UIManager.Instance.OnClickClose();

            tutorialIndex++;

            finger[1].SetActive(true);
            mask[1].SetActive(true);
        }
        // 2차 튜토리얼 종료
        else if(tutorialIndex == 33)
        {
            mask[8].SetActive(false);
            finger[4].SetActive(!false);

            UIManager.Instance.OnClickClose();

            tutorialIndex++;

            tutorial.SetActive(false);
            stop = false;
            playTutorial = false;
            loadTutorial = false;

            WaveManager.Instance.StartWave();
        }
    }

    public void OnClickShop()
    {
        if (tutorialIndex == 21)
        {
            mask[9].SetActive(false);
            finger[5].SetActive(false);

            UIManager.Instance.OnClickShop();

            tutorialIndex++;

            mask[10].SetActive(true);
            finger[6].SetActive(true);
        }
    }

    public void OnClickGacha()
    {
        if(tutorialIndex == 22)
        {
            mask[10].SetActive(false);
            finger[6].SetActive(false);

            UIManager.Instance.windows[6].GetComponent<GachaWindow>().itemGacha.GetComponent<ItemGacha>().OnClickMaxGacha();

            tutorialIndex++;

            mask[11].SetActive(true);
            finger[7].SetActive(true);
        }
    }

    public void OnClickGachaInfoExit()
    {
        if(tutorialIndex == 23)
        {
            mask[11].SetActive(false);
            finger[7].SetActive(false);

            UIManager.Instance.windows[6].GetComponent<GachaWindow>().info.GetComponent<GachaInfo>().OnClickGachaInfoClose();

            tutorialIndex++;

            mask[12].SetActive(true);
            finger[8].SetActive(true);
        }
    }

    public void OnClickShopExit()
    {
        if(tutorialIndex == 24)
        {
            mask[12].SetActive(false);
            finger[8].SetActive(false);

            UIManager.Instance.OnClickClose();

            tutorialIndex++;

            FindTutorialIndex();
        }
    }

    public void OnClickInventory()
    {
        if(tutorialIndex == 27)
        {
            mask[13].SetActive(false);
            finger[9].SetActive(false);

            if (!InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].acquire)
                InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].acquire = true;

            if (InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].stock < 2)
                InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].stock = 2;


            UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().OnClickInventory();

            tutorialIndex++;

            mask[14].SetActive(true);
            finger[10].SetActive(true);
        }
    }

    public void OnClickItem()
    {
        if (tutorialIndex == 28)
        {
            mask[14].SetActive(false);
            finger[10].SetActive(false);

            var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[0].GetComponent<WeaponType>();

            ui.weaponButtons[0].GetComponent<ItemButton>().OnClickWeaponOpenInfo(ui);

            tutorialIndex++;

            mask[15].SetActive(true);
            finger[11].SetActive(true);
        }
    }

    public void OnClickItemEquip()
    {
        if(tutorialIndex == 29)
        {
            mask[15].SetActive(false);
            finger[11].SetActive (false);

            var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[0].GetComponent<WeaponType>();

            ui.info.GetComponent<WeaponEquipInfoUi>().OnClickWeaponEquip();

            tutorialIndex++;

            FindTutorialIndex();
        }
    }

    public void OnClickItemUpgrade()
    {
        if(tutorialIndex == 31)
        {
            mask[16].SetActive(false);
            finger[12].SetActive(false);

            var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[0].GetComponent<WeaponType>();

            ui.info.GetComponent<WeaponEquipInfoUi>().OnClickUpgrade();

            tutorialIndex++;

            mask[17].SetActive(true);
            finger[13].SetActive(true);
        }
    }

    public void OnClickItemInfoExit()
    {
        if(tutorialIndex == 32)
        {
            mask[17].SetActive(false);
            finger[13].SetActive (false);
            var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[0].GetComponent<WeaponType>();

            ui.OnClickCloseWeaponInfo();

            tutorialIndex++;

            mask[8].SetActive(true);
            finger[4].SetActive(true);
        }
    }

    public void OnClickOpenTrain()
    {
        if (tutorialIndex == 36 || tutorialIndex == 48)
        {
            mask[18].SetActive(false);
            mask[0].SetActive(true);
            finger[14].SetActive(false);

            UIManager.Instance.CloseTrain();

            tutorialIndex++;
        }
    }

    public void OnClickFoodOpen()
    {
        if(tutorialIndex == 37)
        {
            mask[19].SetActive(false);
            finger[15].SetActive(false);

            UIManager.Instance.OnClickDiningRoom();

            tutorialIndex++;

            FindTutorialIndex();
        }
    }

    public void OnClickFoodInfo()
    {
        if(tutorialIndex==43)
        {
            mask[20].SetActive(false);
            finger[16].SetActive(false);

            UIManager.Instance.windows[1].gameObject.GetComponent<DiningRoomUIManager>().diningRoomButtdonDatas[0].onClick = true;

            tutorialIndex++;

            mask[21].SetActive(true);
            finger[17].SetActive(true);
        }
    }

    public void OnclickFoodEat()
    {
        if(tutorialIndex==44)
        {
            mask[21].SetActive(false);
            finger[17].SetActive(false);

            UIManager.Instance.windows[1].gameObject.GetComponent<DiningRoomUIManager>().EatFood();

            tutorialIndex++;

            FindTutorialIndex();
        }
    }

    public void OnClickLabOpen()
    {
        if (tutorialIndex == 49)
        {
            mask[22].SetActive(false);
            finger[18].SetActive(false);

            UIManager.Instance.OnClickLab();

            tutorialIndex++;

            FindTutorialIndex();
        }
    }

    public void OnClickLabInfo()
    {
        if(tutorialIndex==53)
        {
            mask[23].SetActive(false);
            finger[19].SetActive(false);

            UIManager.Instance.windows[2].transform.GetChild(5).gameObject.
                transform.GetChild(0).gameObject.
                transform.GetChild(0).gameObject.
                transform.GetChild(1).gameObject.
                transform.GetChild(1).gameObject.
                GetComponent<LabMainVertex>().vertexButton.onClick.Invoke();

            tutorialIndex++;

            mask[24].SetActive(true);
            finger[20].SetActive(true);
        }
    }

    public void OnClickLabStart()
    {
        if(tutorialIndex == 54)
        {
            mask[24].SetActive(false);
            finger[20].SetActive(false);

            UIManager.Instance.windows[2].GetComponent<LabWindow>().labInfoWindow.reasearchButton.onClick.Invoke();

            tutorialIndex++;

            tutorial.SetActive(false);
            stop = false;
            playTutorial = false;
        }
    }
}