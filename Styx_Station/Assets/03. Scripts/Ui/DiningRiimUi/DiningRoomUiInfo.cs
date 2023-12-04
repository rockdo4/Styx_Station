using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiningRoomUiInfo : MonoBehaviour
{
    public List<Sprite> rankSprites = new List<Sprite>();
    public Image foodImage;
    public Image foodRankImage;
    public TextMeshProUGUI foodName;
    public TextMeshProUGUI foodBuffText;
    public TextMeshProUGUI foodguideText;
    public TextMeshProUGUI foodSellInfo;
    private Language prevLanguage;
    private StringTableData foodNameStringData;
    private StringTableData foodBuffStringData;
    private StringTableData foodGuideStringData;
    private StringTableData foodSellInfoStringData;
    private FoodData currentInfFoddData;
    private int[] buffInt = new int[5];

    private void Awake()
    {
        prevLanguage = Global.language;

    }
    private void Start()
    {
        for (int i = 0; i < buffInt.Length; i++)
        {
            buffInt[i] = 0;
        }
    }
    private void Update()
    {
        if (prevLanguage != Global.language)
        {
            prevLanguage = Global.language;
            ChangeLanguage();
        }
    }
    public void OnFoodDataDisplay(FoodData data, StringTableData stringData, StringTableData foodBuff,
        StringTableData foodGuide, StringTableData sellInfo)
    {
        currentInfFoddData = data;
        foodImage.sprite = currentInfFoddData.sprite;
        switch (data.Food_Type)
        {
            case FoodType.F:
                foreach (var rank in rankSprites)
                {
                    if (rank.name == "F" || rank.name == "f")
                    {
                        foodRankImage.sprite = rank;
                        break;
                    }
                }
                break;
            case FoodType.E:
                foreach (var rank in rankSprites)
                {
                    if (rank.name == "E" || rank.name == "e")
                    {
                        foodRankImage.sprite = rank;
                        break;
                    }
                }
                break;
            case FoodType.D:
                foreach (var rank in rankSprites)
                {
                    if (rank.name == "D" || rank.name == "d")
                    {
                        foodRankImage.sprite = rank;
                        break;
                    }
                }
                break;
            case FoodType.C:
                foreach (var rank in rankSprites)
                {
                    if (rank.name == "C" || rank.name == "c")
                    {
                        foodRankImage.sprite = rank;
                        break;
                    }
                }
                break;
            case FoodType.B:
                foreach (var rank in rankSprites)
                {
                    if (rank.name == "B" || rank.name == "B")
                    {
                        foodRankImage.sprite = rank;
                        break;
                    }
                }
                break;
            case FoodType.A:
                foreach (var rank in rankSprites)
                {
                    if (rank.name == "A" || rank.name == "a")
                    {
                        foodRankImage.sprite = foodImage.sprite;
                        break;
                    }
                }
                break;
            case FoodType.S:
                foreach (var rank in rankSprites)
                {
                    if (rank.name == "S" || rank.name == "s")
                    {
                        foodRankImage.sprite = rank;
                        break;
                    }
                }
                break;
        }

        foodNameStringData = stringData;
        foodBuffStringData = foodBuff;
        foodGuideStringData = foodGuide;
        foodSellInfoStringData = sellInfo;
        ChangeLanguage();
    }

    public void ChangeLanguage()
    {
        string foodBuffStr;
        int currentBuffInt = 0;
        if (currentInfFoddData.Food_ATK > 0)
        {
            buffInt[currentBuffInt] = currentInfFoddData.Food_ATK;
            currentBuffInt++;
        }
        if (currentInfFoddData.Food_Cri > 0)
        {
            buffInt[currentBuffInt] = currentInfFoddData.Food_Cri;
            currentBuffInt++;
        }
        if (currentInfFoddData.Food_Skill > 0)
        {
            buffInt[currentBuffInt] = currentInfFoddData.Food_Skill;
            currentBuffInt++;
        }
        if (currentInfFoddData.Food_Boss > 0)
        {
            buffInt[0] = currentInfFoddData.Food_Boss;
            currentBuffInt++;
        }
        if (currentInfFoddData.Food_Silup > 0)
        {
            buffInt[currentBuffInt] = currentInfFoddData.Food_Silup;
            currentBuffInt++;
        }
        switch (Global.language)
        {

            case Language.KOR:
                foodName.text = $"{foodNameStringData.KOR}";
                foodBuffStr = string.Format(foodBuffStringData.KOR, buffInt[0], buffInt[1], buffInt[2], buffInt[3], buffInt[4]);
                foodBuffText.text = foodBuffStr;
                foodguideText.text = foodGuideStringData.KOR;
                foodSellInfo.text = "현재 판매량은 구상중에 있습니다.";
                break;
            case Language.ENG:
                foodName.text = $"{foodNameStringData.ENG}";
                foodBuffStr = string.Format(foodBuffStringData.ENG, buffInt[0], buffInt[1], buffInt[2], buffInt[3], buffInt[4]);
                foodBuffText.text = foodBuffStr;
                foodguideText.text = foodGuideStringData.ENG;
                foodSellInfo.text = "We will Fixed sell info";
                break;
        }
    }
    public void Reset()
    {
        foodNameStringData.KOR = "";
        foodNameStringData.ENG = "";
        foodBuffStringData.ENG = "";
        foodBuffStringData.KOR ="";
        foodGuideStringData.ENG = "";
        foodGuideStringData.KOR = "";
        foodSellInfoStringData.KOR = "";
        foodSellInfoStringData.ENG = "";
        foodSellInfo.text = ""; //testcode
        foodImage.sprite = null;
        foodRankImage.sprite = null;
        ChangeLanguage();
        
    }
}
