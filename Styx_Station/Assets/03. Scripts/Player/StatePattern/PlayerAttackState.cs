using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttackState : PlayerStateBase
{
    public PlayerAttackState(PlayerController playerController) : base(playerController)
    {

    }

    private float defaultSpeed = 1f;
    private float increaseAttackSpeed = 0.01f;
    private float attackSpeed = 1f;
    private float spped;

    private float timer = 0f;
    private float attackDuration;

    public override void Enter()
    {
        spped = defaultSpeed + ((SharedPlayerStats.GetPlayerAttackSpeed() - 1) * increaseAttackSpeed);
        playertController.GetAnimator().speed = spped;
        playertController.GetAnimator().SetTrigger("Attack");
        playertController.GetAnimator().SetFloat("NormalState", 0.5f);

        timer = 0f;
        attackDuration = defaultSpeed / spped;
        Debug.Log($"공격 속도 시작 : : {playertController.GetAnimator().speed}");
    }

    public override void Exit()
    {
        playertController.GetAnimator().speed = defaultSpeed;
        Debug.Log($"공격 속도 나감 :: {playertController.GetAnimator().speed}");
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackDuration)
        {
            timer = 0f;
            playertController.GetAnimator().SetTrigger("Attack");
        }
    }

    public override void FixedUpate()
    {
        int dieMonCount = 0;
        var findEnemey =
            Physics2D.OverlapCircleAll(playertController.transform.position, playertController.GetPlayerAttackRange(),
            playertController.layerMask);

        if (findEnemey.Length<1)
        {
            playertController.SetState(States.Idle);
            return;
        }
        else
        {
            foreach (var enemy in findEnemey)
            {
                if (enemy.GetComponent<MonsterStats>().currHealth > 0)
                {
                    return;
                }
                else
                {
                    dieMonCount++;
                }

            }
            if (dieMonCount == findEnemey.Length)
            {
                playertController.SetState(States.Idle);
                return;
            }
        }
        
    }
}
