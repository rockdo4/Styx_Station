using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TripleShot : SkillBase
{
    private string piercingBowName = "PlayerPiercingArrow";
    private SkillInventory.InventorySKill tripleShot;
    private float speed;

    public TripleShot(SkillInventory.InventorySKill tripleShot)
    {
        this.tripleShot = tripleShot;
        speed = 1 / tripleShot.skill.Skill_Speed; //1���� ���޿� �ɸ��� �ð� -> �ʴ� �̵��ӵ� ��ȯ
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
        var arrow = ObjectPoolManager.instance.GetGo(piercingBowName);
        arrow.transform.position = startPos;

        var piercingArrow = arrow.GetComponent<PiercingArrow>();
        if (!piercingArrow.CheckOnCollided())
        {
            piercingArrow.OnCollided += OnBowCollided;
        }
        piercingArrow.Fire(attacker, speed);
    }

    private void OnBowCollided(GameObject attacker, GameObject defender)
    {
        //�ִϸ��̼� ���� -> ���� Ÿ�� ���̿� �Ÿ��� ������ ����� �� �ֱ� ������ üũ �ʿ�
        if (defender == null) //���������� ���� ������ ���� ����
        {
            return;
        }
        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target);

        //Iattackable�� ��� ���� ���� ������ �� ���� �ֱ� ������ ��� ȣ��
        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}
