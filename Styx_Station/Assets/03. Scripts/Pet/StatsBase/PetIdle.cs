using UnityEngine;

public class PetIdle : PetStateBase
{
    private bool playerDie;
    public PetIdle(PetController petController) : base(petController)
    {
    }

    public override void Enter()
    {
        petController.GetAnimator().SetBool("Attacking",false);
        petController.GetAnimator().SetBool("Run", false);
        petController.GetAnimator().SetFloat("RunState", 0f);
    }

    public override void Exit()
    {

    }

    public override void FixedUpate()
    {
        if (petController.masterPlayer.GetComponent<PlayerController>().currentStates == States.Die)
        {
            petController.transform.position = PetManager.Instance.petStartTransform[petController.index].position;
            return;
        }
        var master = petController.masterPlayer.GetComponent<PlayerController>();
        if (master != null)
        {
            if ((master.currentStates == States.Move))
            {
                petController.SetState(States.Move);
            }
        }
        var findEnemy = Physics2D.OverlapCircleAll(petController.transform.position, petController.range, petController.layerMask);

        if (findEnemy.Length<1)
        {
            return;
        }
        else
        {
            //if (petController.masterPlayer.GetComponent<ResultPlayerStats>().playerCurrentHp <= 0)
                if(!WaveManager.Instance.isWaveInProgress)
                    return;
            foreach (var enemy in findEnemy)
            {
                if(enemy.GetComponent<MonsterStats>().currHealth >0)
                {
                    petController.SetState(States.Attack);
                    return;
                }
            }
        }
       
    }

    public override void Update()
    {
        
    }
}
