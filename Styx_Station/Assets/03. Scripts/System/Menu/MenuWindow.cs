using UnityEngine.UI;
using UnityEngine;

public class MenuWindow : Window
{
    public Button settingButton;
    public Button saveModeButton;
    public GameObject settingBox;

    public override void Open()
    {
        ButtonList.settingButton = SettingButton.None;
        base.Open();
    }

    public override void Close()
    {
        if((ButtonList.settingButton & SettingButton.CouPon) != 0)
        {
            ButtonList.settingButton &= ~SettingButton.CouPon;
            settingBox.GetComponent<SettingBox>().couponWindow.SetActive(false);
        }

        if((ButtonList.settingButton & SettingButton.Cheat)!=0)
        {
            ButtonList.settingButton &= ~SettingButton.Cheat;
            settingBox.GetComponent<SettingBox>().cheat.SetActive(false);
        }
        ButtonList.settingButton = SettingButton.None;
        settingBox.SetActive(false);

        base.Close();
    }

    public void OnClickSaveMode()
    {
        if ((ButtonList.settingButton & SettingButton.SaveMode) == 0)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;

            ButtonList.settingButton |= SettingButton.SaveMode;
        }
    }

    public void OnClickSettingOpen()
    {
        if((ButtonList.settingButton & SettingButton.Setting) == 0 &&
            (ButtonList.settingButton & SettingButton.SaveMode) == 0)
        {
            ButtonList.settingButton |= SettingButton.Setting;
            settingBox.SetActive(true);
            settingBox.GetComponent<SettingBox>().SettingBoxTextUpdate();
        }
    }

    public void OnClickSettingClose()
    {
        if ((ButtonList.settingButton & SettingButton.Setting) != 0 &&
        (ButtonList.settingButton & SettingButton.SaveMode) == 0)
        {
            ButtonList.settingButton &= ~SettingButton.Setting;
            settingBox.SetActive(false);
        }
    }

    private void Update() 
    {
        if(Input.anyKeyDown && (ButtonList.settingButton & SettingButton.SaveMode) != 0)
        {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                ButtonList.settingButton &= ~SettingButton.SaveMode;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && (ButtonList.mainButton & ButtonType.Menu) != 0)
        {
            if((ButtonList.settingButton & SettingButton.Setting) !=0 &&
            (ButtonList.settingButton & SettingButton.SaveMode) == 0 &&
            (ButtonList.settingButton & SettingButton.CouPon) == 0 &&
            (ButtonList.settingButton & SettingButton.Cheat) == 0)
            {
                ButtonList.settingButton &= ~SettingButton.Setting;
                settingBox.SetActive(false);
            }
        } 
    }
}
