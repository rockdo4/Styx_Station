using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : MonoBehaviour
{


    private StateManager playerStateManager = new StateManager();
    private List<StateBase> playerStateBases  = new List<StateBase>();

    [Header("�÷��̾� 1�ʴ� �̵��ϴ� �ӵ�")]
    public float playerMoveSpeed = 3f;
    [Header("��׶��� ����-> ��׶��� / �÷��̾� �ӵ��� ���� ����")]
    public float backgroundLength = 6f;

    // �׽�Ʈ �ڵ��̱� ������ �ٲܼ� ����;

    private PlayerAttributes playerAttribute;


    public void Awake()
    {
        playerStateBases.Add(new PlayerMoveState(this));
        playerStateBases.Add(new PlayerAttackState(this));

        playerAttribute = GetComponent<PlayerAttributes>();

        SetState(States.Move);
    }

    public void Update()
    {
        playerStateManager.Update();
    }

    public void SetState(States newState)
    {
        playerStateManager.ChangeState(playerStateBases[(int)newState]);
    }
}
