using UnityEngine;

public static class SavePower
{
    public static float currentBrightness;
    public static bool onOff;

    public static void Setting()
    {
        Application.targetFrameRate = 60;
#if UNITY_ANDROID
        BrightnessSetting();
#endif
    }

    public static void BrightnessSetting()
    {
        currentBrightness = Screen.brightness;
        onOff = false;
    }

    public static void SaveScreenBrightness(float bright)
    {
        currentBrightness = Screen.brightness;

        Screen.brightness = bright;
    }

    public static void OnScreenBrightness()
    {
        Screen.brightness = currentBrightness;
    }
}
