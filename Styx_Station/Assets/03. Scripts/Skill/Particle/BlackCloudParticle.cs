using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCloudParticle : PoolAble
{
    private float duration;
    private float timer;

    private void Awake()
    {
        duration = gameObject.GetComponent<ParticleSystem>().main.duration;
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
