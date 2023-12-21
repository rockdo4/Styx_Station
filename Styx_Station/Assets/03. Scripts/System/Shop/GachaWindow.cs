using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaWindow : Window
{
    public GachaInfo info;
    public ItemGacha itemGacha;
    public SkillGacha skillGacha;
    public PetGacha petGacha;

    private bool first = false;
    public override void Open()
    {
        base.Open();
        if(!first)
        {
            info.First(itemGacha, skillGacha, petGacha);
            first = true;
        }

        itemGacha.GachaUpdate();
        skillGacha.GachaUpdate();
        petGacha.GachaUpdate();
    }

    public override void Close()
    {
        base.Close();
    }
}
