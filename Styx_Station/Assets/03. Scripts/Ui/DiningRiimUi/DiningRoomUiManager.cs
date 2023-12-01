using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiningRoomUiManager : MonoBehaviour
{
    private bool isDrawDiningRoomDislay;
    public GameObject roomMainPanel;
    public DiningRoomUiFoodButton diningRoomUiButton;
    public DiningRoomUiInfo diningRoomUiInfo;
    public TextMeshProUGUI timerText;
    public float MaxTimer;
    private float timer;
    public List<Sprite> foodSpriteList = new List<Sprite>();
    private bool isResetDiningTable;
    DiningTable diningTable;
    private List<string> foodId;

    private List<FoodData> fGradeFood = new List<FoodData>();
    private List<FoodData> eGradeFood = new List<FoodData>();
    private List<FoodData> dGradeFood = new List<FoodData>();
    private List<FoodData> cGradeFood = new List<FoodData>();
    private List<FoodData> bGradeFood = new List<FoodData>();
    private List<FoodData> aGradeFood = new List<FoodData>();
    private List<FoodData> sGradeFood = new List<FoodData>();

    private StringTable stringTable;
    private Dictionary<String, StringTableData> foodNameDictionary = new Dictionary<String, StringTableData>();
    private Dictionary<String, StringTableData> foodExplanationDictionary = new Dictionary<String, StringTableData>();
    private Dictionary<String, StringTableData> foodGuideDictionary = new Dictionary<String, StringTableData>();
    private Dictionary<String, StringTableData> foodSellInfoDictionary = new Dictionary<String, StringTableData>();

    [HideInInspector]
    public int currentButtonIndex =-1;
    private void Awake()
    {
        roomMainPanel.SetActive(isDrawDiningRoomDislay);
    }
    private void Start()
    {
        timer = MaxTimer;
        if (!isResetDiningTable)
        {
            stringTable = new StringTable(); // 지워야함
            diningTable = new DiningTable();
            isResetDiningTable = true;
            if (diningTable != null)
            {
                foodId = diningTable.GetFoodTableID();
            }
            GetFoodStringTableData();
            FoodGradeInsert();
        }
    }
    private void Update()
    {
        if (!diningRoomUiButton.isFullFood)
            TimeCounting();
    }

    public void OnClickDrawDiningRoomDisplay()
    {
        isDrawDiningRoomDislay = !isDrawDiningRoomDislay;
        roomMainPanel.SetActive(isDrawDiningRoomDislay);
    }

    public void GetFoodDataByMadeFood(FoodData data)
    {
        var stringData = foodNameDictionary[data.Food_Name_ID];
        var buffExplanation = GetFoodExplanationStringTable(data.Food_ID);
        var guideData =GetFoodGuideStringTable(data.Food_ID);
        var selling = GetFoodGuideStringTable(data.Food_ID); // change foodSellInfoString
        diningRoomUiInfo.OnFoodDataDisplay(data, stringData, buffExplanation, guideData, selling);
    }
    private void GetFoodStringTableData()
    {
        var foodNameList = diningTable.GetFoodTableID();
        for (int i = 0; i < foodNameList.Count; i++)
        {
            var id = diningTable.GetFoodTableData(foodNameList[i]);
            var data = stringTable.GetStringTableData(id.Food_Name_ID);
            foodNameDictionary.Add(id.Food_Name_ID, data);

            var buffId = foodNameList[i] + "_Buff";
            var buffData = stringTable.GetStringTableData(buffId);
            foodExplanationDictionary.Add(buffId, buffData);

            var guideId = foodNameList[i] + "_FoodInfo";
            var guideData = stringTable.GetStringTableData(guideId);
            foodGuideDictionary.Add(guideData.ID, guideData);
        }
    }
    private void FoodGradeInsert()
    {
        for (int i = 0; i < diningTable.dic.Count; i++)
        {
            for (int j = 0; j < foodSpriteList.Count; j++)
            {
                if (diningTable.dic[foodId[i]].Food_Name_ID == foodSpriteList[i].name)
                {
                    var t = diningTable.dic[foodId[i]];
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
                    switch (diningTable.dic[foodId[i]].Food_Type)
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

    private void TimeCounting()
    {
        timer -= Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);

        timerText.text = timeSpan.ToString(@"hh\:mm\:ss");

        if (timer <= 0)
        {
            ChooseFoodByProbability();
            timer = MaxTimer;
        }
    }

    private void ChooseFoodByProbability()
    {
        var range = UnityEngine.Random.Range(0, 100);
        List<FoodData> selectedList;
        if (range <= 40) selectedList = fGradeFood;
        else if (range <= 65) selectedList = eGradeFood;
        else if (range <= 80) selectedList = fGradeFood;
        else if (range <= 90) selectedList = cGradeFood;
        else if (range <= 96) selectedList = bGradeFood;
        else if (range <= 99) selectedList = aGradeFood;
        else selectedList = sGradeFood;
        MakeRandomFoodFromList(selectedList);
    }
    private void MakeRandomFoodFromList(List<FoodData> foodList)
    {
        int max = foodList.Sum(food => food.Food_Per);
        var result = UnityEngine.Random.Range(0, max);
        int betwon = 0;

        foreach (var food in foodList)
        {
            betwon += food.Food_Per;
            if (result <= betwon)
            {

                diningRoomUiButton.MakeFood(food);
                break;
            }
        }
    }
    public void EatFoodButton()
    {
        if (currentButtonIndex == -1)
            return;
        diningRoomUiButton.ResetFoodImage(currentButtonIndex);
        diningRoomUiInfo.Reset();
        string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
        string log = $"{currentTime} : 음식을 섭취했습니다.";
        Log.Instance.MakeLogText(log);
        currentButtonIndex = -1;
        // 버프로 가는 코드 작성
    }
    public void SellFood()
    {
        if (currentButtonIndex == -1)
            return;
        diningRoomUiButton.ResetFoodImage(currentButtonIndex);
        diningRoomUiInfo.Reset();
        string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
        string log = $"{currentTime} : 음식을 판매했습니다.";
        Log.Instance.MakeLogText(log);
        currentButtonIndex = -1;
        //은화 코드 작성
    }
    public StringTableData GetFoodExplanationStringTable(string str)
    {
        foreach (var exp in foodExplanationDictionary)
        {
            if (exp.Key.Contains(str))
            {
                return exp.Value;
            }
        }
        return default;
    }
    public StringTableData GetFoodGuideStringTable(string str)
    {
        foreach (var exp in foodGuideDictionary)
        {
            if (exp.Key.Contains(str))
            {
                return exp.Value;
            }
        }
        return default;
    }
    public StringTableData GetSellInfoStringTable(string str)
    {
        foreach (var exp in foodSellInfoDictionary)
        {
            if (exp.Key.Contains(str))
            {
                return exp.Value;
            }
        }
        return default;
    }

    public void closeDiningRoom()
    {
        roomMainPanel.SetActive(false);
    }
}
