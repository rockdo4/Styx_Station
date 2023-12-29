using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillGacha : MonoBehaviour
{
    private ShopSystem shop;
    private GachaInfo window;
    private StateSystem stateSystem;
    private StringTable stringTable;

    public Slider nextLevExp;

    public TextMeshProUGUI gachaName;

    public TextMeshProUGUI currentLev;
    public TextMeshProUGUI currentExp;

    public List<Button> gachaButtons = new List<Button>();

    public Button skillPer;

    public GameObject skillPerWindow;

    public int minGacha;
    public int middleGach;
    public int maxGacha;

    public int minValue;
    public int middleValue;
    public int maxValue;

    public TextMeshProUGUI minValueText;
    public TextMeshProUGUI middleValueText;
    public TextMeshProUGUI maxValueText;

    private bool first = false;
    public void GachaUpdate()
    {
        if (!first)
        {
            shop = ShopSystem.Instance;
            window = UIManager.Instance.windows[6].GetComponent<GachaWindow>().info;
            stateSystem = StateSystem.Instance;
            stringTable = MakeTableData.Instance.stringTable;

            minValueText.text = $"{minValue}";
            middleValueText.text = $"{middleValue}";
            maxValueText.text = $"{maxValue}";

            first = true;
        }
        if (Global.language == Language.KOR)
        {
            gachaName.text = $"{stringTable.GetStringTableData("Gatcha002").KOR}";
            gachaButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{minGacha}\n{stringTable.GetStringTableData("Gatcha004").KOR}";
            gachaButtons[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{middleGach}\n{stringTable.GetStringTableData("Gatcha004").KOR}";
            gachaButtons[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{maxGacha}\n{stringTable.GetStringTableData("Gatcha004").KOR}";

        }
        else if (Global.language == Language.ENG)
        {
            gachaName.text = $"{stringTable.GetStringTableData("Gatcha002").ENG}";
            gachaButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{minGacha}\n{stringTable.GetStringTableData("Gatcha004").ENG}";
            gachaButtons[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{middleGach}\n{stringTable.GetStringTableData("Gatcha004").ENG}";
            gachaButtons[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{maxGacha}\n{stringTable.GetStringTableData("Gatcha004").ENG}";
        }

        currentLev.text = $"Lv.{shop.currentSkillRank}";
        currentExp.text = $"{shop.currentSkillRankUp} / {shop.skillTable.drops[shop.currentSkillRank].RankUp}";

        nextLevExp.value = (float)shop.currentSkillRankUp / (float)shop.skillTable.drops[shop.currentSkillRank].RankUp;
    }

    public void OnClickMinGacha()
    {
        if (CurrencyManager.money3 < minValue)
            return;

        CurrencyManager.money3 -= minValue;
        shop.SkillGacha(window, minGacha);

        stateSystem.EquipUpdate();
        stateSystem.AcquireUpdate();
        stateSystem.TotalUpdate();

        window.InfoUpdate(0, 1);
        window.gameObject.SetActive(true);

        if ((ButtonList.gachaButton & gachaButton.Info) == 0)
            ButtonList.gachaButton |= gachaButton.Info;

        GachaUpdate();
    }

    public void OnClickMiddleGacha()
    {
        if (CurrencyManager.money3 < middleValue)
            return;

        CurrencyManager.money3 -= middleValue;
        shop.SkillGacha(window, middleGach);

        stateSystem.EquipUpdate();
        stateSystem.AcquireUpdate();
        stateSystem.TotalUpdate();

        window.InfoUpdate(1, 1);
        window.gameObject.SetActive(true);

        if ((ButtonList.gachaButton & gachaButton.Info) == 0)
            ButtonList.gachaButton |= gachaButton.Info;

        GachaUpdate();
    }

    public void OnClickMaxGacha()
    {
        if (CurrencyManager.money3 < maxValue)
            return;

        CurrencyManager.money3 -= maxValue;
        shop.SkillGacha(window, maxGacha);

        stateSystem.EquipUpdate();
        stateSystem.AcquireUpdate();
        stateSystem.TotalUpdate();

        window.InfoUpdate(2, 1);
        window.gameObject.SetActive(true);

        if ((ButtonList.gachaButton & gachaButton.Info) == 0)
            ButtonList.gachaButton |= gachaButton.Info;

        GachaUpdate();
    }

    public void OnClickSkillPer()
    {
        if ((ButtonList.gachaButton & gachaButton.Skill) == 0)
        {
            ButtonList.gachaButton |= gachaButton.Skill;
            skillPerWindow.GetComponent<ShopPer>().Setting();

            skillPerWindow.SetActive(true);
        }
    }

    public void OnClickCloseSkillPer()
    {
        if ((ButtonList.gachaButton & gachaButton.Skill) != 0)
        {
            ButtonList.gachaButton &= ~gachaButton.Skill;
            skillPerWindow.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&
            (ButtonList.mainButton & ButtonType.Shop) != 0 &&
            (ButtonList.gachaButton & gachaButton.Skill) != 0)
        {
            ButtonList.gachaButton &= ~gachaButton.Skill;
            skillPerWindow.SetActive(false);
        }
    }
}
