using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeUiText : MonoBehaviour
{
    private Language prevLanguage = Global.language;
    public Language language =Global.language;
    public TMP_FontAsset korfont;
    public TMP_FontAsset engfont;
    public string stringTableKey;
    public TextMeshProUGUI textMeshProUGUI;
    [Range(0f, 100f)]
    public float korStringSize = 30f;
    private float prevKorStringSize;
    [Range(0f, 100f)]
    public float engStringSize = 30f;
    private float prevEngStringSize;
    private StringTableData StringTable;
    private void Awake()
    {
        prevKorStringSize = korStringSize;
        prevEngStringSize = engStringSize;
    }
    private void Start()
    {

        if (TestChangeLanauge.Instance.stringTable != null)
        {
            StringTable = TestChangeLanauge.Instance.stringTable.dic[stringTableKey];
        }
        else
        {
            TestChangeLanauge.Instance.stringTable = new StringTable();
            StringTable = TestChangeLanauge.Instance.stringTable.dic[stringTableKey];
        }
        SetText();
    }
    public void Update()
    {
        if (language != prevLanguage || language != Global.language)
        {
            SetText();
        }
        if (language == Language.KOR || prevKorStringSize != korStringSize)
        {
            textMeshProUGUI.fontSize = korStringSize;
        }
        if (language == Language.ENG && prevEngStringSize != engStringSize)
        {
            textMeshProUGUI.fontSize = engStringSize;
        }
    }

    private void SetText()
    {
        if(language != Global.language)
        {
            Global.language =language ;
            prevLanguage= language;
        }
        else prevLanguage = language;
        switch (language)
        {
            case Language.KOR:
                textMeshProUGUI.font = korfont;
                textMeshProUGUI.text = $"{StringTable.KOR}";
                textMeshProUGUI.fontSize = korStringSize;
                break;
            case Language.ENG:
                textMeshProUGUI.font = engfont;
                textMeshProUGUI.text = $"{StringTable.ENG}";
                textMeshProUGUI.fontSize = engStringSize;
                break;
        }
    }
}
