using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Pets/PetTable")]
public class PetTable : ScriptableObject
{
    [SerializeField]
    private List<Pet> table = new List<Pet>();

    public int GetTableSize()
    {
        return table.Count;
    }

    public Pet GetPet(int index)
    {
        return table[index];
    }

    public Pet GetPet(string name)
    {
        return table.Where(x => x.name == name).FirstOrDefault();
    }
}
