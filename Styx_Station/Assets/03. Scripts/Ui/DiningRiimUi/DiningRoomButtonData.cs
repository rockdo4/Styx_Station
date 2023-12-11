using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiningRoomButtonData : MonoBehaviour
{
    public FoodData foodData;
    [HideInInspector] public bool isPossibleButton;
    public Button button;
    public Image imageObject;
    public Image lockImageObject;
    public Sprite lockSprite;
    public Sprite backGround;
    [HideInInspector] public bool onClick;
    

    private void Awake()
    {
        SetDiningRoomData();
        button.onClick.AddListener(()=> onClick = !onClick);
    }
    public void SetFoodData(FoodData foodData)
    {
        this.foodData = foodData;
        SetDiningRoomData();
    }
    public void SetDiningRoomData()
    {
        if (isPossibleButton)
        {
            lockImageObject.gameObject.SetActive(false);
            if (foodData == null)
            {
                imageObject.sprite = backGround;
                button.interactable = false;
            }
            else
            {
                imageObject.sprite = foodData.sprite;
                button.interactable = true;
            }
        }
        else
        {
            lockImageObject.sprite = lockSprite;
            button.interactable = false;
        }
    }
}
