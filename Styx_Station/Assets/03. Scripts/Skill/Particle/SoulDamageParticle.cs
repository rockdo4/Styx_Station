using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDamageParticle : PoolAble
{
    private float duration;
    private float timer;
    private WaveManager waveManager;

    private void Awake()
    {
        duration = gameObject.GetComponent<ParticleSystem>().main.duration;
        waveManager = WaveManager.Instance;
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        if(!waveManager.isWaveInProgress)
        {
            ReleaseObject();
        }
        if(gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if(timer > duration)
            {
                ReleaseObject();
            }
        }

    }

    public override void ReleaseObject()
    {
        timer = 0f;
        base.ReleaseObject();   
    }
}
