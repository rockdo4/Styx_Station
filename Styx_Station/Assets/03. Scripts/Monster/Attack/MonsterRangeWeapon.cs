using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/MonsterRangeWeapon")]
public class MonsterRangeWeapon : AttackDefinition
{
    public MonsterArrow bowPrefab;
    public float speed; //발사체 이동 속도
    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        var rects = attacker.GetComponentsInChildren<RectTransform>();
        if (rects[1] == null)
        {
            Debug.Log("ERR: No BezierPoint");
            return;
        }
        if (rects[3] == null)
        {
            Debug.Log("ERR: No FirePoint");
            return;
        }

        var startPos = rects[3].transform.position;
        var targetPos = defender.transform.position;
        targetPos.y = startPos.y;

        var direction = Vector2.left;
        //var bow = Instantiate(bowPrefab, startPos, Quaternion.identity);
        var bow = ObjectPoolManager.instance.GetGo(bowPrefab.name);
        bow.transform.position = startPos;
        var projectileBow = bow.GetComponent<MonsterArrow>();
        if (!projectileBow.CheckOnCollided())
        {
            projectileBow.OnCollided += OnBowCollided;
        }
        projectileBow.Fire(attacker, speed, targetPos, rects[1].transform.position);
    }

    private void OnBowCollided(GameObject attacker, GameObject defender)
    {
        //애니메이션 시작 -> 실제 타격 사이에 거리와 방향이 변경될 수 있기 때문에 체크 필요
        if (defender == null) //남아있지만 죽은 상태일 수도 있음
        {
            return;
        }

        //거리, 방향은 검사 필요 x
        var characterStats = attacker.GetComponent<MonsterStats>();
        var targetStats = defender.GetComponent<ResultPlayerStats>();
        Attack attack = CreateAttackToPlayer(characterStats, targetStats);

        //Iattackable을 상속 받은 것이 여러개 일 수도 있기 때문에 모두 호출
        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            if (defender.GetComponent<PlayerController>().currentStates == States.Die)
            {
                return;
            }
            attackable.OnAttack(attacker, attack);
        }
    }
}
