using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    private StringTable stringTable;

    public bool shop = false;
    public bool dining = false;
    public bool lab = false;
    public bool clean = false;
    public bool failrue = false;

    public int tutorialIndex = 0;

    private bool message = false;
    private bool next = false;

    public GameObject tutorial;

    public bool playTutorial = false;

    public List<GameObject> mask = new List<GameObject>();

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
        tutorial.SetActive(true);

        mask[0].SetActive(true);

        FindTutorialIndex();
    }

    public void FindTutorialIndex()
    {
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
                Shop_1();
                break;

            case 21:
                Shop_2();
                break;

            case 22:
            case 23:
                tutorialIndex = 24;
                Log_15();
                break;

            case 24:
                Log_15();
                break;

            case 25:
                Info_1();
                break;

            case 26:
                Info_3();
                break;

            case 27:
                Info_4();
                break;

            case 28:
                Info_5();
                break;

            case 29:
                Log_16();
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

        if (!mask[25].activeSelf)
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
    private void Quest_1()
    {
        mask[1].SetActive(true);
    }

    private void Quest_2()
    {
        mask[1].SetActive(true);
    }

    private void Quest_3()
    {
        mask[1].SetActive(true);
    }

    private void Info_1()
    {
        mask[0].SetActive(false);

        mask[6].SetActive(true);
    }

    private void Info_2()
    {
        UIManager.Instance.OnClickInfo();

        mask[7].SetActive(true);
    }

    private void Info_3()
    {
        UIManager.Instance.OnClickInfo();

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
    }

    private void Info_5()
    {
        UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().OnClickInventory();

        var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[0].GetComponent<WeaponType>();

        ui.weaponButtons[0].GetComponent<ItemButton>().OnClickWeaponOpenInfo(ui);

        mask[15].SetActive(true);
    }

    private void Shop_1()
    {
        mask[9].SetActive(true);
    }

    private void Shop_2()
    {
        UIManager.Instance.OnClickShop();

        mask[10].SetActive(true);
    }

    private IEnumerator TextCoroutine(string text)
    {
        for(int i = 0; i< text.Length; i++)
        {
            crewText.text = text.Substring(0, i);
            yield return timer;
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

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

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

                        // 웨이브 스타트

                        tutorialIndex++;


                        //파인드 삭제
                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 3:
                    if (!message && next)
                    {
                        next = false;

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 4:
                    if (!message && next)
                    {
                        next = false;

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 5:
                    if (!message && next)
                    {
                        next = false;

                        mask[2].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 6:
                    if (!message && next)
                    {
                        next = false;

                        mask[3].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 7:
                    if (!message && next)
                    {
                        next = false;

                        mask[4].SetActive(false);

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 8:
                    if (!message && next)
                    {
                        next = false;

                        mask[0].SetActive(false);

                        mask[6].SetActive(true);

                        tutorialIndex++;
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 11:
                    if (!message && next)
                    {
                        next = false;

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 12:
                    if (!message && next)
                    {
                        next = false;

                        mask[0].SetActive(false);
                        textBox.SetActive(false);

                        tutorialIndex++;

                        mask[8].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 15:
                    if (!message && next)
                    {
                        next = false;

                        tutorialIndex++;

                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 16:
                    if (!message && next)
                    {
                        next = false;

                        mask[0].SetActive(false);
                        textBox.SetActive(false);

                        //웨이브 시작
                        tutorialIndex++;

                        //웨이브 적용 시 파인드 삭제
                        FindTutorialIndex();
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 17:
                    if (!message && next)
                    {
                        next = false;

                        textBox.SetActive(false);
                        mask[0].SetActive(false);

                        tutorialIndex++;

                        mask[1].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 19:
                    if (!message && next)
                    {
                        next = false;

                        textBox.SetActive(false);
                        mask[0].SetActive(false);

                        tutorialIndex++;

                        mask[9].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 24:
                    if (!message && next)
                    {
                        next = false;

                        mask[0].SetActive(false);

                        tutorialIndex++;

                        mask[6].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

                        message = false;
                        next = true;
                    }
                    break;

                case 29:
                    if (!message && next)
                    {
                        next = false;

                        mask[25].SetActive(false);

                        tutorialIndex++;

                        mask[18].SetActive(true);
                    }
                    else if (message && !next)
                    {
                        StopCoroutine(text);
                        text = null;

                        crewText.text = str;

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

            UIManager.Instance.questSystemUi.GetComponent<QuestSystemUi>().questButton.onClick.Invoke();

            tutorialIndex++;

            mask[0].SetActive(true);

            FindTutorialIndex();
        }
        else if(tutorialIndex == 18)
        {
            mask[1].SetActive(false);

            UIManager.Instance.questSystemUi.GetComponent<QuestSystemUi>().questButton.onClick.Invoke();

            tutorialIndex++;

            tutorial.SetActive(false);
            playTutorial = false;
        }
    }

    public void OnClickInfo()
    {
        if(tutorialIndex == 9)
        {
            mask[6].SetActive(false);

            UIManager.Instance.OnClickInfo();

            tutorialIndex++;

            textBox.SetActive(false);
            mask[7].SetActive(true);

            FindTutorialIndex();
        }
        else if(tutorialIndex == 25)
        {
            mask[6].SetActive(false);

            UIManager.Instance.OnClickInfo();

            tutorialIndex++;

            textBox.SetActive(false);

            mask[13].SetActive(true);
        }
    }

    public void OnClickStateUpgrade()
    {
        if(tutorialIndex == 10)
        {
            mask[7].SetActive(false);

            // 스텟 오르는거 전부 Private 라 접근이 안됨

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

            UIManager.Instance.OnClickClose();

            tutorialIndex++;

            mask[1].SetActive(true);
        }
    }

    public void OnClickShop()
    {
        if (tutorialIndex == 20)
        {
            mask[9].SetActive(false);

            UIManager.Instance.OnClickShop();

            tutorialIndex++;

            mask[10].SetActive(true);
        }
    }

    public void OnClickGacha()
    {
        if(tutorialIndex == 21)
        {
            mask[10].SetActive(false);

            UIManager.Instance.windows[6].GetComponent<GachaWindow>().itemGacha.GetComponent<ItemGacha>().OnClickMaxGacha();

            tutorialIndex++;

            mask[11].SetActive(true);
        }
    }

    public void OnClickGachaInfoExit()
    {
        if(tutorialIndex == 22)
        {
            mask[11].SetActive(false);

            UIManager.Instance.windows[6].GetComponent<GachaWindow>().info.GetComponent<GachaInfo>().OnClickGachaInfoClose();

            tutorialIndex++;

            mask[12].SetActive(true);
        }
    }

    public void OnClickShopExit()
    {
        if(tutorialIndex == 23)
        {
            mask[12].SetActive(false);

            UIManager.Instance.OnClickClose();

            tutorialIndex++;

            FindTutorialIndex();
        }
    }

    public void OnClickInventory()
    {
        if(tutorialIndex == 26)
        {
            mask[13].SetActive(false);

            if (!InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].acquire)
                InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].acquire = true;

            if (InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].stock < 2)
                InventorySystem.Instance.inventory.GetComponent<Inventory>().weapons[0].stock = 2;


            UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().OnClickInventory();

            tutorialIndex++;

            mask[14].SetActive(true);
        }
    }

    public void OnClickItem()
    {
        if (tutorialIndex == 27)
        {
            mask[14].SetActive(false);

            var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[0].GetComponent<WeaponType>();

            ui.weaponButtons[0].GetComponent<ItemButton>().OnClickWeaponOpenInfo(ui);

            tutorialIndex++;

            mask[15].SetActive(true);
        }
    }

    public void OnClickItemEquip()
    {
        if(tutorialIndex == 28)
        {
            mask[14].SetActive(false);

            var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[0].GetComponent<WeaponType>();

            ui.info.GetComponent<WeaponEquipInfoUi>().OnClickWeaponEquip();

            tutorialIndex++;

            FindTutorialIndex();
        }
    }

    public void OnClickItemUpgrade()
    {
        if(tutorialIndex == 30)
        {
            mask[18].SetActive(false);

            var ui = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>().inventoryTypes[0].GetComponent<WeaponType>();

            ui.info.GetComponent<WeaponEquipInfoUi>().OnClickUpgrade();

            tutorialIndex++;

        }
    }
}
