using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class StringTableUI : MonoBehaviour
{
    public string key;
    private StringTableData data;
    private Language currentLang;
    private TextMeshProUGUI textMeshProUGUI;
    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        SetStrintTable();
    }
    public void SettingTextLanague()
    {
        if(currentLang != Global.language)
        {
            SetStrintTable();
        }
    }
    private void SetStrintTable()
    {
        if (textMeshProUGUI == null)
            return;

        if (data.ID == string.Empty || data.ID == null)
            data = MakeTableData.Instance.stringTable.GetStringTableData(key);

        currentLang = Global.language;
        switch (currentLang)
        {
            case Language.KOR:

                textMeshProUGUI.text = data.KOR;
                break;
            case Language.ENG:
                textMeshProUGUI.text = data.ENG;
                break;
        }
    }
}
