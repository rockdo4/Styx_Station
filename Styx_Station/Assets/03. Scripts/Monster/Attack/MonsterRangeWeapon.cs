using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/MonsterRangeWeapon")]
public class MonsterRangeWeapon : AttackDefinition
{
    public MonsterArrow bowPrefab;
    public float speed; //�߻�ü �̵� �ӵ�
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
        //�ִϸ��̼� ���� -> ���� Ÿ�� ���̿� �Ÿ��� ������ ����� �� �ֱ� ������ üũ �ʿ�
        if (defender == null) //���������� ���� ������ ���� ����
        {
            return;
        }

        //�Ÿ�, ������ �˻� �ʿ� x
        var characterStats = attacker.GetComponent<MonsterStats>();
        var targetStats = defender.GetComponent<ResultPlayerStats>();
        Attack attack = CreateAttackToPlayer(characterStats, targetStats);

        //Iattackable�� ��� ���� ���� ������ �� ���� �ֱ� ������ ��� ȣ��
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
