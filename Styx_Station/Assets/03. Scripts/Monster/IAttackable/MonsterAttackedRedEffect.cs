using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackedRedEffect : MonoBehaviour, IAttackable
{
    private WaitForSeconds waitTime = new WaitForSeconds(0.3f);
    private SpriteRenderer[] spriteRenderers;
    private List<Color> originalColors = new List<Color>();
    private Color effectColor = new Color(1f, 0.64f, 0.64f);
    public float redIntensity = 0.8f;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors.Add(spriteRenderers[i].color);
            //Debug.Log(i);
        }
        if(gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<MonsterController>().SetOriginalColor(originalColors);
        }
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        foreach(SpriteRenderer spriteRenderer in spriteRenderers)
        {
            //Color ori = spriteRenderer.color + Color.red * redIntensity;
            //ori.r =  Mathf.Clamp(ori.r, ori.r, redIntensity);
            //ori.r += effectColor.r;
            //ori.g += effectColor.g;
            //ori.b += effectColor.b;

            Color ori = new Color(1f, 0, 0);

            spriteRenderer.color = ori;
        }

        yield return waitTime;

        for(int i = 0;i < spriteRenderers.Length;i++)
        {
            spriteRenderers[i].color = originalColors[i];    
        }
    }
}
