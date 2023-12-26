using System.Text;
using TMPro;
using UnityEngine;

public class ShopPer : MonoBehaviour
{
    public TextMeshProUGUI windowName;
    public TextMeshProUGUI info;

    private ShopSystem shop;
    private StringTable stringTable;

    private bool first = false;
    public void Setting()
    {
        if (!first)
        {
            shop = ShopSystem.Instance;
            stringTable = MakeTableData.Instance.stringTable;
            first = true;
        }

        if (Global.language == Language.KOR)
        {
            windowName.text = $"{stringTable.GetStringTableData("Gatcha007").KOR}";
            var skill = shop.skillTable.drops[shop.currentSkillRank].skill.skills;
            float weight = 0f;
            StringBuilder sb = new StringBuilder();

            string lev = string.Format(stringTable.GetStringTableData("Gatcha009").KOR, shop.currentSkillRank);
            sb.AppendLine($"{lev}");
            sb.AppendLine();
            foreach (var skillP in skill)
            {
                weight += skillP.weight;
            }
            foreach (var skillR in skill)
            {
                sb.AppendLine($"{stringTable.GetStringTableData(skillR.skill.name + "_Name").KOR} : {(skillR.weight / weight):F5}%");
            }
            info.text = $"{sb}";
        }
        else if (Global.language == Language.ENG)
        {
            windowName.text = $"{stringTable.GetStringTableData("Gatcha007").ENG}";
            var skill = shop.skillTable.drops[shop.currentSkillRank].skill.skills;
            float weight = 0f;
            StringBuilder sb = new StringBuilder();

            string lev = string.Format(stringTable.GetStringTableData("Gatcha009").ENG, shop.currentSkillRank);
            sb.AppendLine($"{lev}");
            sb.AppendLine();
            foreach (var skillP in skill)
            {
                weight += skillP.weight;
            }
            foreach (var skillR in skill)
            {
                sb.AppendLine($"{stringTable.GetStringTableData(skillR.skill.name + "_Name").ENG} : {(skillR.weight / weight):F5}%");
            }
            info.text = $"{sb}";
        }
    }
}
