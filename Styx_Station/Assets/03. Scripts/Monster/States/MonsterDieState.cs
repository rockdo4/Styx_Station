using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieState : MonsterStateBase
{
    public float effectDuration = 2.0f; // ȿ�� ���� �ð� ����
    public float redIntensity = 0.5f; // ������ ���� ����
    private SpriteRenderer[] spriteRenderers;
    private Color originalColor;
    private List<Color> originalColors = new List<Color>();
    private float timer = 0f;
    private float fadeDuration = 1f;

    public MonsterDieState(MonsterController monsterCtrl) : base(monsterCtrl)
    {
        
    }

    public override void Enter()
    {
        monsterCtrl.animator.SetTrigger("Die");

        spriteRenderers = monsterCtrl.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors.Add(spriteRenderers[i].color);
        }
        timer = 0;
    }

    public override void Exit()
    {

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
        else
        {
            timer = 0f;
            originalColors.Clear();
            monsterCtrl.SetState(States.Idle);
            monsterCtrl.ReleaseObject();
        }
    }
}

