using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitWindow : Window
{
    private bool isAwkeSetting;
    //public string exitWindowNameStringKey;
    //private StringTableData exitWindowNameStringTableData;
    //public TextMeshProUGUI exitWindowNameTextMeshProUGUI;

    //public string exitWindowInfoStringKey;
    //private StringTableData exitWindowInfoStringTableData;
    //public TextMeshProUGUI exitWindowInfoTextMeshProUGUI;

    //public string exitWindoYesStringKey;
    //private StringTableData exitWindoYesStringTableData;
    //public TextMeshProUGUI exitWindoYesTextMeshProUGUI;

    //public string exitWindowNoStrinKey;
    //private StringTableData exitWindowNoStringTableData;
    //public TextMeshProUGUI exitWindowNoTextMeshProUGUI;

    public List<StringTableUI> stringtableUi= new List<StringTableUI>();

    public override void Open()
    {
        //if(!isAwkeSetting)
        //{
        //    exitWindowNameStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(exitWindowNameStringKey);
        //    exitWindowInfoStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(exitWindowInfoStringKey);
        //    exitWindoYesStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(exitWindoYesStringKey);
        //    exitWindowNoStringTableData = MakeTableData.Instance.stringTable.GetStringTableData(exitWindowNoStrinKey);
        //    isAwkeSetting = true;
        //}
        //switch(Global.language)
        //{
        //    case Language.KOR:
        //        exitWindowNameTextMeshProUGUI.text = exitWindowNameStringTableData.KOR;
        //        exitWindowInfoTextMeshProUGUI.text = exitWindowInfoStringTableData.KOR;
        //        exitWindoYesTextMeshProUGUI.text = exitWindoYesStringTableData.KOR;
        //        exitWindowNoTextMeshProUGUI.text = exitWindowNoStringTableData.KOR;
        //        break;
        //    case Language.ENG:
        //        exitWindowNameTextMeshProUGUI.text = exitWindowNameStringTableData.ENG;
        //        exitWindowInfoTextMeshProUGUI.text = exitWindowInfoStringTableData.ENG;
        //        exitWindoYesTextMeshProUGUI.text = exitWindoYesStringTableData.ENG;
        //        exitWindowNoTextMeshProUGUI.text = exitWindowNoStringTableData.ENG;
        //        break;
        //}
        foreach(StringTableUI ui in stringtableUi)
        {
            ui.SettingTextLanague();
        }
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
