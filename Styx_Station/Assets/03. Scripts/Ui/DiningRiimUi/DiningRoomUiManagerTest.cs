using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiningRoomUiManagerTest : MonoBehaviour
{
    private bool isDrawDiningRoomDislay;
    public GameObject roomMainPanel;
    public DiningRoomUiFoodButton diningRoomUiButton;
    public DiningRoomUiInfoTest diningRoomUiInfo;
    public TextMeshProUGUI timerText;
    public float MinTimer = 1800f;
    public float MaxTimer;
    [HideInInspector]
    public float timer;
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

    public GameObject upgradePanel;
    private bool drawUpgradePanel;

    [HideInInspector]
    public int foodTimerUpgradeLevelUp = 0;
    public TextMeshProUGUI foodTimerUpgradeLevelUpTextGui;
    public Button foodTimerUpgradeLevelUpButton;
    private BigInteger foodTimerUpgradeLevelPrcie = new BigInteger(200);
    public int foodTimerUpgradeLevelPrcieWeight = 14;
    private int divideFoodTimerUpgradeLevelPrcieWeight = 10;

    [HideInInspector]
    public int foodSelectUpgradeLevelUp = 0;
    public TextMeshProUGUI foodSelectUpgradeLevelUpTextGui;
    public Button foodSelectUpgradeLevelUpUpButton;
    private BigInteger foodSelectUpgradeLevelUpPrcie = new BigInteger(1000);
    public int foodSelectUpgradeLevelUpPrcieWeight = 1000;

    private void Awake()
    {
        roomMainPanel.SetActive(isDrawDiningRoomDislay);
    }
    private void Start()
    {
        

        if (!isResetDiningTable)
        {
            isResetDiningTable = true;
            MaxTimer -= foodTimerUpgradeLevelUp * 30f;
            if (MaxTimer <= MinTimer)
            {
                MaxTimer = MinTimer;
                foodTimerUpgradeLevelUpTextGui.text = "Max Upgrade";
                foodTimerUpgradeLevelUpButton.interactable = false;
            }
            else
            {
                //testcode
                foodTimerUpgradeLevelUpTextGui.text = $"Now Timer Leve: :{foodTimerUpgradeLevelUp + 1}";
            }
            if (timer <= 0f)
                timer = MaxTimer;
            if(foodSelectUpgradeLevelUp ==4)
            {
                foodSelectUpgradeLevelUpTextGui.text = "Max Upgrade";
                foodSelectUpgradeLevelUpUpButton.interactable = false;
            }
            else
            {
                //testCode
                foodSelectUpgradeLevelUpTextGui.text = $"Now SelectCount :{foodSelectUpgradeLevelUp + 1}";
            }

            SettingGradeFood(); //내부적으로 코드 일부 지워야할 거 있음
        }

      
       

        if(diningRoomUiButton.isFullFood )
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(0);
            timerText.text = timeSpan.ToString(@"hh\:mm\:ss");
            timer = MaxTimer;
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
    private void SettingGradeFood()
    {
        if (fGradeFood.Count == 0 || eGradeFood.Count == 0 || dGradeFood.Count == 0 || cGradeFood.Count == 0 || bGradeFood.Count == 0
            || aGradeFood.Count == 0 || sGradeFood.Count == 0)
        {
            stringTable = new StringTable(); // 지워야함
            diningTable = new DiningTable();
            if (diningTable != null)
            {
                foodId = diningTable.GetFoodTableID();
            }
            GetFoodStringTableData();
            FoodGradeInsert();

        }
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

    public void DrawUpgradePanel()
    {
        drawUpgradePanel = !drawUpgradePanel;
        upgradePanel.SetActive(drawUpgradePanel);
    }    

    public void UpgradeTimer()
    {
        if (CurrencyManager.money1 > foodTimerUpgradeLevelPrcie)
        {
            CurrencyManager.money1 -= foodTimerUpgradeLevelPrcie;
            foodTimerUpgradeLevelPrcie *= foodTimerUpgradeLevelPrcieWeight / divideFoodTimerUpgradeLevelPrcieWeight;
        }
        else
            return;

        if (MaxTimer > MinTimer)
        {
            foodTimerUpgradeLevelUp++;
            foodTimerUpgradeLevelUpTextGui.text = $"Now Timer Leve: :{foodTimerUpgradeLevelUp + 1}";
            MaxTimer -= 30f;
        }    
        else
        {
            foodTimerUpgradeLevelUpButton.interactable = false;
            foodTimerUpgradeLevelUpTextGui.text = $"MaxUpgrade";
        }
    }
      
    public void UpgradeSelectFoddCount()
    {
        if (CurrencyManager.money2 > foodSelectUpgradeLevelUpPrcie)
        {
            CurrencyManager.money2 -= foodSelectUpgradeLevelUpPrcie;
            foodSelectUpgradeLevelUpPrcie += foodSelectUpgradeLevelUpPrcieWeight;
        }
        else
            return;
        
        var count = diningRoomUiButton.upgradeSelectFoodCount;
        if(count <6)
        {
                diningRoomUiButton.upgradeSelectFoodCount++;
                foodSelectUpgradeLevelUp++;
                foodSelectUpgradeLevelUpTextGui.text = $"Now SelectCount :{foodSelectUpgradeLevelUp + 1}";
                diningRoomUiButton.ResetFoodImage(count);
                if(foodSelectUpgradeLevelUp ==4)
                {
                    foodSelectUpgradeLevelUpTextGui.text = $"Max Upgrade SelectCount";
                    foodSelectUpgradeLevelUpUpButton.interactable = false;
                } 
        }
        else
        {
            foodSelectUpgradeLevelUpTextGui.text = $"Max Upgrade SelectCount";
            foodSelectUpgradeLevelUpUpButton.interactable = false;
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
        else if (range <= 80) selectedList = dGradeFood;
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
        var data = diningRoomUiButton.GetFoodData(currentButtonIndex);
        diningRoomUiInfo.Reset();
        string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
        string log = $"{currentTime} : 음식을 섭취했습니다.";
        Log.Instance.MakeLogText(log);
        currentButtonIndex = -1;
        // 상시 습득으로 변경 추후 한번만인지 ? 확인후 변경해야함 ...
        PlayerBuff.GetBuffAll(data.Food_ATK, data.Food_Cri, data.Food_Skill, data.Food_Boss, data.Food_Silup, data.Food_Du);
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
        //test code임

        CurrencyManager.GetSilver(5000, 0);
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
 
    public void LoadFood(SaveFoodData foodData)
    {
        SettingGradeFood();
        
        switch ((FoodType)foodData.Food_Type)
        {
            case FoodType.F:
                foreach (var f in fGradeFood)
                {
                    if(f.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        diningRoomUiButton.MakeFood(f);
                        return;
                    }
                }
                break;

            case FoodType.E:
                foreach (var e in eGradeFood)
                {
                    if (e.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        diningRoomUiButton.MakeFood(e);
                        return;
                    }
                }
                break;

            case FoodType.D:
                foreach (var d in dGradeFood)
                {
                    if (d.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        diningRoomUiButton.MakeFood(d);
                        return;
                    }
                }
                break;

            case FoodType.C:
                foreach (var c in cGradeFood)
                {
                    if (c.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        diningRoomUiButton.MakeFood(c);
                        return;
                    }
                }
                break;

            case FoodType.B:
                foreach (var b in bGradeFood)
                {
                    if (b.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        diningRoomUiButton.MakeFood(b);
                        return;
                    }
                }
                break;

            case FoodType.A:
                foreach (var a in aGradeFood)
                {
                    if (a.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        diningRoomUiButton.MakeFood(a);
                        return;
                    }
                }
                break;

            case FoodType.S:
                foreach (var s in sGradeFood)
                {
                    if (s.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        diningRoomUiButton.MakeFood(s);
                        return;
                    }
                }
                break;
        }
    }
    public void closeDiningRoom()
    {
        roomMainPanel.SetActive(false);
    }
}
