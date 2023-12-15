using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillInventory;
using UnityEngine.UI;

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

public class SkillManager : Singleton<SkillManager>
{
    private SkillCool[] skillCools = new SkillCool[] 
    { 
        SkillCool.None,
        SkillCool.skill001,
        SkillCool.skill002,
        SkillCool.skill003,
        SkillCool.skill004,
        SkillCool.skill005,
        SkillCool.skill006,
        SkillCool.skill007,
        SkillCool.skill008,
        SkillCool.skill009,
        SkillCool.skill010,
        SkillCool.skill011,
        SkillCool.skill012,
        SkillCool.skill013,
        SkillCool.skill014,
        SkillCool.skill015,
        SkillCool.skill016,
        SkillCool.skill017,
        SkillCool.skill018,
    };

    private SkillCool skillcool = SkillCool.None;
    private SkillInventory inventory;
    private InventorySKill[] equipSkills;
    private SkillCool[] equipSkillFlags = new SkillCool[6];
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
        //equipSkills = inventory.equipSkills;
        SetEquipSkill();
        player = GameObject.FindGameObjectWithTag("Player");

        //inventory.EquipSkill(0, 0); //트리플샷, skill001
        //inventory.EquipSkill(1, 1); //화살비, skill002
        //inventory.EquipSkill(2, 2); //독화살, skill003
        //inventory.EquipSkill(5, 3); //회오리샷, skill006
        //inventory.EquipSkill(6, 4); //먹구름, skill007

        skills.Add(new TripleShot(inventory.skills[0], tripleShotShooterPrefab));
        skills.Add(new ArrowRain(inventory.skills[1], ArrowRainShooterPrefab, enemyLayer, castZone));
        skills.Add(new PoisonArrowShot(inventory.skills[2], poisonArrowPrefab));
        skills.Add(new PoisonArrowShot(inventory.skills[2], poisonArrowPrefab)); //임시
        skills.Add(new PoisonArrowShot(inventory.skills[2], poisonArrowPrefab)); //임시
        skills.Add(new TornatoShot(inventory.skills[5], TornadoShotPrefab));
        skills.Add(new BlackCloud(inventory.skills[6], blackCloudPrefab));

