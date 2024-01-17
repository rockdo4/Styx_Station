using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterDieState : MonsterStateBase
{
    public float effectDuration = 2.0f; // 효과 지속 시간 설정
    public float redIntensity = 0.5f; // 빨간색 강도 설정

    private SpriteRenderer[] spriteRenderers;
    private float timer = 0f;
    private float releaseTimer = 0f;
    private float releaseTime = 0f; 
    private float fadeDuration = 1f;
    private List<Color> colorList = new List<Color>();
    private Transform idlePos;

    /// <summary>
    /// 임시 사용, 유아이 매니저 제작 시 이동 예정
    /// </summary>
    

    public MonsterDieState(MonsterController monsterCtrl) : base(monsterCtrl)
    {
        
    }

    public override void Enter()
    {
        monsterCtrl.animator.SetTrigger("Die");
        timer = 0;
        colorList = monsterCtrl.GetOriginalColor();
        spriteRenderers = monsterCtrl.gameObject.GetComponentsInChildren<SpriteRenderer>();
        monsterCtrl.GetComponent<Collider2D>().enabled = false;

        idlePos = monsterCtrl.idlePos;
        
    }

    public override void Exit()
    {
        monsterCtrl.animator.SetBool("EditChk", true);
    }

    public override void FixedUpate()
    {
    
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if(timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                Color newColor = renderer.color;
                newColor.a = alpha;
                renderer.color = newColor;
            }
        }
        if(timer > fadeDuration && timer < releaseTime + fadeDuration)
        {
            monsterCtrl.gameObject.transform.position = idlePos.position;
            if(!monsterCtrl.animator.GetBool("EditChk"))
                monsterCtrl.animator.SetBool("EditChk", true);
            monsterCtrl.animator.Play("RunState");
        }
        if(timer >= releaseTime + fadeDuration)
        {
            timer = 0f;
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = colorList[i];
            }

            monsterCtrl.SetState(States.Idle);

            if(monsterCtrl.gameObject.activeSelf)
            {
                monsterCtrl.ReleaseObject();
            }
        }
    }
}

