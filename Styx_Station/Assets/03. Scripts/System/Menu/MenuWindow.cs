using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering;

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
            SavePower.SaveScreenBrightness(0f);

            OnDemandRendering.renderFrameInterval = 3;

            SavePower.onOff = true;

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
        // 절전 모드 OFF
        if(Input.anyKeyDown && (ButtonList.settingButton & SettingButton.SaveMode) != 0)
        {
            Debug.Log("절전 모드 OFF 작업 시작");
            Debug.Log($"롤백할 밝기 : {SavePower.currentBrightness}");
            Debug.Log($"적용 전 현재 밝기 : {Screen.brightness}");

            SavePower.OnScreenBrightness();

            OnDemandRendering.renderFrameInterval = 1;

            SavePower.onOff = false;

            ButtonList.settingButton &= ~SettingButton.SaveMode;
            Debug.Log($"적용 후 현재 밝기 : {Screen.brightness}");
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
