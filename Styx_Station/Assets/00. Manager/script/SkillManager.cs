using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillInventory;

public class SkillManager : MonoBehaviour
{
    private SkillInventory inventory;
    private InventorySKill[] equipSkills;

    public List<SkillBase> skills = new List<SkillBase>(); //��ų �κ��丮�� skill index�� index ���߱�
    private GameObject player;

    public GameObject shooterPrefab;


    /// <summary>
    /// ���̺�ε�
    /// </summary>
    private SaveLoad saveLoad;

    private void Awake()
    {
        inventory = InventorySystem.Instance.skillInventory;
        equipSkills = inventory.equipSkills;
        player = GameObject.FindGameObjectWithTag("Player");
        saveLoad = GetComponent<SaveLoad>();

        skills.Add(new TripleShot(inventory.skills[0], shooterPrefab));


        saveLoad.Load();
    }

    private void Start()
    {
        //skills.Add(new TripleShot(inventory.skills[0], shooterPrefab));
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
