using System.Text;
using TMPro;
using UnityEngine;

public class ItemPer : MonoBehaviour
{
    public TextMeshProUGUI windowName;
    public TextMeshProUGUI info;

    private ShopSystem shop;
    private StringTable stringTable;

    private bool first = false;
    public void Setting()
    {
        if(!first)
        {
            shop = ShopSystem.Instance;
            stringTable = MakeTableData.Instance.stringTable;
            first = true;
        }

        if(Global.language == Language.KOR)
        {
            windowName.text = $"{stringTable.GetStringTableData("Gatcha006").KOR}";
            var item = shop.itemTable.drops[shop.currentItemRank].item.items;
            float weight = 0f;
            StringBuilder sb = new StringBuilder();

            string lev = string.Format(stringTable.GetStringTableData("Gatcha009").KOR, shop.currentItemRank);
            sb.AppendLine($"{lev}");
            sb.AppendLine();
            foreach (var itemP in item)
            {
                weight += itemP.weight;
            }
            foreach(var itemR in item)
            { 
                sb.AppendLine($"{stringTable.GetStringTableData(itemR.item.name + "_Name").KOR} : {(itemR.weight / weight):F5}%");
            }
            info.text = $"{sb}";
        }
        else if(Global.language == Language.ENG)
        {
            windowName.text = $"{stringTable.GetStringTableData("Gatcha006").ENG}";
            var item = shop.itemTable.drops[shop.currentItemRank].item.items;
            float weight = 0f;
            StringBuilder sb = new StringBuilder();

            string lev = string.Format(stringTable.GetStringTableData("Gatcha009").ENG, shop.currentItemRank);
            sb.AppendLine($"{lev}");
            sb.AppendLine();
            foreach (var itemP in item)
            {
                weight += itemP.weight;
            }
            foreach (var itemR in item)
            {
                sb.AppendLine($"{stringTable.GetStringTableData(itemR.item.name + "_Name").ENG} : {(itemR.weight / weight):F5}%");
            }
            info.text = $"{sb}";
        }
    }
}
