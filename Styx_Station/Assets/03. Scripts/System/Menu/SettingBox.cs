using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingBox : MonoBehaviour
{
    private StringTable stringTable;

    public TextMeshProUGUI setting;

    public TextMeshProUGUI sound;
    public TextMeshProUGUI soundSetting;
    public TextMeshProUGUI sound_OnOff;

    public TextMeshProUGUI lenguage;
    public TextMeshProUGUI lenguageSetting;
    public TextMeshProUGUI lenguage_ENKR;

    public TextMeshProUGUI coupon;
    public TextMeshProUGUI couponEnter;
    public TextMeshProUGUI couponNumber;
    public TextMeshProUGUI couponSelect;


    public GameObject couponWindow;

    public GameObject cheat;

    public TMP_InputField couponText;

    private bool first = false;
    public bool soundValue = true;

    public AudioMixer mainGameAudioMixer;
    //12.29yyl
    public GameObject backgroundMusic;
    public void OnClickChangeLenguage()
    {
        if(Global.language == Language.KOR)
            Global.language = Language.ENG;
        else if(Global.language == Language.ENG)
            Global.language = Language.KOR;
        
        UIManager.Instance.AutoTextUpdate();
        SettingBoxTextUpdate();
    }
  
    public void OnClickChangeSound()
    {
        soundValue = !soundValue;
        SoundValueText();
        SoundSetting();
        //backgroundMusic.SetActive(soundValue);//////
    }

    public void OnClickCouPon()
    {
        if((ButtonList.settingButton & SettingButton.CouPon) == 0)
        {
            ButtonList.settingButton |= SettingButton.CouPon;
            couponWindow.SetActive(true);
        }
    }

    public void OnClickCouponInit()
    {
        if(couponText.text == "QWER")
        {
            couponText.text = string.Empty;
            if ((ButtonList.settingButton & SettingButton.CouPon) != 0)
            {
                ButtonList.settingButton &= ~SettingButton.CouPon;
                couponWindow.SetActive(false);
            }

            if ((ButtonList.settingButton & SettingButton.Cheat) == 0)
            {
                ButtonList.settingButton |= SettingButton.Cheat;
                cheat.SetActive(true);
            }
        }
        else
        {
            couponText.text = string.Empty;
            if ((ButtonList.settingButton & SettingButton.CouPon) != 0)
            {
                ButtonList.settingButton &= ~SettingButton.CouPon;
                couponWindow.SetActive(false);
            }
        }
    }

    public void OnClickCloseCheat()
    {
        if ((ButtonList.settingButton & SettingButton.CouPon) == 0 &&
            (ButtonList.settingButton & SettingButton.Cheat) != 0)
        {
            ButtonList.settingButton &= ~SettingButton.Cheat;
            cheat.SetActive(false);
        }
    }
    public void SettingBoxTextUpdate()
    {
        if(!first)
        {
            stringTable = MakeTableData.Instance.stringTable;
            first = true;
        }
        if(Global.language == Language.KOR)
        {
            setting.text = $"{stringTable.GetStringTableData("Setting001").KOR}";
            sound.text = $"{stringTable.GetStringTableData("Setting002").KOR}";
            soundSetting.text = $"{stringTable.GetStringTableData("Setting003").KOR}";

            SoundValueText();

            lenguage.text = $"{stringTable.GetStringTableData("Setting006").KOR}";
            lenguageSetting.text = $"{stringTable.GetStringTableData("Setting007").KOR}";
            lenguage_ENKR.text = $"{stringTable.GetStringTableData("Setting008").KOR}";

            coupon.text = $"{stringTable.GetStringTableData("Setting010").KOR}";
            couponEnter.text = $"{stringTable.GetStringTableData("Setting011").KOR}";
            couponNumber.text = $"{stringTable.GetStringTableData("Setting012").KOR}";
            couponSelect.text = $"{stringTable.GetStringTableData("Setting013").KOR}";
        }
        else if(Global.language == Language.ENG)
        {
            setting.text = $"{stringTable.GetStringTableData("Setting001").ENG}";
            sound.text = $"{stringTable.GetStringTableData("Setting002").ENG}";
            soundSetting.text = $"{stringTable.GetStringTableData("Setting003").ENG}";

            SoundValueText();

            lenguage.text = $"{stringTable.GetStringTableData("Setting006").ENG}";
            lenguageSetting.text = $"{stringTable.GetStringTableData("Setting007").ENG}";
            lenguage_ENKR.text = $"{stringTable.GetStringTableData("Setting009").ENG}";

            coupon.text = $"{stringTable.GetStringTableData("Setting010").ENG}";
            couponEnter.text = $"{stringTable.GetStringTableData("Setting011").ENG}";
            couponNumber.text = $"{stringTable.GetStringTableData("Setting012").ENG}";
            couponSelect.text = $"{stringTable.GetStringTableData("Setting013").ENG}";
        }
    }

    public void SoundValueText()
    {
        if (Global.language == Language.KOR)
        {
            if (soundValue)
            {
                sound_OnOff.text = $"{stringTable.GetStringTableData("Setting004").KOR}";
            }
            else
            {
                sound_OnOff.text = $"{stringTable.GetStringTableData("Setting005").KOR}";
            }
        }
        else if (Global.language == Language.ENG)
        {

            if (soundValue)
            {
                sound_OnOff.text = $"{stringTable.GetStringTableData("Setting004").ENG}";
            }
            else
            {
                sound_OnOff.text = $"{stringTable.GetStringTableData("Setting005").ENG}";
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && 
            (ButtonList.settingButton & SettingButton.CouPon) != 0 &&
            (ButtonList.settingButton & SettingButton.Cheat) == 0)
        {
            ButtonList.settingButton &= ~SettingButton.CouPon;
            couponWindow.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) &&
            (ButtonList.settingButton & SettingButton.CouPon) == 0 &&
            (ButtonList.settingButton & SettingButton.Cheat) != 0)
        {
            ButtonList.settingButton &= ~SettingButton.Cheat;
            cheat.SetActive(false);
        }
    }
    public void SoundSetting()
    {
        if (soundValue)
        {
            mainGameAudioMixer.SetFloat("GameSound", 0f);
        }
        else
        {
            mainGameAudioMixer.SetFloat("GameSound", -80f);
        }
    }
}
