using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drops/PetDrop")]
public class PetDrop : ScriptableObject
{
    [Tooltip("드롭 아이템 테이블")]
    public List<AddPet> pets = new List<AddPet>();

    [System.Serializable]
    public class AddPet
    {
        public Pet pet;
        public float weight;
    }

    public Pet PickUp()
    {
        if (pets == null)
            return null;

        if (pets.Count <= 0)
            return null;

        float sum = 0;
        foreach (var pet in pets)
        {
            sum += pet.weight;
        }

        if (sum <= 0)
            return null;

        if (sum > 1)
            return null;

        var random = Random.Range(0, sum);

        for (int i = 0; i < pets.Count; ++i)
        {
            var pet = pets[i];
            if (pet.weight > random)
                return pet.pet;

            else
                random -= pet.weight;
        }

        return null;
    }
}
