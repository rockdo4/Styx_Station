using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiningRoomUiFoodButton : MonoBehaviour
{
    public List<Button> foodButton;
    public int upgradeSelectFoodCount = 2;
    public Sprite lockImage;
    public bool isFullFood; // 버튼을 눌러서 판매 또는 섭취하면 false 로 만들기



    public Sprite cookImage;
    public Sprite testImage;
    private void Awake()
    {

    }

    private void Start()
    {
        CheckFoodButton();
    }

    private void CheckFoodButton()
    {
        for (int i = 0; i < foodButton.Count; i++)
        {
            if (i < upgradeSelectFoodCount)
            {
                foodButton[i].interactable = true;
                var texture = foodButton[i].GetComponent<Image>();
                texture.sprite = cookImage;
            }
            else
            {
                foodButton[i].interactable = false;
                var texture = foodButton[i].GetComponent<Image>();
                texture.sprite = lockImage;
            }
        }
    }
    public void MakeFood(FoodData data)
    {
        for(int i = 0; i < upgradeSelectFoodCount; i++)
        {
            var texture = foodButton[i].GetComponent<Image>();
            if (texture.sprite == cookImage)
            {

                texture.sprite = data.sprite;

                if(i == upgradeSelectFoodCount-1)
                {
                    isFullFood = true;
                }
                break;
            }
        }
        
    }
}
