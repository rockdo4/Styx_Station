using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillGacha : MonoBehaviour
{
    private ShopSystem shop;
    private GachaInfo window;
    private StateSystem stateSystem;
    
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

            gachaButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{minGacha}\nGacha";
            gachaButtons[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{middleGach}\nGacha";
            gachaButtons[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{maxGacha}\nGacha";

            minValueText.text = $"{minValue}";
            middleValueText.text = $"{middleValue}";
            maxValueText.text = $"{maxValue}";

            first = true;
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

        GachaUpdate();
    }

    public void OnClickSkillPer()
    {
        skillPerWindow.GetComponent<ShopPer>().Setting();

        skillPerWindow.SetActive(true);
    }

    public void OnClickCloseSkillPer()
    {
        skillPerWindow.SetActive(false);
    }
}
