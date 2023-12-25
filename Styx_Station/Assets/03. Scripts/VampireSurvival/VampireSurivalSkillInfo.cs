using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VampireSurivalSkillInfo;

public class VampireSurivalSkillInfo : MonoBehaviour
{


    private VampireSkillInfoDataType skillData;

    public VamprieSurivalPlayerController player;

    public delegate void SkillEvent();
    public static event SkillEvent OnTripleArrowShot;

    private void Awake()
    {
        OnTripleArrowShot = TripleArrowShotAction;
        switch (skillData.currentSkillType)
        {
            case VampireSkillType.TripleArrowShot:
                skillData.skillEvent = OnTripleArrowShot;
                break;
        }
    }



    public void TripleArrowShotAction()
    {
        var arrow = ObjectPoolManager.instance.GetGo("VampireSurivalArrow");
        var collider = Physics2D.OverlapCircle(arrow.transform.position, skillData.range);
        if (collider != null && collider.CompareTag("VampireEnemy"))
        {
            arrow.transform.position = transform.position;
            Vector3 direction = collider.transform.position - arrow.transform.position;
            direction.Normalize();
            arrow.GetComponent<VamprieSurivalAttackArrow>().LineAttackRange(direction);

            for (int i = 1; i < 3; ++i)
            {
                var arrowLoop = ObjectPoolManager.instance.GetGo("VampireSurivalArrow");
                arrowLoop.transform.position = transform.position;
                float subAngle = 0f;
                if (i == 1)
                {
                    subAngle += 30f;
                }
                else if (i == 2)
                {
                    subAngle -= 30f;
                }
                arrowLoop.transform.rotation = Quaternion.Euler(0f, 0f, subAngle);
                Vector2 directionLoop = new Vector2(Mathf.Cos(subAngle * Mathf.Deg2Rad), Mathf.Sin(subAngle * Mathf.Deg2Rad));
                arrowLoop.transform.position += (Vector3)directionLoop;
                arrowLoop.GetComponent<VamprieSurivalAttackArrow>().LineAttackRange(directionLoop);
            }
        }
        else
        {
            var set = arrow.GetComponent<VamprieSurivalPlayerAttackType>();
            set.Setting(skillData.damage, skillData.speed, skillData.aliveTime);
            arrow.transform.position = transform.position;
            float randomAngle = UnityEngine.Random.Range(0f, 360f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);
            Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
            arrow.GetComponent<VamprieSurivalAttackArrow>().LineAttackRange(direction);
            for (int i = 1; i < 3; ++i)
            {
                var arrowLoop = ObjectPoolManager.instance.GetGo("VampireSurivalArrow");
                arrowLoop.transform.position = transform.position;
                float subAngle = 0f;
                if (i == 1)
                {
                    subAngle = randomAngle + 30f;
                }
                else if (i == 2)
                {
                    subAngle = randomAngle - 30f;
                }
                arrowLoop.transform.rotation = Quaternion.Euler(0f, 0f, subAngle);
                Vector2 directionLoop = new Vector2(Mathf.Cos(subAngle * Mathf.Deg2Rad), Mathf.Sin(subAngle * Mathf.Deg2Rad));
                arrowLoop.transform.position += (Vector3)directionLoop;
                arrowLoop.GetComponent<VamprieSurivalAttackArrow>().LineAttackRange(directionLoop);
            }
        }
    }
    public void ArrowRain()
    {
        var copy = skillData.particle;
        var pos =player.transform.position;
        pos.y += 0.5f;
        copy.transform.position = pos;
        copy.Play();
    }
}
[System.Serializable]
public struct VampireSkillInfoDataType
{
    public string skillName;
    public Sprite skillImage;
    public bool isUnLock;
    public string skillDataStringKey;
    public VampireSkillType currentSkillType;
    public VampirePlayerDebuffSkill currentDebuffSkill;
    public Tier skillTier;
    public int level;
    public float damage;
    public float range;
    public float speed;
    [HideInInspector]public float timer;
    public ParticleSystem particle;
    public float aliveTime;
    public float coolTime;
    public float attackTimeDelay;
    public float debuffTime;
    public float debuffDamage;
    public float levelUpBuffDamage;
    public float levelUpBuffSpeed;
    public float levelUpBuffRange;
    public float levelUpBuffTimer;
    public float levelUpBuffDebuffDamage;

    [HideInInspector]
    public SkillEvent skillEvent;
}