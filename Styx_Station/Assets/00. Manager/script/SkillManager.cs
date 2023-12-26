using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillInventory;
using UnityEngine.UI;
using System;
using System.Linq;

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
    public GameObject meteorPrefab;
    public GameObject energyVoltPerfab;

    public LayerMask enemyLayer;

    public GameObject castZone;

    private delegate void AutoSkillDelegate(Slider cool);
    //private AutoSkillDelegate[] useSkillArray = new AutoSkillDelegate[6];
    //private Queue<AutoSkillDelegate> autoSkillQueue = new Queue<AutoSkillDelegate>();
    private Queue<GameObject> skillbutton = new Queue<GameObject>();
    public bool isAuto { get; private set; } = false;

    private SkillWindow skillWindow;
    /// <summary>
    /// 세이브로드
    /// </summary>

    private void Awake()
    {
        if(inventory ==null)
            inventory = InventorySystem.Instance.skillInventory;
        if(equipSkills ==null)
            equipSkills = inventory.equipSkills;
        SetEquipSkill();
        player = GameObject.FindGameObjectWithTag("Player");

        skills.Add(new TripleShot(inventory.skills[0], tripleShotShooterPrefab)); //트리플샷1
        skills.Add(new ArrowRain(inventory.skills[1], ArrowRainShooterPrefab, enemyLayer, castZone)); //화살비
        skills.Add(new PoisonArrowShot(inventory.skills[2], poisonArrowPrefab)); //독화살
        skills.Add(new PassiveSkillBase()); //임시(생기증가)
        skills.Add(new TripleShot(inventory.skills[4], tripleShotShooterPrefab)); //트리플샷2
        skills.Add(new TornatoShot(inventory.skills[5], TornadoShotPrefab)); //회오리바람
        skills.Add(new BlackCloud(inventory.skills[6], blackCloudPrefab)); //먹구름
        skills.Add(new PassiveSkillBase()); //임시(공격력증가)
        skills.Add(new TripleShot(inventory.skills[8], tripleShotShooterPrefab)); //트리플샷3
        skills.Add(new Meteor(inventory.skills[9], meteorPrefab)); //메테오
        skills.Add(new EnergyVolt(inventory.skills[10], energyVoltPerfab)); //에너지볼트
        skills.Add(new PassiveSkillBase()); //임시(공격력 증가2)
        skills.Add(new TripleShot(inventory.skills[11], tripleShotShooterPrefab)); //트리플샷3


        skillWindow = UIManager.Instance.skill.GetComponent<SkillWindow>();

        for(int i = 0; i < 6; i++)
        {
            skillbutton.Enqueue(skillWindow.slotButtons[i]);
        }
        SetEquipSkillCool();
    }
    private void Start()
    {
        //skills.Add(new TripleShot(inventory.skills[0], shooterPrefab));
    }

    public void SetIsAuto(bool isA)
    {
        isAuto = isA;
        Debug.Log($"is Auto { isAuto }");
    }

    private void Update()
    {
        if(isAuto)
        {
            if(!WaveManager.Instance.isWaveInProgress)
            {
                return;
            }
            SortSkillButton();
            while (skillbutton.Count > 0)
            {
                var button = skillbutton.Dequeue();
                if (button.GetComponentInChildren<NormalButton>().skillIndex < 0)
                {
                    return;
                }
                else
                {
                    button.GetComponentInChildren<NormalButton>().AutoSkillActive(skillWindow);
                }
            }

        }
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
        if (inventory == null)
            inventory = InventorySystem.Instance.skillInventory;
        if(equipSkills == null)
            equipSkills = inventory.equipSkills;
        equipSkills[index] = inventory.equipSkills[index];
        equipSkillFlags[index] = skillCools[equipSkills[index].skillIndex + 1];
    }

    public void SetDequipSkillByIndex(int index)
    {
        equipSkills[index] = null;
    }

    private bool CheckSkillCool(int equipIndex) //true: 쿨x false: 쿨O
    {
        return ((skillcool & equipSkillFlags[equipIndex]) == 0);
    }

    public void UseSkill()
    {
        //skills[9].UseSkill(player);
        skills[10].UseSkill(player);
    }
    public void UseSkill1(Slider cool)
    {
        if(!WaveManager.Instance.isWaveInProgress)
        {
            return;
        }
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
            Debug.Log($"{equipSkills[0].skillIndex} 스킬 사용");
            cool.value = 1;
            FindeSkillBase(equipSkills[0].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[0];
            StartCoroutine(Skill1CoolDown(equipSkills[0].skill.Skill_Cool, equipSkillFlags[0], cool));
        }
    }

    public void UseSkill2(Slider cool)
    {
        if (!WaveManager.Instance.isWaveInProgress)
        {
            return;
        }
        if (equipSkills[1] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & equipSkillFlags[1]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            Debug.Log($"{equipSkills[1].skillIndex} 스킬 사용");
            cool.value = 1;
            FindeSkillBase(equipSkills[1].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[1];
            StartCoroutine(Skill2CoolDown(equipSkills[1].skill.Skill_Cool, equipSkillFlags[1], cool));
        }
    }

    public void UseSkill3(Slider cool)
    {
        if (!WaveManager.Instance.isWaveInProgress)
        {
            return;
        }
        if (equipSkills[2] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & equipSkillFlags[2]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            Debug.Log($"{equipSkills[2].skillIndex} 스킬 사용");
            cool.value = 1;
            FindeSkillBase(equipSkills[2].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[2];
            StartCoroutine(Skill3CoolDown(equipSkills[2].skill.Skill_Cool, equipSkillFlags[2], cool));
        }
    }

    public void UseSkill4(Slider cool)
    {
        if (!WaveManager.Instance.isWaveInProgress)
        {
            return;
        }
        if (equipSkills[3] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & equipSkillFlags[3]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            Debug.Log($"{equipSkills[3].skillIndex} 스킬 사용");
            cool.value = 1;
            FindeSkillBase(equipSkills[3].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[3];
            StartCoroutine(Skill4CoolDown(equipSkills[3].skill.Skill_Cool, equipSkillFlags[3], cool));
        }
    }

    public void UseSkill5(Slider cool)
    {
        if (!WaveManager.Instance.isWaveInProgress)
        {
            return;
        }
        if (equipSkills[4] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & equipSkillFlags[4]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            Debug.Log($"{equipSkills[4].skillIndex} 스킬 사용");
            cool.value = 1;
            FindeSkillBase(equipSkills[4].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[4];
            StartCoroutine(Skill5CoolDown(equipSkills[4].skill.Skill_Cool, equipSkillFlags[4], cool));
        }
    }

    public void UseSkill6(Slider cool)
    {
        if (!WaveManager.Instance.isWaveInProgress)
        {
            return;
        }
        if (equipSkills[5] == null)
        {
            return;
        }
        if (player == null)
        {
            Debug.Log("ERR: Player is Null");
            return;
        }
        if ((skillcool & equipSkillFlags[5]) != 0)
        {
            Debug.Log("스킬 쿨 대기 중");
        }
        else
        {
            Debug.Log($"{equipSkills[5].skillIndex} 스킬 사용");       
            cool.value = 1;
            FindeSkillBase(equipSkills[5].skillIndex).UseSkill(player);
            skillcool |= equipSkillFlags[5];
            StartCoroutine(Skill6CoolDown(equipSkills[5].skill.Skill_Cool, equipSkillFlags[5], cool));
        }
    }
    
    private SkillBase FindeSkillBase(int skillIndex)
    {
        return skills[skillIndex];
    }
    IEnumerator Skill1CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

        skillbutton.Enqueue(skillWindow.slotButtons[0]);
    }

    IEnumerator Skill2CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

        skillbutton.Enqueue(skillWindow.slotButtons[1]);
    }

    IEnumerator Skill3CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

        skillbutton.Enqueue(skillWindow.slotButtons[2]);
    }

    IEnumerator Skill4CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

        skillbutton.Enqueue(skillWindow.slotButtons[3]);
    }
    IEnumerator Skill5CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

        skillbutton.Enqueue(skillWindow.slotButtons[4]);
    }
    IEnumerator Skill6CoolDown(float cooldown, SkillCool cool, Slider coolSl)
    {
        yield return new WaitForSeconds(cooldown);
        skillcool &= ~cool;
        coolSl.value = 0;

        skillbutton.Enqueue(skillWindow.slotButtons[5]);
    }

    private void SortSkillButton()
    {
        List<GameObject> sortList = new List<GameObject>();

        sortList = skillbutton.ToList();
        sortList.Sort(
            (x, y) => x.GetComponentInChildren<NormalButton>().equipIndex.CompareTo(y.GetComponentInChildren<NormalButton>().equipIndex));

        skillbutton = new Queue<GameObject>(sortList);
    }

}
