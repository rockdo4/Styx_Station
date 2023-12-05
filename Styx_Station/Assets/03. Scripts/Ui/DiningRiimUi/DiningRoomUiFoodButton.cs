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
    [HideInInspector]
    public FoodData[] foodDatas = new FoodData[6];
    private DiningRoomUiManager parentDiningRoomUi;
    private void Awake()
    {
        parentDiningRoomUi = GetComponentInParent<DiningRoomUiManager>();
    }

    private void Start()
    {
        CheckFoodButton();
        AddButtonMethod();
    }
    private void AddButtonMethod()
    {
        for (int i = 0; i < foodButton.Count; i++)
        {
            var num = i;
            foodButton[i].onClick.AddListener(() => InitFoodButtonOnClick(num));
        }
    }
    public FoodData GetFoodData(int index)
    {
        return foodDatas[index];
    }
    private void InitFoodButtonOnClick(int index)
    {
        parentDiningRoomUi.currentButtonIndex = index;  
        if (foodDatas[index] != null)
        {
            Debug.Log(foodDatas[index].sprite.name);
            parentDiningRoomUi.GetFoodDataByMadeFood(foodDatas[index]);
        }
    }
    private void CheckFoodButton()
    {
        for (int i = 0; i < foodButton.Count; i++)
        {
            if (i < upgradeSelectFoodCount)
            {
                
                var texture = foodButton[i].GetComponent<Image>();
                texture.sprite = cookImage;
                foodButton[i].interactable = false;
            }
            else
            {
                foodButton[i].interactable = false;
                var texture = foodButton[i].GetComponent<Image>();
                texture.sprite = lockImage;
            }
        }
    }
    public void ResetFoodImage(int index)
    {
        var texture = foodButton[index].GetComponent<Image>();
        texture.sprite = cookImage;
        foodButton[index].interactable=false;
        isFullFood = false;
    }
    public void MakeFood(FoodData data)
    {
        for(int i = 0; i < upgradeSelectFoodCount; i++)
        {
            var texture = foodButton[i].GetComponent<Image>();
            if (texture.sprite == cookImage)
            {
                foodButton[i].interactable = true;
                texture.sprite = data.sprite;
                foodDatas[i] = data;
                if(i == upgradeSelectFoodCount - 1)
                {
                    isFullFood = true;
                }
                return;
            }
        }
    }
}
