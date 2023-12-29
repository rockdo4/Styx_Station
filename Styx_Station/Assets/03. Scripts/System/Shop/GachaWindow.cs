using TMPro;
using UnityEngine;

public class GachaWindow : Window
{
    private StringTable stringTable;
    public GachaInfo info;

    public TextMeshProUGUI windowName;
    public ItemGacha itemGacha;
    public SkillGacha skillGacha;
    public PetGacha petGacha;

    private bool first = false;
    public override void Open()
    {
        ButtonList.gachaButton = gachaButton.None;

        base.Open();

        if(!first)
        {
            if(stringTable == null)
            {
                stringTable = MakeTableData.Instance.stringTable;
            }
            info.First(itemGacha, skillGacha, petGacha);
            first = true;
        }

        if (Global.language == Language.KOR)
            windowName.text = $"{stringTable.GetStringTableData("Gatcha004").KOR}";
        else if (Global.language == Language.ENG)
            windowName.text = $"{stringTable.GetStringTableData("Gatcha004").ENG}";

        itemGacha.GachaUpdate();
        skillGacha.GachaUpdate();
        petGacha.GachaUpdate();
    }

    public override void Close()
    {
        info.OnClickGachaInfoClose();
        itemGacha.OnClickCloseItemPer();
        skillGacha.OnClickCloseSkillPer();
        petGacha.OnClickClosePetPer();

        base.Close();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) &&
            (ButtonList.mainButton & ButtonType.Shop) != 0 &&
            (ButtonList.gachaButton & gachaButton.Info) != 0)
        {
            ButtonList.gachaButton &= ~gachaButton.Info;
            info.OnClickGachaInfoClose();
        }
    }
}
