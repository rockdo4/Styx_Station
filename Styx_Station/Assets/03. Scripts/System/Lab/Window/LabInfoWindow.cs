using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LabInfoWindow : Window
{
    private StringTableData labTypeNameStringDatas;
    private StringTableData labTypeBuffStringDatas;
    private StringTableData labTaxBuffStringDatas;
    private StringTableData timerStringDatas;

    private int level;
    private float timer;

    public TextMeshProUGUI researchTypeText;
    public TextMeshProUGUI researchTypeBuffText;
    public TextMeshProUGUI researchTaxText;
    public TextMeshProUGUI researchTimer;

    public void SetVertex(StringTableData labTypeNameStringDatas,StringTableData labTypeBuffStringDatas,StringTableData labTaxBuffStringDatas, StringTableData timerStringDatas, int level,float timer)
    {
        this.labTypeNameStringDatas = labTypeNameStringDatas;
        this.labTypeBuffStringDatas = labTypeBuffStringDatas;
        this.labTaxBuffStringDatas=labTaxBuffStringDatas;
        this.timerStringDatas = timerStringDatas;
        this.level = level;
        this.timer = timer;
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

}
