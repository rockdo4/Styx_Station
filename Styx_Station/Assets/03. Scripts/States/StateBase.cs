using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateBase //�߻� Ŭ���� ���µ��� �⺻�� ��.
{
    abstract public void Enter();
    abstract public void Update();
    abstract public void Exit();
}
