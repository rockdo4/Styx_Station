using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemGacha : MonoBehaviour
{
    private ShopSystem shop;
    private GachaInfo window;
    private StateSystem stateSystem;

    public Slider nextLevExp;

    public TextMeshProUGUI gachaName;

    public TextMeshProUGUI currentLev;
    public TextMeshProUGUI currentExp;

    public List<Button> gachaButtons = new List<Button>();

    public Button itemPer;

    public GameObject itemPerWindow;

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
        if(!first)
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


        currentLev.text = $"Lv.{shop.currentItemRank}";
        currentExp.text = $"{shop.currentItemRankUp} / {shop.itemTable.drops[shop.currentItemRank].RankUp}";

        nextLevExp.value = (float)shop.currentItemRankUp / (float)shop.itemTable.drops[shop.currentItemRank].RankUp;
    }

    public void OnClickMinGacha()
    {
        if (CurrencyManager.money3 < minValue)
            return;

        CurrencyManager.money3 -= minValue;
        shop.ItemGacha(window, minGacha);

        stateSystem.EquipUpdate();
        stateSystem.AcquireUpdate();
        stateSystem.TotalUpdate();

        window.InfoUpdate(0,0);
        window.gameObject.SetActive(true);

        GachaUpdate();
    }

    public void OnClickMiddleGacha()
    {
        if (CurrencyManager.money3 < middleValue)
            return;

        CurrencyManager.money3 -= middleValue;
        shop.ItemGacha(window, middleGach);

        stateSystem.EquipUpdate();
        stateSystem.AcquireUpdate();
        stateSystem.TotalUpdate();

        window.InfoUpdate(1, 0);
        window.gameObject.SetActive(true);

        GachaUpdate();
    }

    public void OnClickMaxGacha()
    {
        if (CurrencyManager.money3 < maxValue)
            return;

        CurrencyManager.money3 -= maxValue;
        shop.ItemGacha(window, maxGacha);

        stateSystem.EquipUpdate();
        stateSystem.AcquireUpdate();
        stateSystem.TotalUpdate();

        window.InfoUpdate(2, 0);
        window.gameObject.SetActive(true);

        GachaUpdate();
    }
}
