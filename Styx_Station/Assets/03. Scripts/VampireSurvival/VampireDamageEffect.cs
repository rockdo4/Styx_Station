using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireDamageEffect : MonoBehaviour
{
    private WaitForSeconds waitTime = new WaitForSeconds(0.3f);
    private SpriteRenderer[] spriteRenderers;
    private List<Color> originalColors = new List<Color>();
    public Coroutine effectCoroutine;
    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors.Add(spriteRenderers[i].color);
        }
    }
    public IEnumerator ChangeColor()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color ori = new Color(1f, 0, 0);

            spriteRenderer.color = ori;
        }

        yield return waitTime;

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }
        effectCoroutine = null;
    }
}
