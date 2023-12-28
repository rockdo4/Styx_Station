using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerStateBase
{
    public float timer = 0f;
    private float duration = 1.5f;
    private PlayerController controller;
    public PlayerDieState(PlayerController playertController) : base(playertController)
    {
        controller = playertController;
    }

    public override void Enter()
    {
        WaveManager.Instance.EndWave();
        WaveManager.Instance.DecreaseCurrentWave();
        controller.GetAnimator().SetTrigger("Die");
        timer = 0f;
    }

    public override void Exit()
    {
        playertController.transform.position = playertController.initialPos;
        playertController.GetAnimator().SetBool("EditChk", true);
        playertController.IsStartTarget = false;
        playertController.GetComponent<ResultPlayerStats>().ResetHp();
    }

    public override void FixedUpate()
    {

    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
            timer = 0f;
            if(!WaveManager.Instance.GetIsRepeat())
            {
                UIManager.Instance.SetGameOverPopUpActive(true);
                WaveManager.Instance.SetRepeat(true);
            }
            //playertController.transform.position = playertController.initialPos;
            //playertController.GetAnimator().SetBool("EditChk", true);
            //playertController.IsStartTarget = false;
            playertController.SetState(States.Idle);
        }
    }
}
