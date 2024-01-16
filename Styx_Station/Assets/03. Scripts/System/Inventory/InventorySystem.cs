using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    private static InventorySystem instance;

    public static InventorySystem Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("InventorySystem");
                instance = go.AddComponent<InventorySystem>();
                instance.item = Resources.Load<ItemTable>("Table/ItemTable");
                instance.skill = Resources.Load<SkillTable>("Table/SkillTable");
                instance.pet = Resources.Load<PetTable>("Table/PetTable");

                instance.optionTable = Resources.Load<CustomOptionTable>("Table/CustomOptionTable");
                instance.inventory = go.AddComponent<Inventory>();
                instance.skillInventory = go.AddComponent<SkillInventory>();
                instance.petInventory = go.AddComponent<PetInventory>();
                instance.customMaker = go.AddComponent<CreateCustom>();
                instance.stateSystem = StateSystem.Instance;
                instance.Setting();
                instance.shopSystem = ShopSystem.Instance;
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public Inventory inventory;
    public SkillInventory skillInventory;
    public PetInventory petInventory;

    private StateSystem stateSystem;

    private ShopSystem shopSystem;

    public CreateCustom customMaker;

    private ItemTable item;
    private SkillTable skill;
    private PetTable pet;

    public CustomOptionTable optionTable { get; private set; }

    public Inventory.InventoryItem LoadCustom(Item item, List<Item.AddOption> options)
    {
        var dummy = inventory.CreateDummy(item);

        foreach(var option in options)
        {
            dummy.addOptions.Add(option);
        }

        var dummys = inventory.AddCustom(item, dummy);

        return dummys;
    }

    public Inventory.InventoryItem Custom(Item item, int index, string table)
    {
        var dummy = inventory.CreateDummy(item);

        for (int i = 0; i < index; ++i)
        {
            OptionCustom(dummy, table);
        }

        var dummys = inventory.AddCustom(item, dummy);

        return dummys;
    }

    public void OptionCustom(Item item, string tableName)
    {
        if (item == null)
            return;

        var option = optionTable.GetPickCustom(tableName);

        if (option == null)
            return;

        if (option.optionName == AddOptionString.None)
            return;

        switch(item.enchant)
        {
            case Enchant.Old:
                option.value *= 1f;
                break;
            case Enchant.EntryLevel:
                option.value *= 1.2f;
                break;
            case Enchant.Creation:
                option.value *= 1.4f;
                break;
            case Enchant.Masters:
                option.value *= 1.6f;
                break;
            case Enchant.MasterPiece:
                option.value *= 2.0f;
                break;
        }
        item.AddOptions(option.optionName, option.value);
    }

    public void Setting()
    {
        for (int i = 0; i < item.GetTableSize(); ++i)
        {
            var addItem = item.GetItemList(i);
            inventory.AddItem(addItem);
        }

        for(int i=0; i< skill.GetTableSize(); ++i)
        {
            var addSkill = skill.GetSkill(i);
            skillInventory.AddSkill(addSkill);
        }


        for (int i = 0; i < pet.GetTableSize(); ++i)
        {
            var addPet = pet.GetPet(i);
            petInventory.AddPet(addPet);
        }

        for (int i = 0; i <= (int)ItemType.Symbol; ++i)
        {
            inventory.ItemSorting((ItemType)i);
        }

        skillInventory.SkillSorting();

        petInventory.PetSorting();

        stateSystem.Setting();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            customMaker.CreateCustomItem(1);
        }
    }
}