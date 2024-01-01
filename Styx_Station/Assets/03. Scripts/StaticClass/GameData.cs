using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class GameData
{
    public static StringBuilder exitTime = new StringBuilder() ; // 종료한 시간 시점 -> 식당칸에서 사용 예정
    public static StringBuilder keyPrevAccumlateTime = new StringBuilder();
    public static StringBuilder nowTime = new StringBuilder();

    public static int result = 0;
    public static int maxResult = 1440; // 24시간을 분으로 나눈 상태 

    [HideInInspector]
    public static string datetimeString = "yy년 MM월 dd일 HH시 mm분 ss초";

    public static StageData stageData;

    public static List<LabSaveData> Re_AtkSaveDataList = new List<LabSaveData>();
    public static List<LabSaveData> Re_HPSaveDataList = new List<LabSaveData>();
    public static List<LabSaveData> Re_CriSaveDataList =new List<LabSaveData>();
    public static List<LabSaveData>Re_SilupSaveDataList=new List<LabSaveData>();
    public static List<LabSaveData> Re_MidAtkSaveDataList = new List<LabSaveData>();
    public static List<LabSaveData> Re_MidHPSaveDataList = new List<LabSaveData>();
    public static CurrentLavSaveData currnetLabSaveData;

    public static int tic = 1000;

    public static LabBuffData labBuffData;
    public static int labBuffDataPercent = 10;
    private static int compensationTime;

    private static bool isBanchiCompensationTime;


    public static int stageData_WaveManager;
    public static bool isRepeatData_WaveManager;

    public static List<EquipData> equipItemData;
    public static List<SkillData> sData;
    public static List<EquipSkillData> equipSkillDatas;
    public static List<PetData> pData;
    public static List<EquipPetData> equipPetData;
    public static bool isAutoData;

    public static void GetAccumulateOfflineEarnings()
    {
        if(keyPrevAccumlateTime.ToString() == string.Empty)
        {
            keyPrevAccumlateTime.Append(DateTime.Now.ToString(datetimeString));
        }
        var prevData = DateTime.ParseExact(keyPrevAccumlateTime.ToString(), datetimeString, null);
        nowTime.Clear();
        nowTime.Append(DateTime.Now.ToString(datetimeString));
        var date = DateTime.ParseExact(nowTime.ToString(), datetimeString, null);

        TimeSpan timeDifference = date.Subtract(prevData);
        if (timeDifference.TotalMinutes < 10)
        {
            isBanchiCompensationTime = false;
            return;
        }
        isBanchiCompensationTime = true;
        if (timeDifference.TotalDays >= 1)
        {
            result = maxResult;
        }
        else
        {
            result = (timeDifference.Hours * 60 + timeDifference.Minutes);
        }

        //CurrencyManager.money1 += result * 100 / 100;
        compensationTime = result;
        //ChnageTime();
    }

    public static void ChnageTime()
    {
        keyPrevAccumlateTime.Clear();
        var rt = DateTime.Now.ToString(datetimeString);
        keyPrevAccumlateTime.Append(rt);
        if(result >0)
        {
            // 보상 흭득 랜덤하게 하는 코드 넣기
            result = 0;
        }
    }
    public static int GetCompensationTime()
    {
        return compensationTime;
    }
    public static void ResetCompensationTime()
    {
        ChnageTime();
        compensationTime = 0;
    }
    public static bool GetBanchiCompensationTime()
    {
        return isBanchiCompensationTime;
    }

    public static void EquipItemDataSetting()
    {
        var uiInvenvtory = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[1].GetComponent<InventoryWindow>();
        uiInvenvtory.Setting();

        var inventory = InventorySystem.Instance.inventory;

        var weaponInfo = uiInvenvtory.inventoryTypes[0].gameObject.GetComponent<WeaponType>().info.GetComponent<WeaponEquipInfoUi>();
        var armorInfo = uiInvenvtory.inventoryTypes[1].gameObject.GetComponent<ArmorType>().info.GetComponent<ArmorEquipInfoUi>();

        foreach (var item in equipItemData)
        {
            switch (item.itemType)
            {
                case ItemType.Weapon:
                    var weapon = inventory.weapons.Where(x => x.item.name == item.itemName).FirstOrDefault();
                    if (weapon != null)
                    {
                        weaponInfo.selectIndex = weapon.index;
                        weaponInfo.OnClickWeaponEquip();
                    }
                    break;

                case ItemType.Armor:
                    var armor = inventory.armors.Where(x => x.item.name == item.itemName).FirstOrDefault();
                    if (armor != null)
                    {
                        armorInfo.selectIndex = armor.index;
                        armorInfo.OnClickArmorEquip();
                    }
                    break;

                case ItemType.Ring:
                    var ring = inventory.customRings.Where(x => x.item.item.name == item.itemName).FirstOrDefault();
                    if (ring != null)
                        inventory.EquipItem(ring.item.index, item.itemType);
                    break;

                case ItemType.Symbol:
                    var symbol = inventory.customSymbols.Where(x => x.item.item.name == item.itemName).FirstOrDefault();
                    if (symbol != null)
                        inventory.EquipItem(symbol.item.index, item.itemType);
                    break;
            }
        }
    }
    public static void SkillDataSetting()
    {
        var skillInventory = InventorySystem.Instance.skillInventory;
        foreach (var skill in sData)
        {
            var skillData = skillInventory.skills.Where(x => x.skill.name == skill.skillName).FirstOrDefault();
            if (skillData != null)
            {
                skillData.upgradeLev = skill.skillLevel;
                skillData.acquire = skill.acquire;
                skillData.stock = skill.stock;
            }
        }
    }
    public static void EquipSkillDataSetting()
    {
        var uiSkill = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[2].GetComponent<SkillWindow>();
        uiSkill.Setting();
        var skillInventory = InventorySystem.Instance.skillInventory;
        foreach (var equip in equipSkillDatas)
        {
            var skill = skillInventory.skills.Where(x => x.skill.name == equip.skillName).FirstOrDefault();
            if (skill != null)
            {
                uiSkill.selectIndex = skill.skillIndex;
                uiSkill.equipMode = true;
                uiSkill.equipButtons[equip.equipIndex].GetComponent<NormalButton>().OnClickEquip(uiSkill);
                uiSkill.selectIndex = -1;
            }
        }
        UIManager.Instance.SkillButtonOn();
    }
    public static void PetDataSetting()
    {
        var uiPet = UIManager.Instance.windows[0].gameObject.GetComponent<InfoWindow>().inventorys[3].GetComponent<PetWindow>();
        uiPet.Setting();
        var petInventory = InventorySystem.Instance.petInventory;
        foreach (var pet in pData)
        {
            var petData = petInventory.pets.Where(x => x.pet.name == pet.petName).FirstOrDefault();
            if (petData != null)
            {
                petData.upgradeLev = pet.petLevel;
                petData.acquire = pet.acquire;
                petData.stock = pet.stock;
            }
        }
    }
    public static void EquipPetDataSetting()
    {
        var petInventory = InventorySystem.Instance.petInventory;
        foreach (var equip in equipPetData)
        {
            var pet = petInventory.pets.Where(x => x.pet.name == equip.petName).FirstOrDefault();
            if (pet != null)
            {
                petInventory.EquipPet(pet.petIndex, equip.equipIndex);
            }
        }
    }
}
