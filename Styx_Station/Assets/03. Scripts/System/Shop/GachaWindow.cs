using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaWindow : Window
{
    public GachaInfo info;
    public ItemGacha itemGacha;
    public SkillGacha skillGacha;

    private bool first = false;
    public override void Open()
    {
        base.Open();
        if(!first)
        {
            info.First(itemGacha, skillGacha);
            first = true;
        }

        itemGacha.GachaUpdate();
        skillGacha.GachaUpdate();
    }

    public override void Close()
    {
        base.Close();
    }
}
