using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PetInventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryPet
    {
        public Pet pet;
        public int upgradeLev;
        public bool acquire;
        public bool equip;
        public int stock;
        public int petIndex;
        public int equipIndex;

        public InventoryPet(Pet pet, int upgradeLev, bool acquire, bool equip, int stock, int petIndex, int equipIndex)
        {
            this.pet = pet;
            this.upgradeLev = upgradeLev;
            this.acquire = acquire;
            this.equip = equip;
            this.stock = stock;
            this.petIndex = petIndex;
            this.equipIndex = equipIndex;
        }
    }

    public List<InventoryPet> pets { get; private set; } = new List<InventoryPet>();

    public InventoryPet[] equipPets { get; private set; } = new InventoryPet[2];

    public void PetSorting()
    {
        for(int i = 0; i < pets.Count; ++i)
        {
            pets[i].petIndex = i;
        }
    }

    public void AddPet(Pet pet)
    {
        if (pet == null)
            return;

        var addPet = pets.Where(x=>x.pet.name == pet.name).FirstOrDefault();

        if (addPet != null)
            return;

        pets.Add(new InventoryPet(pet, 0, false, false, 0, -1, -1));
    }

    public void EquipPet(int petIndex, int equipIndex)
    {
        if (petIndex < 0)
            return;

        if (equipIndex < 0)
            return;

        if (equipPets == null)
            return;

        if (pets[petIndex].equip)
            DequipPet(petIndex, pets[petIndex].equipIndex);

        if (equipPets[equipIndex] == null || equipPets[equipIndex].pet == null)
            equipPets[equipIndex] = pets[petIndex];

        else if (equipPets[equipIndex] != null || equipPets[equipIndex].pet != null)
        {
            equipPets[equipIndex].equip = false;
            equipPets[equipIndex].equipIndex = -1;
        }

        equipPets[equipIndex] = pets[petIndex];
        pets[petIndex].equip = true;
        pets[petIndex].equipIndex = equipIndex;
    }

    public void DequipPet(int petIndex, int equipIndex)
    {
        if (pets == null)
            return;

        if (petIndex < 0)
            return;

        if (equipIndex < 0)
            return;

        if (pets[petIndex] == null)
            return;

        if (pets[petIndex].pet == null)
            return;

        if (!pets[petIndex].acquire)
            return;


        if (equipPets[equipIndex] == null)
            return;

        if (equipPets[equipIndex].pet == null)
            return;

        pets[petIndex].equip = false;
        pets[petIndex].equipIndex = -1;
        equipPets[equipIndex] = null;
    }
}
