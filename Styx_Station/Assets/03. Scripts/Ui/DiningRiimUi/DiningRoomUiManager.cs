using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiningRoomUiManager : MonoBehaviour
{
    private bool isDrawDiningRoomDislay;
    public GameObject roomMainPanel;
    public DiningRoomUiFoodButton diningRoomUiButton;
    public TextMeshProUGUI timerText;
    public float MaxTimer;
    private float timer;
    public List<Sprite> foodSpriteList = new List<Sprite>();
    private bool isResetDiningTable;
    DiningTable diningTable;
    private List<string> foodId;

    private List<FoodData> fGradeFood=new List<FoodData>();
    private List<FoodData> eGradeFood = new List<FoodData>();
    private List<FoodData> dGradeFood = new List<FoodData>();
    private List<FoodData> cGradeFood = new List<FoodData>();
    private List<FoodData> bGradeFood = new List<FoodData>();
    private List<FoodData> aGradeFood = new List<FoodData>();
    private List<FoodData> sGradeFood = new List<FoodData>();
    private int maxFoodPer;
    private void Awake()
    {
        roomMainPanel.SetActive(isDrawDiningRoomDislay);
        Debug.Log("음식만들기 준비중");
    }
    private void Start()
    {
        timer = MaxTimer;
        if(!isResetDiningTable)
        {
            diningTable =new DiningTable();
            isResetDiningTable =true;
        }
        if(diningTable != null)
        {
            foodId=diningTable.GetFoodTableID();
        }
        for(int i=0;i< diningTable.dic.Count; i++)
        {
            for(int j =0; j < foodSpriteList.Count;j++)
            {
                if (diningTable.dic[foodId[i]].Food_Name_ID == foodSpriteList[i].name)
                {
                    var t = diningTable.dic[foodId[i]];
                    FoodData type = new FoodData();
                    type.sprite = foodSpriteList[i];
                    type.Food_Per = t.Food_Per;
                    type.Food_Sil = t.Food_Sil;
                    type.Food_Soul  = t.Food_Soul; 
                    type.Food_ATK = t.Food_ATK;
                    type.Food_Cri = t.Food_Cri;
                    type.Food_Skill=t.Food_Skill;
                    type.Food_Boss= t.Food_Boss;
                    type.Food_Silup= t.Food_Silup;
                    type.Food_Du= t.Food_Du;
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
    private void Update()
    {
        if(!diningRoomUiButton.isFullFood)
            TimeCounting();
    }

    public void OnClickDrawDiningRoomDisplay()
    {
        isDrawDiningRoomDislay = !isDrawDiningRoomDislay;
        roomMainPanel.SetActive(isDrawDiningRoomDislay);
    }

    private void TimeCounting()
    {
        timer -= Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);

        timerText.text = timeSpan.ToString(@"hh\:mm\:ss");

        if (timer <= 0)
        {

            var range = UnityEngine.Random.Range(0, 100);
            if(range <=40)
            {
                int max = 0;
                for(int i =0;i<fGradeFood.Count;i++)
                {
                    max += fGradeFood[i].Food_Per;
                }
                var result = UnityEngine.Random.Range(0, max);
                int betwon=0;
                for (int i = 0; i < fGradeFood.Count; i++)
                {
                    betwon += fGradeFood[i].Food_Per;
                    if (result <=betwon)
                    {
                        diningRoomUiButton.MakeFood(fGradeFood[i]);
                        timer = MaxTimer;
                        break;
                    }
                }
            }
            else if(range >40 && range <=65)
            {
                int max = 0;
                for (int i = 0; i < eGradeFood.Count; i++)
                {
                    max += eGradeFood[i].Food_Per;
                }
                var result = UnityEngine.Random.Range(0, max);
                int betwon = 0;
                for (int i = 0; i < eGradeFood.Count; i++)
                {
                    betwon += eGradeFood[i].Food_Per;
                    if (result <= betwon)
                    {
                        diningRoomUiButton.MakeFood(eGradeFood[i]);
                        timer = MaxTimer;
                        break;
                    }
                }
            }
            else if(range >65 && range<=80)
            {
                int max = 0;
                for (int i = 0; i < fGradeFood.Count; i++)
                {
                    max += fGradeFood[i].Food_Per;
                }
                var result = UnityEngine.Random.Range(0, max);
                int betwon = 0;
                for (int i = 0; i < fGradeFood.Count; i++)
                {
                    betwon += fGradeFood[i].Food_Per;
                    if (result <= betwon)
                    {
                        diningRoomUiButton.MakeFood(fGradeFood[i]);
                        timer = MaxTimer;
                        break;
                    }
                }
            }
            else if (range > 80 && range <= 90)
            {
                int max = 0;
                for (int i = 0; i < cGradeFood.Count; i++)
                {
                    max += cGradeFood[i].Food_Per;
                }
                var result = UnityEngine.Random.Range(0, max);
                int betwon = 0;
                for (int i = 0; i < cGradeFood.Count; i++)
                {
                    betwon += cGradeFood[i].Food_Per;
                    if (result <= betwon)
                    {
                        diningRoomUiButton.MakeFood(cGradeFood[i]);
                        timer = MaxTimer;
                        break;
                    }
                }
            }
            else if(range >90 && range <=96)
            {
                int max = 0;
                for (int i = 0; i < bGradeFood.Count; i++)
                {
                    max += bGradeFood[i].Food_Per;
                }
                var result = UnityEngine.Random.Range(0, max);
                int betwon = 0;
                for (int i = 0; i < bGradeFood.Count; i++)
                {
                    betwon += bGradeFood[i].Food_Per;
                    if (result <= betwon)
                    {
                        diningRoomUiButton.MakeFood(bGradeFood[i]);
                        timer = MaxTimer;
                        break;
                    }
                }
            }
            else if(range >96 && range<=99)
            {
                int max = 0;
                for (int i = 0; i < aGradeFood.Count; i++)
                {
                    max += aGradeFood[i].Food_Per;
                }
                var result = UnityEngine.Random.Range(0, max);
                int betwon = 0;
                for (int i = 0; i < aGradeFood.Count; i++)
                {
                    betwon += aGradeFood[i].Food_Per;
                    if (result <= betwon)
                    {
                        diningRoomUiButton.MakeFood(aGradeFood[i]);
                        timer = MaxTimer;
                        break;
                    }
                }
            }
            else
            {
                int max = 0;
                for (int i = 0; i < sGradeFood.Count; i++)
                {
                    max += sGradeFood[i].Food_Per;
                }
                var result = UnityEngine.Random.Range(0, max);
                int betwon = 0;
                for (int i = 0; i < sGradeFood.Count; i++)
                {
                    betwon += sGradeFood[i].Food_Per;
                    if (result <= betwon)
                    {
                        diningRoomUiButton.MakeFood(sGradeFood[i]);
                        timer = MaxTimer;
                        break;
                    }
                }
            }
            
        }
    }

}
