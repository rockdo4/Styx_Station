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

    public GameObject tripleShotShooterPrefab;
    public GameObject ArrowRainShooterPrefab;

    public LayerMask enemyLayer;

    public GameObject castZone;
    /// <summary>
    /// ���̺�ε�
    /// </summary>

    private void Awake()
    {
        inventory = InventorySystem.Instance.skillInventory;
        equipSkills = inventory.equipSkills;
        player = GameObject.FindGameObjectWithTag("Player");

        skills.Add(new TripleShot(inventory.skills[0], tripleShotShooterPrefab));
        skills.Add(new ArrowRain(inventory.skills[1], ArrowRainShooterPrefab, enemyLayer, castZone));

        inventory.EquipSkill(0, 0); //Ʈ���ü�
        inventory.EquipSkill(1, 1); //ȭ���
        inventory.EquipSkill(2, 2); //��ȭ��
        inventory.EquipSkill(5, 3); //ȸ������
        inventory.EquipSkill(6, 4); //�Ա���
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
