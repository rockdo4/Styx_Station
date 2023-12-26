using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PetGacha : MonoBehaviour
{
    private ShopSystem shop;
    private GachaInfo window;
    private StateSystem stateSystem;

    public Slider nextLevExp;

    public TextMeshProUGUI gachaName;

    public TextMeshProUGUI currentLev;
    public TextMeshProUGUI currentExp;

    public List<Button> gachaButtons = new List<Button>();

    public Button petPer;

    public GameObject petPerWindow;

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


        currentLev.text = $"Lv.{shop.currentPetRank}";
        currentExp.text = $"{shop.currentPetRankUp} / {shop.petTable.drops[shop.currentPetRank].RankUp}";

        nextLevExp.value = (float)shop.currentPetRankUp / (float)shop.petTable.drops[shop.currentPetRank].RankUp;
    }

    public void OnClickMinGacha()
    {
        if (CurrencyManager.money3 < minValue)
            return;

        CurrencyManager.money3 -= minValue;
        shop.PetGacha(window, minGacha);

        window.InfoUpdate(0, 2);
        window.gameObject.SetActive(true);

        GachaUpdate();
    }

    public void OnClickMiddleGacha()
    {
        if (CurrencyManager.money3 < middleValue)
            return;

        CurrencyManager.money3 -= middleValue;
        shop.PetGacha(window, middleGach);

        window.InfoUpdate(1, 2);
        window.gameObject.SetActive(true);

        GachaUpdate();
    }

    public void OnClickMaxGacha()
    {
        if (CurrencyManager.money3 < maxValue)
            return;

        CurrencyManager.money3 -= maxValue;
        shop.PetGacha(window, maxGacha);

        window.InfoUpdate(2, 2);
        window.gameObject.SetActive(true);

        GachaUpdate();
    }
    public void OnClickPetPer()
    {
        petPerWindow.GetComponent<PetPer>().Setting();

        petPerWindow.SetActive(true);
    }

    public void OnClickClosePetPer()
    {
        petPerWindow.SetActive(false);
    }
}
