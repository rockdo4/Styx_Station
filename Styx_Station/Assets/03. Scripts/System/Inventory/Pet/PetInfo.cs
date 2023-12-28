using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetInfo : MonoBehaviour
{
    public PetInventory inventory;

    public PetInventory.InventoryPet pet;

    public Image image;

    public TextMeshProUGUI equipMark;
    public TextMeshProUGUI Lv;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI atkSpeed;
    public TextMeshProUGUI petName;
    public TextMeshProUGUI upgradeValue;

    public Button Equip;
    public Button Upgrade;

    public void OnClickEquip(PetWindow window)
    {
        if(pet.equip)
        {
            window.selectIndex = pet.equipIndex;
            inventory.DequipPet(pet.petIndex, pet.equipIndex);
        }
        else if(!pet.equip)
        {
            if (inventory.equipPets[0] == null || inventory.equipPets[0].pet == null)
            {
                pet.equipIndex = 0;
                window.selectIndex = 0;
                inventory.EquipPet(pet.petIndex, pet.equipIndex);
            }
            else if (inventory.equipPets[1] == null || inventory.equipPets[1].pet == null)
            {
                pet.equipIndex = 1;
                window.selectIndex = 1;
                inventory.EquipPet(pet.petIndex, pet.equipIndex);
            }
            else
            {
                if(window.selectIndex == -1)
                {
                    window.selectIndex = 1;
                }

                inventory.DequipPet(inventory.equipPets[window.selectIndex].petIndex, window.selectIndex);
                inventory.EquipPet(pet.petIndex, window.selectIndex);
                
                if(window.selectIndex == 0)
                {
                    window.selectIndex = 1;
                }
                else if(window.selectIndex == 1)
                {
                    window.selectIndex = 0;
                }
            }
        }
        window.InfoTextUpdate();
    }

    public void OnClickUpgrade(PetWindow window)
    {
        gameObject.GetComponent<Upgrade>().PetUpgrade(pet.petIndex);

        window.InfoTextUpdate();
    }
}
