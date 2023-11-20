using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateBase //추상 클래스 상태들의 기본이 됨.
{
    abstract public void Enter();
    abstract public void Update();
    abstract public void Exit();
}
