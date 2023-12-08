using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiningRoomUIManager : MonoBehaviour
{ 
    private bool isResetDiningTable;
    private int prevSelectCount;

    public List<Sprite> foodSpriteList = new List<Sprite>();
    private List<string> foodId;
    private List<FoodData> fGradeFood = new List<FoodData>();
    private List<FoodData> eGradeFood = new List<FoodData>();
    private List<FoodData> dGradeFood = new List<FoodData>();
    private List<FoodData> cGradeFood = new List<FoodData>();
    private List<FoodData> bGradeFood = new List<FoodData>();
    private List<FoodData> aGradeFood = new List<FoodData>();
    private List<FoodData> sGradeFood = new List<FoodData>();

    [HideInInspector] public FoodData[] makeFoodData = new FoodData[6];

    public DiningRoomButtonData[] diningRoomButtdonDatas =new DiningRoomButtonData[6];
    public DiningRoomUIFoodDataInfo diningRoomUIFoodDataInfo;
    private void Awake()
    {
        makeFoodData = DiningRoomSystem.Instance.foodDatas;
    }
    private void Start()
    {
        prevSelectCount = DiningRoomSystem.Instance.selectFoodCount;
        DiningRoomButtonDataSetIsPossibleButton();
        if (!isResetDiningTable)
        {
            isResetDiningTable = true;
            SettingGradeFood();
        }
        MakeFood();
    }

    private void Update()
    {
        MakeFood();
        if(prevSelectCount !=  DiningRoomSystem.Instance.selectFoodCount)
        {
            DiningRoomButtonDataSetIsPossibleButton();
        }
    }
    private void SendFoodDataInfo()
    {
        for(int i=0;i < DiningRoomSystem.Instance.selectFoodCount;++i)
        {
            if(diningRoomButtdonDatas[i].onClick)
            {
                //함수 호출
            }
        }
    }
    private void DiningRoomButtonDataSetIsPossibleButton()
    {
        for (int i = 0; i < diningRoomButtdonDatas.Length; i++)
        {
            if (i < prevSelectCount)
            {
                diningRoomButtdonDatas[i].isPossibleButton = true;
            }
            else
                break;
        }
    }
    private void MakeFood()
    {
        if (DiningRoomSystem.Instance.counting > 0)
        {
            DiningRoomSystem.Instance.FoodDatasNullCheck();
            if (DiningRoomSystem.Instance.isFullFood)
            {
                DiningRoomSystem.Instance.counting =0;
                return;
            }
            int temp = 0;
            for (int i = 0; i < DiningRoomSystem.Instance.counting; ++i)
            {
                var food = ChooseFoodByProbability();
                DiningRoomSystem.Instance.SetFood(food);
                temp++;
            }
            DiningRoomSystem.Instance.counting -= temp;

        }
        makeFoodData = DiningRoomSystem.Instance.GetAllFoodData();
        for(int i=0;i< diningRoomButtdonDatas.Length;++i)
        {
            diningRoomButtdonDatas[i].SetFoodData(makeFoodData[i]);
        }
    }

    private void SettingGradeFood()
    {
        if (fGradeFood.Count == 0 || eGradeFood.Count == 0 || dGradeFood.Count == 0 || cGradeFood.Count == 0 || bGradeFood.Count == 0
            || aGradeFood.Count == 0 || sGradeFood.Count == 0)
        { 
            if (MakeTableData.Instance.diningRoomTable != null)
            {
                foodId = MakeTableData.Instance.diningRoomTable.GetFoodTableID();
            }
            else
            {
                MakeTableData.Instance.diningRoomTable = new DiningTable();
                foodId = MakeTableData.Instance.diningRoomTable.GetFoodTableID();
            }
            FoodGradeInsert();
        }
    }
    private void FoodGradeInsert()
    {
        for (int i = 0; i < MakeTableData.Instance.diningRoomTable.dic.Count; i++)
        {
            for (int j = 0; j < foodSpriteList.Count; j++)
            {
                if (MakeTableData.Instance.diningRoomTable.dic[foodId[i]].Food_Name_ID == foodSpriteList[i].name)
                {
                    var t = MakeTableData.Instance.diningRoomTable.dic[foodId[i]];
                    FoodData type = new FoodData();
                    type.sprite = foodSpriteList[i];
                    type.Food_ID = t.Food_ID;
                    type.Food_Name_ID = t.Food_Name_ID;
                    type.Food_Per = t.Food_Per;
                    type.Food_Sil = t.Food_Sil;
                    type.Food_Soul = t.Food_Soul;
                    type.Food_ATK = t.Food_ATK;
                    type.Food_Cri = t.Food_Cri;
                    type.Food_Skill = t.Food_Skill;
                    type.Food_Boss = t.Food_Boss;
                    type.Food_Silup = t.Food_Silup;
                    type.Food_Du = t.Food_Du;
                    type.Food_Type = (FoodType)t.Food_Type;
                    switch (MakeTableData.Instance.diningRoomTable.dic[foodId[i]].Food_Type)
                    {
                        case 0:
                            fGradeFood.Add(type);
                            break;
                        case 1:
                            eGradeFood.Add(type);
                            break;
                        case 2:
                            dGradeFood.Add(type);
                            break;
                        case 3:
                            cGradeFood.Add(type);
                            break;
                        case 4:
                            bGradeFood.Add(type);
                            break;
                        case 5:
                            aGradeFood.Add(type);
                            break;
                        case 6:
                            sGradeFood.Add(type);
                            break;
                    }
                    break;
                }
            }
        }
    }
    private FoodData ChooseFoodByProbability()
    {
        var range = UnityEngine.Random.Range(0, 100);
        List<FoodData> selectedList;
        if (range <= 40) selectedList = fGradeFood;
        else if (range <= 65) selectedList = eGradeFood;
        else if (range <= 80) selectedList = dGradeFood;
        else if (range <= 90) selectedList = cGradeFood;
        else if (range <= 96) selectedList = bGradeFood;
        else if (range <= 99) selectedList = aGradeFood;
        else selectedList = sGradeFood;
        return MakeRandomFoodFromList(selectedList);
    }
    private FoodData MakeRandomFoodFromList(List<FoodData> foodList)
    {
        int max = foodList.Sum(food => food.Food_Per);
        var result = UnityEngine.Random.Range(0, max);
        int betwon = 0;

        foreach (var food in foodList)
        {
            betwon += food.Food_Per;
            if (result <= betwon)
            {
                return food;
            }
        }
        return null;
    }

    public void LoadFood(SaveFoodData foodData)
    {
        SettingGradeFood();
    }
}
