using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillInventory;

public class SkillManager : MonoBehaviour
{
    private SkillInventory inventory;
    private InventorySKill[] equipSkills;

    public List<SkillBase> skills = new List<SkillBase>(); //스킬 인벤토리의 skill index와 index 맞추기
    private GameObject player;


    /// <summary>
    /// 세이브로드
    /// </summary>
    private SaveLoad saveLoad;

    private void Awake()
    {
        inventory = InventorySystem.Instance.skillInventory;
        equipSkills = inventory.equipSkills;
        player = GameObject.FindGameObjectWithTag("Player");
        saveLoad = GetComponent<SaveLoad>();

        saveLoad.Load();
    }

    private void Start()
    {
        skills.Add(new TripleShot(inventory.skills[0]));
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            if(player == null)
            {
                Debug.Log("ERR: Player is Null");
                return;
            }
            FindeSkillBase(0).UseSkill(player, player);
        }
    }

    private SkillBase FindeSkillBase(int skillIndex)
    {
        return skills[skillIndex];
    }

}
