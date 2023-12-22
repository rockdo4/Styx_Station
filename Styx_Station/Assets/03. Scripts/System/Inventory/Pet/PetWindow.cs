using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PetWindow : SubWindow
{
    private PetInventory inventory;
    private StringTable stringTable;

    public GameObject pets;

    public GameObject petSlot;

    public int selectIndex = -1;

    private bool first = false;
    public List<GameObject> petsList {  get; private set; } = new List<GameObject>();
    public override void Open()
    {
        Setting();

        base.Open();

        for(int i=0; i<petsList.Count; ++i)
        {
            var pet = petsList[i].GetComponent<PetInfo>();

            if (!inventory.pets[i].acquire)
            {
                Color currentColor = pet.image.color;
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.3f);
                pet.image.color = newColor;
                pet.Equip.interactable = false;
                pet.Upgrade.interactable = false;
            }
            else
            {
                Color currentColor = pet.image.color;
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
                pet.image.color = newColor;
                pet.Equip.interactable = true;
                pet.Upgrade.interactable = true;
            }
        }

        InfoTextUpdate();
    }

    public override void Close()
    {
        base.Close();
    }

    public void InfoTextUpdate()
    {
        for (int i = 0; i < petsList.Count; ++i)
        {
            var pet = petsList[i].GetComponent<PetInfo>();

            if (!inventory.pets[i].equip)
            {
                pet.equipMark.gameObject.SetActive(false);
            }
            else
            {
                pet.equipMark.gameObject.SetActive(true);
            }

            if (Global.language == Language.KOR)
            {
                pet.petName.text = $"{stringTable.GetStringTableData(pet.pet.pet.name + "_Name").KOR}";
                pet.Lv.text = $"Lv.{pet.pet.upgradeLev}";
                string atk = string.Format(stringTable.GetStringTableData("Pet_Info_Atk").KOR, pet.pet.pet.Pet_Attack + pet.pet.pet.Pet_Attack_Lv * pet.pet.upgradeLev);
                pet.atk.text = $"{atk}";
                string atkSpeed = string.Format(stringTable.GetStringTableData("Pet_Info_As").KOR, pet.pet.pet.Pet_AttackSpeed);
                pet.atkSpeed.text = $"{atkSpeed}";
                pet.Upgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Upgrade").KOR}";
                if (inventory.pets[i].equip)
                {
                    pet.Equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Dequip").KOR}";
                }
                else
                {
                    pet.Equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Equip").KOR}";
                }
            }
            else if (Global.language == Language.ENG)
            {
                pet.petName.text = $"{stringTable.GetStringTableData(pet.pet.pet.name + "_Name").ENG}";
                pet.Lv.text = $"Lv.{pet.pet.upgradeLev}";
                string atk = string.Format(stringTable.GetStringTableData("Pet_Info_Atk").ENG, pet.pet.pet.Pet_Attack + pet.pet.pet.Pet_Attack_Lv * pet.pet.upgradeLev);
                pet.atk.text = $"{atk}";
                string atkSpeed = string.Format(stringTable.GetStringTableData("Pet_Info_As").ENG, pet.pet.pet.Pet_AttackSpeed);
                pet.atkSpeed.text = $"{atkSpeed}";
                pet.Upgrade.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Upgrade").ENG}";
                if (inventory.pets[i].equip)
                {
                    pet.Equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Dequip").ENG}";
                }
                else
                {
                    pet.Equip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{stringTable.GetStringTableData("Equip").ENG}";
                }
            }
        }
    }

    public void Setting()
    {
        if(!first)
        {
            inventory = InventorySystem.Instance.petInventory;
            stringTable = MakeTableData.Instance.stringTable;

            for(int i=0; i< inventory.pets.Count; ++i)
            {
                GameObject pet = Instantiate(petSlot, pets.transform);
                var info = pet.GetComponent<PetInfo>();
                info.inventory = inventory;
                info.pet = inventory.pets[i];
                info.Equip.onClick.AddListener(() => info.OnClickEquip(this));
                info.Upgrade.onClick.AddListener(() => info.OnClickUpgrade(this));
                petsList.Add(pet);
            }
            first = true;
        }
    }
}
