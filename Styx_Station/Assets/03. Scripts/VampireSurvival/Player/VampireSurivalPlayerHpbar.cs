using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VampireSurivalPlayerHpbar : MonoBehaviour
{
    public float offset = 0.5f;
    public VamprieSurivalPlayerController player;
    public Image image;

    private void FixedUpdate()
    {
        var pos = player.transform.position;
        pos.y += offset;
        transform.position = pos;

        image.fillAmount = (float)player.currentHp / player.maxHp;
    }
}
