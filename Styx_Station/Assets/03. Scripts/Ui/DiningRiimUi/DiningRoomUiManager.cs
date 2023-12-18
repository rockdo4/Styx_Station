using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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


    public TextMeshProUGUI makeFoodTimerText;
    [SerializeField] private string timeRemainingStringTableKey;
    [HideInInspector] private StringTableData timeRemainingStrinTableData;

    private int timerUpgradeLevel;
    public TextMeshProUGUI timerUpgradeText;
    public TextMeshProUGUI timerText;
    private int timerUpgradePrice=200;
    private int timerUpgradeAmount=200;
    public TextMeshProUGUI upgradePriceText;
    public Button timerUpgradeButton;


    private int selectFoodUpgradeLevel;
    public TextMeshProUGUI selectFoodUpgradeText;
    public TextMeshProUGUI selectFoodText;
    private int selectFoodUpgradePrice = 1000;
    private int selectFoodUpgradeAmount = 1000;
    public TextMeshProUGUI selectFoodUpgradePriceText;
    public Button selectFoodUpgradeButton;
    private void Awake()
    {
        makeFoodData = DiningRoomSystem.Instance.foodDatas;
    }
    private void Start()
    {
        
        prevSelectCount = DiningRoomSystem.Instance.selectFoodCount;

        TimerUpgradeTextSetting();
        SelectUpgradeTextSetting();
        DiningRoomButtonDataSetIsPossibleButton();
        CheckUpgradeButton();
        if (!isResetDiningTable)
        {
            isResetDiningTable = true;
            SettingGradeFood();
            if (MakeTableData.Instance.stringTable != null)
                timeRemainingStrinTableData = MakeTableData.Instance.stringTable.dic[timeRemainingStringTableKey];
            else
            { 
                MakeTableData.Instance.stringTable = new StringTable();
                timeRemainingStrinTableData = MakeTableData.Instance.stringTable.dic[timeRemainingStringTableKey];
            }
        }
        if (!DiningRoomSystem.Instance.isAwkeTime)
        {
            var savefood = DiningRoomSystem.Instance.saveFood;
            for (int i = 0; i < savefood.Length; i++)
            {
                if (savefood[i] != null)
                {
                    LoadFood(savefood[i],i);
                }
            }
            DiningRoomSystem.Instance.isAwkeTime = true;
        }
        makeFoodData = DiningRoomSystem.Instance.GetAllFoodData();
        MakeFood();
    }
    private void FixedUpdate()
    {
        SetDiningRoomTimerText();
        if(!DiningRoomSystem.Instance.isMaxSelectUpgradeLevel || !DiningRoomSystem.Instance.isMaxTimerUpgradeLevel)
            CheckUpgradeButton();

        //if(CurrencyManager.money1 < timerUpgradePrice || DiningRoomSystem.Instance.isMaxTimerUpgradeLevel)
        //{
        //    timerUpgradeButton.interactable = false;
        //}
        //else
        //{
        //    timerUpgradeButton.interactable = true;
        //}    
    }
    private void Update()
    {
        MakeFood();
        if (prevSelectCount !=  DiningRoomSystem.Instance.selectFoodCount)
        {
            prevSelectCount = DiningRoomSystem.Instance.selectFoodCount;
            DiningRoomButtonDataSetIsPossibleButton();
        }
        SendFoodDataInfo();
    }

    private void DiningRoomButtonDataSetIsPossibleButton()
    {
        for (int i = 0; i < diningRoomButtdonDatas.Length; i++)
        {
            if (i < prevSelectCount)
            {
                diningRoomButtdonDatas[i].isPossibleButton = true;
                diningRoomButtdonDatas[i].SetDiningRoomData();
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
                DiningRoomSystem.Instance.counting = 0;
                return;
            }
            int temp = 0;
            for (int i = 0; i < DiningRoomSystem.Instance.counting; ++i)
            {
                var food =ChooseFoodByProbability();
                DiningRoomSystem.Instance.SetFood(food);
                temp++;
            }
            DiningRoomSystem.Instance.counting -= temp;

        }
        makeFoodData = DiningRoomSystem.Instance.GetAllFoodData();
        for (int i = 0; i < diningRoomButtdonDatas.Length; ++i)
        {
            diningRoomButtdonDatas[i].SetFoodData(makeFoodData[i]);
        }
    }

    private void SendFoodDataInfo()
    {
        for(int i=0;i < DiningRoomSystem.Instance.selectFoodCount;++i)
        {
            if(diningRoomButtdonDatas[i].onClick)
            {
                diningRoomUIFoodDataInfo.SetFoodData(diningRoomButtdonDatas[i].foodData,i);
                diningRoomButtdonDatas[i].onClick = false;
            }
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

    public void EatFood()
    {
        if(diningRoomUIFoodDataInfo.foodData != null)
        {
            for (int i = 0; i < DiningRoomSystem.Instance.selectFoodCount; ++i)
            {
                if (diningRoomButtdonDatas[i].foodData == diningRoomUIFoodDataInfo.foodData && i== diningRoomUIFoodDataInfo.currentIndex)
                {
                    DiningRoomSystem.Instance.ReMoveFoodData(i);
                    diningRoomUIFoodDataInfo.DataZero();
                    PlayerBuff.Instance.GetBuffAll(diningRoomButtdonDatas[i].foodData.Food_ATK, diningRoomButtdonDatas[i].foodData.Food_Cri, diningRoomButtdonDatas[i].foodData.Food_Skill,
                        diningRoomButtdonDatas[i].foodData.Food_Boss, diningRoomButtdonDatas[i].foodData.Food_Silup, diningRoomButtdonDatas[i].foodData.Food_Du, diningRoomButtdonDatas[i].foodData.Food_Type);
                    break;
                }
            }
            makeFoodData = DiningRoomSystem.Instance.GetAllFoodData();
        }
    }
    public void SellFood()
    {
        if (diningRoomUIFoodDataInfo.foodData != null)
        {
            for (int i = 0; i < DiningRoomSystem.Instance.selectFoodCount; ++i)
            {
                if (diningRoomButtdonDatas[i].foodData == diningRoomUIFoodDataInfo.foodData && i == diningRoomUIFoodDataInfo.currentIndex)
                {
                    DiningRoomSystem.Instance.ReMoveFoodData(i);
                    diningRoomUIFoodDataInfo.DataZero();
                    CurrencyManager.GetSilver(diningRoomButtdonDatas[i].foodData.Food_Sil, 0);
                    CurrencyManager.GetSilver(diningRoomButtdonDatas[i].foodData.Food_Soul, 2);
                    break;
                }
            }
            makeFoodData = DiningRoomSystem.Instance.GetAllFoodData();
        }
    }
    private void SelectUpgradeTextSetting()
    {
        if(selectFoodUpgradeLevel != DiningRoomSystem.Instance.selectFoodCount || !DiningRoomSystem.Instance.isAwkeTime)
        {

            selectFoodUpgradeLevel = DiningRoomSystem.Instance.selectFoodCount;
            if (!DiningRoomSystem.Instance.isAwkeTime)
            {
                for (int i = 1; i < selectFoodUpgradeLevel; ++i)
                {
                    selectFoodUpgradePrice += selectFoodUpgradeAmount;
                }
            }
            else
            {
                selectFoodUpgradePrice += selectFoodUpgradeAmount;
            }

            if (selectFoodUpgradeLevel == DiningRoomSystem.Instance.maxSelectfoodCount-1)
            {
                selectFoodUpgradeText.text = $"LV.{selectFoodUpgradeLevel} > Max";
                selectFoodText.text = $"LV.{selectFoodUpgradeLevel} > Max";
                selectFoodUpgradePriceText.text = $"{UnitConverter.OutString(selectFoodUpgradePrice)}";
            }
            else if (selectFoodUpgradeLevel >= DiningRoomSystem.Instance.maxSelectfoodCount)
            {
                selectFoodUpgradeText.text = $"Max";
                selectFoodText.text = $"Max";
                selectFoodUpgradePriceText.text = "Max";
                selectFoodUpgradeButton.interactable = false;
                DiningRoomSystem.Instance.isMaxSelectUpgradeLevel = true;
            }
            else
            {
                selectFoodUpgradeText.text = $"LV.{selectFoodUpgradeLevel} > LV.{selectFoodUpgradeLevel + 1}";
                selectFoodText.text = $"LV.{selectFoodUpgradeLevel} > LV.{selectFoodUpgradeLevel + 1}";
                selectFoodUpgradePriceText.text = $"{UnitConverter.OutString(selectFoodUpgradePrice)}";
            }
            
           
        }
    }
    private void TimerUpgradeTextSetting()
    {
        if ( timerUpgradeLevel != DiningRoomSystem.Instance.timerUpgradeLevel || !DiningRoomSystem.Instance.isAwkeTime)
        {
            timerUpgradeLevel = DiningRoomSystem.Instance.timerUpgradeLevel;
            if (timerUpgradeLevel < DiningRoomSystem.Instance.maxTimerUpgradeLevel - 2)
                timerUpgradeText.text = $"LV.{timerUpgradeLevel + 1} > LV.{timerUpgradeLevel + 2}";
            else if (timerUpgradeLevel == DiningRoomSystem.Instance.maxTimerUpgradeLevel - 1)
            {
                timerUpgradeText.text = $"LV.{timerUpgradeLevel + 1} > Max";
            }
            else
            {
                timerUpgradeText.text = $"Max";
            }

            if (!DiningRoomSystem.Instance.isAwkeTime)
            {
                for (int i = 0; i <= timerUpgradeLevel; ++i)
                {
                    timerUpgradePrice += (timerUpgradeAmount * (timerUpgradeLevel * 14)) / 10;
                }
            }
            else
            {
                timerUpgradePrice += (timerUpgradeAmount * (timerUpgradeLevel * 14)) / 10;
            }

            if (timerUpgradeLevel < DiningRoomSystem.Instance.maxTimerUpgradeLevel)
            {
                TimeSpan timeSpanMax = TimeSpan.FromSeconds(DiningRoomSystem.Instance.max);
                var timerStr = timeSpanMax.ToString(@"hh\:mm\:ss");
                TimeSpan timeSpanDecrease = TimeSpan.FromSeconds(DiningRoomSystem.Instance.max - DiningRoomSystem.Instance.decreaseMaxTimer);
                var timerStr2 = timeSpanDecrease.ToString(@"hh\:mm\:ss");
                timerText.text = $"{timerStr} > {timerStr2}";
                upgradePriceText.text = $"{UnitConverter.OutString(timerUpgradePrice)}";
            }
            else
            {
                int t = (int)(DiningRoomSystem.Instance.max / 60);
                timerText.text = $"{t}:00";
                upgradePriceText.text = "Max";
                timerUpgradeButton.interactable = false;    
            }
        }
        
    }
    private void SetDiningRoomTimerText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(DiningRoomSystem.Instance.timer);

        var timer = timeSpan.ToString(@"hh\:mm\:ss");
        int makeCount = 0;
        var selecCount = DiningRoomSystem.Instance.selectFoodCount;
        for (int i = 0; i < selecCount; ++i)
        {
            if (makeFoodData[i] != null)
            {
                makeCount++;
            }
        }
        if (DiningRoomSystem.Instance.isFullFood)
        {
            string str = string.Empty;
            switch (Global.language)
            {
                case Language.KOR:
                    str = timeRemainingStrinTableData.KOR;
                    break;
                case Language.ENG:
                    str = timeRemainingStrinTableData.ENG;
                    break;
            }
            makeFoodTimerText.text = $"{str} : 00:00:00 \t{makeCount}/{selecCount}";
        }
        else
        {
            string str = string.Empty;
            switch (Global.language)
            {
                case Language.KOR:
                    str = timeRemainingStrinTableData.KOR;
                    break;
                case Language.ENG:
                    str = timeRemainingStrinTableData.ENG;
                    break;
            }
            makeFoodTimerText.text = $"{str} : {timer} \t{makeCount}/{selecCount}";
        }
    }

    public void LoadFood(SaveFoodData foodData,int index)
    {
        SettingGradeFood();

        switch ((FoodType)foodData.Food_Type)
        {
            case FoodType.F:
                foreach (var f in fGradeFood)
                {
                    if (f.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        DiningRoomSystem.Instance.SetFood(f,index);
                        return;
                    }
                }
                break;

            case FoodType.E:
                foreach (var e in eGradeFood)
                {
                    if (e.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        DiningRoomSystem.Instance.SetFood(e,index);
                        return;
                    }
                }
                break;

            case FoodType.D:
                foreach (var d in dGradeFood)
                {
                    if (d.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        DiningRoomSystem.Instance.SetFood(d, index);
                        return;
                    }
                }
                break;

            case FoodType.C:
                foreach (var c in cGradeFood)
                {
                    if (c.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        DiningRoomSystem.Instance.SetFood(c, index);
                        return;
                    }
                }
                break;

            case FoodType.B:
                foreach (var b in bGradeFood)
                {
                    if (b.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        DiningRoomSystem.Instance.SetFood(b , index);
                        return;
                    }
                }
                break;

            case FoodType.A:
                foreach (var a in aGradeFood)
                {
                    if (a.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        DiningRoomSystem.Instance.SetFood(a , index);
                        return;
                    }
                }
                break;

            case FoodType.S:
                foreach (var s in sGradeFood)
                {
                    if (s.Food_Name_ID == foodData.Food_Name_ID)
                    {
                        DiningRoomSystem.Instance.SetFood(s, index);
                        return;
                    }
                }
                break;
        }
    }
    public void TimerUpgradeButton()
    {

        if (CurrencyManager.money1>=timerUpgradePrice)
        {
            CurrencyManager.money1 -= timerUpgradePrice;
        }
        else
        {
            return;
        }
        DiningRoomSystem.Instance.UpgradeTimerLevel();

        if (DiningRoomSystem.Instance.isMaxTimerUpgradeLevel)
        {
            timerUpgradeButton.interactable = false;
        }
        DiningRoomSystem.Instance.max -= DiningRoomSystem.Instance.decreaseMaxTimer;
        TimerUpgradeTextSetting();
    }
    public void SelectFoodUpgradeButton()
    {
        if (CurrencyManager.money3 >= selectFoodUpgradePrice)
        {
            CurrencyManager.money3 -= selectFoodUpgradePrice;
        }
        else
        {
            return;
        }
        DiningRoomSystem.Instance.UpgradeSelectFoodLevel();

        if(DiningRoomSystem.Instance.isMaxSelectUpgradeLevel)
        {
            selectFoodUpgradeButton.interactable = false;
        }
        SelectUpgradeTextSetting();
    }
    private void CheckUpgradeButton()
    {
        if (CurrencyManager.money1 < timerUpgradePrice || DiningRoomSystem.Instance.isMaxTimerUpgradeLevel)
        {
            timerUpgradeButton.interactable = false; 
        }
        else
        {
            timerUpgradeButton.interactable = true;
        }
        if (CurrencyManager.money3 < selectFoodUpgradePrice || DiningRoomSystem.Instance.isMaxSelectUpgradeLevel)
        {
            selectFoodUpgradeButton.interactable = false;
        }
        else
        {
            selectFoodUpgradeButton.interactable = true;    
        }
        
    }
   
}
