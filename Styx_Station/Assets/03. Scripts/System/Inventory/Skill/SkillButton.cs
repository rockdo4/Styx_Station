using TMPro;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public SkillInventory inventory;

    public TextMeshProUGUI skillName;

    public int skillIndex;

    public void OnClickOpenInfo(SkillWindow window)
    {
        if (window == null)
            return;

        window.selectIndex = skillIndex;
        window.skillTabs.SetActive(false);
        var info = window.info.GetComponent<SkillInfoUi>();
        info.selectIndex = skillIndex;
        info.InfoUpdate();
        window.info.SetActive(true);
    }
}
