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

    public GameObject tripleShotShooterPrefab;
    public GameObject ArrowRainShooterPrefab;

    public LayerMask enemyLayer;

    public GameObject castZone;
    /// <summary>
    /// 세이브로드
    /// </summary>

    private void Awake()
    {
        inventory = InventorySystem.Instance.skillInventory;
        equipSkills = inventory.equipSkills;
        player = GameObject.FindGameObjectWithTag("Player");

        skills.Add(new TripleShot(inventory.skills[0], tripleShotShooterPrefab));
        skills.Add(new ArrowRain(inventory.skills[1], ArrowRainShooterPrefab, enemyLayer, castZone));

        inventory.EquipSkill(0, 0); //트리플샷
        inventory.EquipSkill(1, 1); //화살비
        inventory.EquipSkill(2, 2); //독화살
        inventory.EquipSkill(5, 3); //회오리샷
        inventory.EquipSkill(6, 4); //먹구름
    }

    private void Start()
    {
        //skills.Add(new TripleShot(inventory.skills[0], shooterPrefab));
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    inventory.EquipSkill(0, 0);
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (player == null)
            {
                Debug.Log("ERR: Player is Null");
                return;
            }
            FindeSkillBase(0).UseSkill(player);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (player == null)
            {
                Debug.Log("ERR: Player is Null");
                return;
            }
            FindeSkillBase(1).UseSkill(player);
        }
    }

    private SkillBase FindeSkillBase(int skillIndex)
    {
        return skills[skillIndex];
    }

}
