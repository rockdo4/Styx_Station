using System;
using TMPro;

public class BangchiWindow : Window
{
    private long sliver=0;
    private long pogme =0;

    public string bangchiOverTimeStringKey;
    public string bangchiPanelNameStringKey;
    private StringTableData bangchiOverTimeStringTagleData;
    private StringTableData bangchiPanelNameStringTableData;

    public TextMeshProUGUI bangchiOverTimeTextMeshProUGUI;
    public TextMeshProUGUI bangchiName;

    public TextMeshProUGUI sliverText;
    public TextMeshProUGUI pgmeText;

    private bool isAwakeSet;
    public override void Open()
    {
        if(!isAwakeSet)
        {
            if (MakeTableData.Instance.stringTable != null)
            {
                bangchiOverTimeStringTagleData = MakeTableData.Instance.stringTable.GetStringTableData(bangchiOverTimeStringKey);
                bangchiPanelNameStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(bangchiPanelNameStringKey);
            }
            else
            {
                MakeTableData.Instance.stringTable = new StringTable();
                bangchiOverTimeStringTagleData = MakeTableData.Instance.stringTable.GetStringTableData(bangchiOverTimeStringKey);
                bangchiPanelNameStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(bangchiPanelNameStringKey);
            }

            isAwakeSet=true;
        }
       
        var prevData = DateTime.ParseExact(GameData.keyPrevAccumlateTime.ToString(), GameData.datetimeString, null);
        var date = DateTime.ParseExact(GameData.nowTime.ToString(), GameData.datetimeString, null);

        TimeSpan timeDifference = date.Subtract(prevData);
        var minutes = (int)timeDifference.TotalMinutes;
        if(minutes >=GameData.maxResult)
            minutes = GameData.maxResult;
        switch (Global.language)
        {
            case Language.KOR:
                bangchiOverTimeTextMeshProUGUI.text =$"{minutes} {bangchiOverTimeStringTagleData.KOR}";
                bangchiName.text = bangchiPanelNameStringTableData.KOR;
                break;
            case Language.ENG:
                bangchiOverTimeTextMeshProUGUI.text = $"{minutes} {bangchiOverTimeStringTagleData.ENG}";
                bangchiName.text = bangchiPanelNameStringTableData.ENG;
                break;
        }
        if (WaveManager.Instance.currStage.isBossWave)
        {
            sliver = WaveManager.Instance.currStage.bossMonster.monster_coin * GameData.GetCompensationTime();
            pogme = WaveManager.Instance.currStage.bossMonster.monster_coin * GameData.GetCompensationTime();
        }
        else
        {
            sliver = WaveManager.Instance.currStage.monsterList[0].monster_coin * GameData.GetCompensationTime();
            pogme = WaveManager.Instance.currStage.monsterList[0].monster_pommegrande * GameData.GetCompensationTime();
        }
       
        if(pogme ==0)
        {
            pogme = sliver / 10;
        }
        sliverText.text = $"{sliver}";
        pgmeText.text = $"{pogme}";
        base.Open();
    }

    public override void Close()
    {
        GameData.ResetCompensationTime();
        CurrencyManager.GetSilver(sliver, 0);
        CurrencyManager.GetSilver(pogme, 1);
        base.Close();
    }
}
