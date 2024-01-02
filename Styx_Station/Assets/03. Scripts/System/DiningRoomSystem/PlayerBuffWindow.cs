using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerBuffWindow : Window
{
    public GameObject playerBuffInfoOBject;
    public Image foodImage;
    public TextMeshProUGUI buffInfoText;
    private StringTableData buffInfoStringTableData;
    private int[] buffInt = new int[6];
    private Language language;

    private bool isDraw;
    public GameObject playerBuffInfoObjectArrow;
    public GameObject playerBuffInfoObjectTextType;

    private bool isFristSetting;
    public Button foodBuffButton;

    public override void Open()
    {

        playerBuffInfoOBject.SetActive(true);
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
        if (!isFristSetting)
        {
            isFristSetting = true;
            var eventTrigger = foodBuffButton.GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                var pointerDown = new EventTrigger.Entry();
                pointerDown.eventID = EventTriggerType.PointerDown;
                pointerDown.callback.AddListener((data) => { DrawBuffText(); });
                eventTrigger.triggers.Add(pointerDown);

                var pointerUp = new EventTrigger.Entry();
                pointerUp.eventID = EventTriggerType.PointerUp;
                pointerUp.callback.AddListener((data) => { DrawBuffText(); });
                eventTrigger.triggers.Add(pointerUp);
            }
        }
        base.Open();
    }

    public override void Close()
    {
        playerBuffInfoOBject.SetActive(false);
        isDraw=false;
        ActiveFalseObj();
        base.Close();
    }
    private void LateUpdate()
    {
        if(language!=Global.language &&PlayerBuff.Instance.buffData.isEatFood && isDraw)
        {
            SetStringTableData();
        }
    }
    public void DrawBuffText()
    {
        if (!PlayerBuff.Instance.buffData.isEatFood)
            return;
        isDraw = !isDraw;
        if(isDraw)
        {
            ActiveTrueObj();
            SetStringTableData();
        }
        else
        {
            ActiveFalseObj();
        }
    }
    private void ActiveTrueObj()
    {
        playerBuffInfoObjectArrow.SetActive(true);
        playerBuffInfoObjectTextType.SetActive(true);
    }
    private void ActiveFalseObj()
    {
        playerBuffInfoObjectArrow.SetActive(false);
        playerBuffInfoObjectTextType.SetActive(false);
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
