using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillInventory;

/// <summary>
/// 설정: skillcool |= SkillCool.skill001;
/// 해제: skillcool &= ~SkillCool.skill001;
/// 확인: if((skillcool & SkillCool.skill001) != 0)
/// </summary>
public enum SkillCool 
{
    None = 0,
    skill001 = 1 << 0,
    skill002 = 1 << 1,
    skill003 = 1 << 2,
    skill004 = 1 << 3,
    skill005 = 1 << 4,
    skill006 = 1 << 5,
    skill007 = 1 << 6,
    skill008 = 1 << 7,
    skill009 = 1 << 8,
    skill010 = 1 << 9,
    skill011 = 1 << 10,
    skill012 = 1 << 11,
    skill013 = 1 << 12,
    skill014 = 1 << 13,
    skill015 = 1 << 14,
    skill016 = 1 << 15,
    skill017 = 1 << 16,
    skill018 = 1 << 17,
}

public class SkillManager : MonoBehaviour
{
    private SkillCool skillcool = SkillCool.None;
    private SkillInventory inventory;
    private InventorySKill[] equipSkills;

    public List<SkillBase> skills = new List<SkillBase>(); //스킬 인벤토리의 skill index와 index 맞추기
    private GameObject player;

    public GameObject tripleShotShooterPrefab;
    public GameObject ArrowRainShooterPrefab;
    public GameObject TornadoShotPrefab;
    public GameObject poisonArrowPrefab;
    public GameObject blackCloudPrefab;

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

        inventory.EquipSkill(0, 0); //트리플샷, skill001
        inventory.EquipSkill(1, 1); //화살비, skill002
        inventory.EquipSkill(2, 2); //독화살, skill003
        inventory.EquipSkill(5, 3); //회오리샷, skill006
        inventory.EquipSkill(6, 4); //먹구름, skill007

        skills.Add(new TripleShot(inventory.skills[0], tripleShotShooterPrefab));
        skills.Add(new ArrowRain(inventory.skills[1], ArrowRainShooterPrefab, enemyLayer, castZone));
        skills.Add(new PoisonArrowShot(inventory.skills[2], poisonArrowPrefab));
        skills.Add(new TornatoShot(inventory.skills[5], TornadoShotPrefab));

    }

    private void Start()
    {
        //skills.Add(new TripleShot(inventory.skills[0], shooterPrefab));
    }


    private void Update()
    {
        if(player.GetComponent<PlayerController>().currentStates == States.Move)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill1();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            UseSkill2();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSkill6();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UseSkill3();
        }
    }

    public void UseSkill1()
    {
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & SkillCool.skill001) != 0) //쿨 도는 중
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            FindeSkillBase(0).UseSkill(player);
            StartCoroutine(Skill1CoolDown(inventory.skills[0].skill.Skill_Cool));
        }
    }

    public void UseSkill2()
    {
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & SkillCool.skill002) != 0) //쿨 도는 중
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            FindeSkillBase(1).UseSkill(player);
            StartCoroutine(Skill2CoolDown(inventory.skills[1].skill.Skill_Cool));
        }
    }

    public void UseSkill3()
    {
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & SkillCool.skill003) != 0) //쿨 도는 중
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            FindeSkillBase(2).UseSkill(player);
            StartCoroutine(Skill3CoolDown(inventory.skills[2].skill.Skill_Cool));
        }
    }

    public void UseSkill6()
    {
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & SkillCool.skill006) != 0) //쿨 도는 중
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            FindeSkillBase(3).UseSkill(player);
            StartCoroutine(Skill6CoolDown(inventory.skills[5].skill.Skill_Cool));
        }
    }

    private SkillBase FindeSkillBase(int skillIndex)
    {
        return skills[skillIndex];
    }

    IEnumerator Skill1CoolDown(float cooldown)
    {
        skillcool |= SkillCool.skill001;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~SkillCool.skill001;
    }

    IEnumerator Skill2CoolDown(float cooldown)
    {
        skillcool |= SkillCool.skill002;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~SkillCool.skill002;
    }

    IEnumerator Skill3CoolDown(float cooldown)
    {
        skillcool |= SkillCool.skill003;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~SkillCool.skill003;
    }

    IEnumerator Skill6CoolDown(float cooldown)
    {
        skillcool |= SkillCool.skill006;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~SkillCool.skill006;
    }
}
