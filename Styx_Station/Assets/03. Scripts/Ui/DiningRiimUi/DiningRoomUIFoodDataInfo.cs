using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiningRoomUIFoodDataInfo : MonoBehaviour
{
    private Language language;
    private StringTableData defaultFoodDataInfo = new StringTableData();
    
    [HideInInspector] public FoodData foodData;
    private bool isSetting = false;
    [SerializeField] private Image foodImage;
    [SerializeField] private Sprite defaultFoodImage;

    [SerializeField] private TextMeshProUGUI foodRankText;
    [SerializeField] private TextMeshProUGUI foodNameText;
    private StringTableData foodNameStringTable;
    [SerializeField] private TextMeshProUGUI foodSellSliverText;
    [SerializeField] private TextMeshProUGUI foodSellPomegranateText;
    [SerializeField] private TextMeshProUGUI foodInfoText;
    private StringTableData foodInfoStringTable;
    public int currentIndex;

    public Button sellButton;
    public Button eatButton;
    private void Start()
    {
        language = Global.language;
        DataZero();

        if( foodData == null )
        {
            sellButton.interactable = false;
            eatButton.interactable = false;
        }
    }

    private void Update()
    {
        if(isSetting && language !=Global.language)
        {
            SetStringTableData();
        }
    }


    public void SetFoodData(FoodData foodData,int index)
    {
        if(PlayerBuff.Instance.buffData.foodType > foodData.Food_Type)
        {
            eatButton.gameObject.SetActive(false);
        }
        else
        {
            eatButton.gameObject.SetActive(true);
        }
        currentIndex=index;
        this.foodData = foodData;
        foodImage.sprite = this.foodData.sprite;
        isSetting=true; 
        switch (this.foodData.Food_Type)
        {
            case FoodType.F:
                foodRankText.text = "F";
                break;
            case FoodType.E:
                foodRankText.text = "E";
                break;
            case FoodType.D:
                foodRankText.text = "D";
                break;
            case FoodType.C:
                foodRankText.text = "C";
                break;
            case FoodType.B:
                foodRankText.text = "B";
                break;
            case FoodType.A:
                foodRankText.text = "A";
                break;
            case FoodType.S:
                foodRankText.text = "S";
                break;
        }
        foodNameStringTable = MakeTableData.Instance.stringTable.dic[foodData.Food_Name_ID];
        var str = foodData.Food_ID + "_FoodInfo";
        foodInfoStringTable = MakeTableData.Instance.stringTable.dic[str];
        foodSellSliverText.text = $"{foodData.Food_Sil}";
        foodSellPomegranateText.text = $"{foodData.Food_Soul}";
        SetStringTableData();
        sellButton.interactable = true;
        eatButton.interactable = true;
    }

    private void SetStringTableData()
    {
        language = Global.language;
        switch(Global.language)
        {
            case Language.KOR:
                foodNameText.text = foodNameStringTable.KOR;
                foodInfoText.text=foodInfoStringTable.KOR;
                break;
            case Language.ENG:
                foodNameText.text = foodNameStringTable.ENG;
                foodInfoText.text =foodInfoStringTable.ENG;
                break;
        }
    }
    public void DataZero()
    {
        foodData = null;
        foodImage.sprite = defaultFoodImage;
        foodRankText.text = "";
        foodNameText.text = "";
        foodNameStringTable = defaultFoodDataInfo;
        foodSellSliverText.text = "";
        foodSellPomegranateText.text = "";
        foodInfoText.text = "";
        foodInfoStringTable = defaultFoodDataInfo;

        sellButton.interactable = false;
        eatButton.interactable = false;
    }
}
