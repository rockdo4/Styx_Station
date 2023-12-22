using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSurivalSkillInfo : MonoBehaviour
{

    private delegate void SkillAction();
    private bool isUnLock;

    private VampireSkillInfoDataType skillData;

    private VamprieSurivalPlayerController player;
    private Action StartSkill;
    private SkillAction skillAction;

    private void Awake()
    {
        switch (skillData.currentSkillType)
        {
            case VampireSkillType.TripleArrowShot:
                StartSkill  = TripleArrowShotAction;
                break;
        }
    }

    public void Update()
    {
        if (isUnLock && player != null)
        {
            skillData.timer += Time.deltaTime;
            if(skillData.timer >= skillData.coolTime)
            {
                skillData.timer = 0f;
                StartSkill.Invoke();
            }
        }
    }
    /*
    public int damage;
    protected Vector2 direction;
    public float range;
    public float speed;
    public float aliveTime;
    [HideInInspector]
    public float nowTime;
    public float coolTime;
    public float timer;
    public VamprieSurivalAttackType attackType;
    public abstract void LineAttackRange(Vector2 position);
     */

    public void SkillUnLock(VamprieSurivalPlayerController player)
    {
        this.player = player;
        isUnLock = true;
    }

    public void TripleArrowShotAction()
    {
        var arrow = ObjectPoolManager.instance.GetGo("VampireSurivalArrow");
        arrow.transform.position = transform.position;

        var set = arrow.GetComponent<VamprieSurivalPlayerAttackType>();
        set.Setting(skillData.damage, skillData.speed, skillData.aliveTime);
        //var range = arrow.GetComponent<VamprieSurivalPlayerAttackType>().range;


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
[System.Serializable]
public struct VampireSkillInfoDataType
{
    public string skillName;
    public Sprite skillImage;
    public string skillDataStringKey;
    public VampireSkillType currentSkillType;
    public Tier skillTier;
    public int level;
    public int damage;
    public float speed;
    [HideInInspector]public float timer;
    public float aliveTime;
    public float coolTime;
}