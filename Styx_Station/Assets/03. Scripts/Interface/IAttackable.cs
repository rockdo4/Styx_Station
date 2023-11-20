using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
    //공격당할 때 할 일을 하나씩 쪼개서 구현. 모든 일들이 상속 받아야함
{
    void OnAttack(GameObject attacker, Attack attack);
}
