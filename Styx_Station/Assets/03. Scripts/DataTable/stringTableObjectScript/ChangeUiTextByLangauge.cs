using TMPro;
using UnityEngine;
public class ChangeUiTextByLangauge : MonoBehaviour
{
    
    [HideInInspector]public Language language =Global.language;
    public TMP_FontAsset korfont;
    public TMP_FontAsset engfont;
    public string stringTableKey;
    public TextMeshProUGUI textMeshProUGUI;
    private StringTableData StringTable;
    private void Awake()
    {
    }
    private void Start()
    {
        if (MakeTableData.Instance.stringTable != null)
        {
            StringTable = MakeTableData.Instance.stringTable.dic[stringTableKey];
        }
        else
        {
            MakeTableData.Instance.stringTable = new StringTable();
            StringTable = MakeTableData.Instance.stringTable.dic[stringTableKey];
        }
        SetText();
    }
    public void Update()
    {
        if (language != Global.language)
        {
            SetText();
        }
    }

    private void SetText()
    {
        language=Global.language;
        switch (language)
        {
            case Language.KOR:
                textMeshProUGUI.font = korfont;
                textMeshProUGUI.text = $"{StringTable.KOR}";
                break;
            case Language.ENG:
                textMeshProUGUI.font = engfont;
                textMeshProUGUI.text = $"{StringTable.ENG}";
                break;
        }
    }
}
