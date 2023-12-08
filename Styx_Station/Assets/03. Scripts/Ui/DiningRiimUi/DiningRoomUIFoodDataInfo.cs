using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiningRoomUIFoodDataInfo : MonoBehaviour
{
    private FoodData foodData;

    [SerializeField] private Image foodImage;
    [SerializeField] private Sprite defaultFoodImage;

    [SerializeField] private TextMeshProUGUI foodRank;


    public void SetFoodData(FoodData foodData)
    {
        this.foodData = foodData;
        foodImage.sprite = this.foodData.sprite;
        switch(this.foodData.Food_Type)
        {
            case FoodType.F:
                foodRank.text = "F";
                break;
        }
    }
}
