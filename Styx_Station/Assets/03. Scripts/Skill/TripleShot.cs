using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TripleShot : SkillBase
{
    private string piercingBowName = "PlayerPiercingArrow";
    private SkillInventory.InventorySKill tripleShot;
    private float speed;

    private int fireCount = 3;

    private float fireBet = 0.2f;
    private float lastfireTime = 0;

    private WaitForSeconds waitForFireBet;

    public TripleShot(SkillInventory.InventorySKill tripleShot)
    {
        this.tripleShot = tripleShot;
        speed = 1 / this.tripleShot.skill.Skill_Speed; //1유닛 도달에 걸리는 시간 -> 초당 이동속도 변환

        waitForFireBet = new WaitForSeconds(fireBet);
    }

    public override void UseSkill(GameObject attacker, GameObject defender)
    {
        var rects = attacker.GetComponentsInChildren<RectTransform>();
        if (rects[1] == null)
        {
            Debug.Log("ERR: No FirePoint");
            return;
        }
        var startPos = rects[1].transform.position;

        FireArrow(attacker, startPos);
    }

    public void FireArrow(GameObject attacker, Vector2 Pos)
    {
        var arrow = ObjectPoolManager.instance.GetGo(piercingBowName);
        if (arrow == null)
        {
            Debug.Log("ERR: arrow is null");
            return;
        }
        arrow.transform.position = Pos;

        var piercingArrow = arrow.GetComponent<PiercingArrow>();
        if (!piercingArrow.CheckOnCollided())
        {
            piercingArrow.OnCollided += OnBowCollided;
        }
        piercingArrow.Fire(attacker, speed);

        fireCount--;
    }

    private void OnBowCollided(GameObject attacker, GameObject defender)
    {
        //애니메이션 시작 -> 실제 타격 사이에 거리와 방향이 변경될 수 있기 때문에 체크 필요
        if (defender == null) //남아있지만 죽은 상태일 수도 있음
        {
            return;
        }
        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target);

        //Iattackable을 상속 받은 것이 여러개 일 수도 있기 때문에 모두 호출
        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}
