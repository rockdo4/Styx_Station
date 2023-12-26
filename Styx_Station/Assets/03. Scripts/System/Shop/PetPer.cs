using System.Text;
using TMPro;
using UnityEngine;
public class PetPer : MonoBehaviour
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
            windowName.text = $"{stringTable.GetStringTableData("Gatcha008").KOR}";
            var pet = shop.petTable.drops[shop.currentPetRank].pet.pets;
            float weight = 0f;
            StringBuilder sb = new StringBuilder();

            string lev = string.Format(stringTable.GetStringTableData("Gatcha009").KOR, shop.currentPetRank);
            sb.AppendLine($"{lev}");
            sb.AppendLine();
            foreach (var petP in pet)
            {
                weight += petP.weight;
            }
            foreach (var petR in pet)
            {
                sb.AppendLine($"{stringTable.GetStringTableData(petR.pet.name + "_Name").KOR} : {(petR.weight / weight):F5}%");
            }
            info.text = $"{sb}";
        }
        else if (Global.language == Language.ENG)
        {
            windowName.text = $"{stringTable.GetStringTableData("Gatcha008").ENG}";
            var pet = shop.petTable.drops[shop.currentPetRank].pet.pets;
            float weight = 0f;
            StringBuilder sb = new StringBuilder();

            string lev = string.Format(stringTable.GetStringTableData("Gatcha009").ENG, shop.currentPetRank);
            sb.AppendLine($"{lev}");
            sb.AppendLine();
            foreach (var petP in pet)
            {
                weight += petP.weight;
            }
            foreach (var petR in pet)
            {
                sb.AppendLine($"{stringTable.GetStringTableData(petR.pet.name + "_Name").ENG} : {(petR.weight / weight):F5}%");
            }
            info.text = $"{sb}";
        }
    }
}
