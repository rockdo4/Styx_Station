using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuffWindow : Window
{
    public List<GameObject> playerBuffInfoOBject=new List<GameObject>();
    public Image foodImage;
    public TextMeshProUGUI buffInfoText;
    private StringTableData buffInfoStringTableData;
    private int[] buffInt = new int[6];
    private Language language;
    public override void Open()
    {
        foreach(var obj in playerBuffInfoOBject)
        {
           obj.SetActive(true);
        }
        Sprite newFoodImage = null;
        if (MakeTableData.Instance.diningRoomTable ==null)
            MakeTableData.Instance.diningRoomTable =new DiningTable();
        if(MakeTableData.Instance.stringTable==null)
            MakeTableData.Instance.stringTable=new StringTable();

        FoodTableData foodData = MakeTableData.Instance.diningRoomTable.GetFoodTableData(PlayerBuff.Instance.foodId.ToString());
        var strKey = PlayerBuff.Instance.foodId.ToString() + "_BuffText"; 
        buffInfoStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(strKey);
        foreach (var sprite in UIManager.Instance.roomUIManager.foodSpriteList)
        {
            if (sprite.name == foodData.Food_Name_ID)
            {
                newFoodImage = sprite;
            }
        }
        if(newFoodImage != null) 
            foodImage.sprite = newFoodImage;
        SetStringTableData();
        base.Open();
    }

    public override void Close()
    {
        foreach (var obj in playerBuffInfoOBject)
        {
            obj.SetActive(false);
        }
        base.Close();
    }
    private void LateUpdate()
    {
        if(language!=Global.language)
        {
            SetStringTableData();
        }
    }
    private void SetStringTableData()
    {
        language = Global.language;
        string foodBuffStr;
        int currentBuffInt = 0;
        if (PlayerBuff.Instance.buffData.playerPowerBuff > 0)
        {
            buffInt[currentBuffInt] = PlayerBuff.Instance.buffData.playerPowerBuff;
            currentBuffInt++;
        }
        if (PlayerBuff.Instance.buffData.criticalPowerBuff > 0)
        {
            buffInt[currentBuffInt] = PlayerBuff.Instance.buffData.criticalPowerBuff;
            currentBuffInt++;
        }
        if (PlayerBuff.Instance.buffData.skillBuff > 0)
        {
            buffInt[currentBuffInt] = PlayerBuff.Instance.buffData.skillBuff;
            currentBuffInt++;
        }
        if (PlayerBuff.Instance.buffData.bossAttackBuff > 0)
        {
            buffInt[0] = PlayerBuff.Instance.buffData.bossAttackBuff;
            currentBuffInt++;
        }
        if (PlayerBuff.Instance.buffData.silingBuff > 0)
        {
            buffInt[currentBuffInt] = PlayerBuff.Instance.buffData.silingBuff;
            currentBuffInt++;
        }
        switch (language)
        {
            case Language.KOR:
                foodBuffStr = string.Format(buffInfoStringTableData.KOR, buffInt[0], buffInt[1], buffInt[2], buffInt[3], buffInt[4]);
                buffInfoText.text = foodBuffStr;
                break;
            case Language.ENG:
                foodBuffStr = string.Format(buffInfoStringTableData.ENG, buffInt[0], buffInt[1], buffInt[2], buffInt[3], buffInt[4]);
                buffInfoText.text = foodBuffStr;
                break;
        }
    }
}
