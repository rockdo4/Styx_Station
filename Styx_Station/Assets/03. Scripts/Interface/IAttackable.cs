using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
    //���ݴ��� �� �� ���� �ϳ��� �ɰ��� ����. ��� �ϵ��� ��� �޾ƾ���
{
    void OnAttack(GameObject attacker, Attack attack);
}