        //SetEquipSkillCool();
    }

    private void Start()
    {
        //skills.Add(new TripleShot(inventory.skills[0], shooterPrefab));
    }

    private void SetEquipSkillCool()
    {
        for (int i = 0; i < equipSkillFlags.Length - 1; i++)
        {
       
            if (equipSkills[i] == null)
            {
                continue;
            }
            equipSkillFlags[i] = skillCools[equipSkills[i].skillIndex + 1];
        }
    }

    public void SetEquipSkill()
    {
        if(inventory.equipSkills == null)
        {
            return;
        }
        equipSkills = inventory.equipSkills;
        SetEquipSkillCool();
    }

    public void SetEquipSkillByIndex(int index)
    {
        equipSkills[index] = inventory.equipSkills[index];
        equipSkillFlags[index] = skillCools[equipSkills[index].skillIndex + 1];
    }

    public void SetDequipSkillByIndex(int index)
    {
        equipSkills[index] = null;
    }

    private void Update()
    {
        if(player.GetComponent<PlayerController>().currentStates == States.Move)
        {
            return;
        }
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    UseSkill1();
        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    UseSkill2();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    UseSkill3();
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    UseSkill4();
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    UseSkill5();
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    UseSkill6();
        //}
    }

    public void UseSkill1(Slider cool)
    {
        if (equipSkills[0] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & equipSkillFlags[0]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            cool.value = 1;
            FindeSkillBase(equipSkills[0].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[0];
            StartCoroutine(Skill1CoolDown(equipSkills[0].skill.Skill_Cool, equipSkillFlags[0], cool));
            //skillcool &= ~equipSkillFlags[0];
            //StartCoroutine(Skill1CoolDown(inventory.skills[0].skill.Skill_Cool));
        }
    }

    public void UseSkill2(Slider cool)
    {
        if (equipSkills[1] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        //if ((skillcool & SkillCool.skill002) != 0) //쿨 도는 중
        //{
        //    Debug.Log("스킬 쿨 대기 중");
        //}
        //else
        //{
        //    FindeSkillBase(1).UseSkill(player);
        //    StartCoroutine(Skill2CoolDown(inventory.skills[1].skill.Skill_Cool));
        //}
        if ((skillcool & equipSkillFlags[1]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            cool.value = 1;
            FindeSkillBase(equipSkills[1].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[1];
            StartCoroutine(Skill2CoolDown(equipSkills[1].skill.Skill_Cool, equipSkillFlags[1], cool));
            //skillcool &= ~equipSkillFlags[1];
            //StartCoroutine(Skill1CoolDown(inventory.skills[0].skill.Skill_Cool));
        }
    }

    public void UseSkill3(Slider cool)
    {
        if (equipSkills[2] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        //if ((skillcool & SkillCool.skill003) != 0) //쿨 도는 중
        //{
        //    Debug.Log("스킬 쿨 대기 중");
        //}
        //else
        //{
        //    FindeSkillBase(2).UseSkill(player);
        //    StartCoroutine(Skill3CoolDown(inventory.skills[2].skill.Skill_Cool));
        //}
        if ((skillcool & equipSkillFlags[2]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            cool.value = 1;
            FindeSkillBase(equipSkills[2].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[2];
            StartCoroutine(Skill3CoolDown(equipSkills[2].skill.Skill_Cool, equipSkillFlags[2], cool));
            //skillcool &= ~equipSkillFlags[2];
            //StartCoroutine(Skill1CoolDown(inventory.skills[0].skill.Skill_Cool));
        }
    }

    public void UseSkill4(Slider cool)
    {
        if (equipSkills[3] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        //if ((skillcool & SkillCool.skill006) != 0) //쿨 도는 중
        //{
        //    Debug.Log("스킬 쿨 대기 중");
        //}
        //else
        //{
        //    FindeSkillBase(3).UseSkill(player);
        //    StartCoroutine(Skill6CoolDown(inventory.skills[5].skill.Skill_Cool));
        //}
        if ((skillcool & equipSkillFlags[3]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            cool.value = 1;
            FindeSkillBase(equipSkills[3].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[3];
            StartCoroutine(Skill4CoolDown(equipSkills[3].skill.Skill_Cool, equipSkillFlags[3], cool));
            //skillcool &= ~equipSkillFlags[3];
            //StartCoroutine(Skill1CoolDown(inventory.skills[0].skill.Skill_Cool));
        }
    }

    public void UseSkill5(Slider cool)
    {
        if (equipSkills[4] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        //if ((skillcool & SkillCool.skill006) != 0) //쿨 도는 중
        //{
        //    Debug.Log("스킬 쿨 대기 중");
        //}
        //else
        //{
        //    FindeSkillBase(3).UseSkill(player);
        //    StartCoroutine(Skill6CoolDown(inventory.skills[5].skill.Skill_Cool));
        //}
        if ((skillcool & equipSkillFlags[4]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            cool.value = 1;
            FindeSkillBase(equipSkills[4].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[4];
            StartCoroutine(Skill5CoolDown(equipSkills[4].skill.Skill_Cool, equipSkillFlags[4], cool));
            //skillcool &= ~equipSkillFlags[4];
            //StartCoroutine(Skill1CoolDown(inventory.skills[0].skill.Skill_Cool));
        }
    }

    public void UseSkill6(Slider cool)
    {
        if (equipSkills[5] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        //if ((skillcool & SkillCool.skill006) != 0) //쿨 도는 중
        //{
        //    Debug.Log("스킬 쿨 대기 중");
        //}
        //else
        //{
        //    FindeSkillBase(3).UseSkill(player);
        //    StartCoroutine(Skill6CoolDown(inventory.skills[5].skill.Skill_Cool));
        //}
        if ((skillcool & equipSkillFlags[5]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            cool.value = 1;
            FindeSkillBase(equipSkills[5].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[5];
            StartCoroutine(Skill6CoolDown(equipSkills[5].skill.Skill_Cool, equipSkillFlags[5], cool));
            //skillcool &= ~equipSkillFlags[5];
            //StartCoroutine(Skill1CoolDown(inventory.skills[0].skill.Skill_Cool));
        }
    }
    
    private SkillBase FindeSkillBase(int skillIndex)
    {
        return skills[skillIndex];
    }
    IEnumerator Skill1CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        //skillcool |= SkillCool.skill001;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;
    }

    IEnumerator Skill2CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        //skillcool |= SkillCool.skill002;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

    }

    IEnumerator Skill3CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        //skillcool |= SkillCool.skill003;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

    }

    IEnumerator Skill4CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        //skillcool |= SkillCool.skill006;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

    }
    IEnumerator Skill5CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        //skillcool |= SkillCool.skill006;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

    }
    IEnumerator Skill6CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        //skillcool |= SkillCool.skill006;
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

    }
}
