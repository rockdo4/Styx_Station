using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : MonoBehaviour
{


    private StateManager playerStateManager = new StateManager();
    private List<StateBase> playerStateBases  = new List<StateBase>();

    [Header("플레이어 1초당 이동하는 속도")]
    public float playerMoveSpeed = 3f;
    [Header("백그라운드 길이-> 백그라운드 / 플레이어 속도로 상태 변경")]
    public float backgroundLength = 6f;

    // 테스트 코드이기 떄문에 바꿀수 있음;

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
