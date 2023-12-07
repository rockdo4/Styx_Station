using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguageButton : MonoBehaviour
{
    public void ChnageLanguage()
    {
        if (Global.language == Language.KOR)
            Global.language = Language.ENG;
        else if(Global.language == Language.ENG)
            Global.language = Language.KOR;
    }
}
