using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPopupText : MonoBehaviour
{
    public TextMeshProUGUI gameover001;
    public TextMeshProUGUI gameover002;
    public TextMeshProUGUI gameover003;
    public TextMeshProUGUI gameover004;

    private StringTable stringTable;

    public void TextUpdate()
    {
        if(stringTable == null)
        {
            stringTable = MakeTableData.Instance.stringTable;
        }

        if(Global.language == Language.KOR)
        {
            gameover001.text = $"{stringTable.GetStringTableData("Gameover001").KOR}";
            gameover002.text = $"{stringTable.GetStringTableData("Gameover002").KOR}";
            gameover003.text = $"{stringTable.GetStringTableData("Gameover003").KOR}";
            gameover004.text = $"{stringTable.GetStringTableData("Gameover004").KOR}";
        }
        else if(Global.language == Language.ENG)
        {
            gameover001.text = $"{stringTable.GetStringTableData("Gameover001").ENG}";
            gameover002.text = $"{stringTable.GetStringTableData("Gameover002").ENG}";
            gameover003.text = $"{stringTable.GetStringTableData("Gameover003").ENG}";
            gameover004.text = $"{stringTable.GetStringTableData("Gameover004").ENG}";
        }
    }
}
