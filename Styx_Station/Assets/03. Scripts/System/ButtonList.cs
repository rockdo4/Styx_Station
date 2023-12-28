using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    None = 0,
    Main = 1 << 0, //1
    Info = 1 << 1, //2
    Train = 1 << 2, //4
    DiningRoom = 1 << 3, // 
    Lab = 1 << 4,
    Cleaning = 1 << 5,
    Failure = 1 << 6,
    Shop = 1 << 7,
    Menu = 1 << 8,
    TrainMove = 1 << 9,
    Exit=1 << 10,
}

public enum InfoButton
{
    None = 0,
    State = 1 << 0,
    Inventory = 1 << 1,
    Skill = 1 << 2,
    Pet = 1 << 3,
    WeaponInfo = 1 << 4,
    ArmorInfo = 1 << 5,
    RingInfo = 1 << 6,
    SymbolInfo = 1 << 7,
    SkillInfo = 1 << 8,
}

public enum gachaButton
{
    None = 0,
    Info = 1 << 0,
    Equip = 1 << 1,
    Skill = 1 << 2,
    Pet = 1 << 3,
}
public enum SettingButton
{
    None = 0,
    Setting = 1 << 0,
    SaveMode = 1 << 1,
    CouPon = 1 << 2,
    Cheat = 1 << 3,
}


public static class ButtonList
{
    public static ButtonType mainButton = ButtonType.Main;
    public static InfoButton infoButton = InfoButton.None;
    public static SettingButton settingButton = SettingButton.None;
    public static gachaButton gachaButton = gachaButton.None;
}
