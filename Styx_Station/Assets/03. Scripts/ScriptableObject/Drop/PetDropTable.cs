using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drops/PetDropTable")]
public class PetDropTable : ScriptableObject
{
    public List<DropTable> drops = new List<DropTable>();

    [System.Serializable]
    public class DropTable
    {
        public PetDrop pet;
        public int RankUp;
    }

    public Pet GetPet(int rank)
    {
        if (drops == null)
            return null;

        if (drops.Count <= 0)
            return null;

        if (rank >= drops.Count)
            return null;

        return drops[rank].pet.PickUp();
    }
}
