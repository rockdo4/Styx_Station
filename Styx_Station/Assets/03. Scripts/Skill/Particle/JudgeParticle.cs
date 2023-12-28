using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeParticle : PoolAble
{
    private WaveManager waveManager;
    private void Awake()
    {
        waveManager = WaveManager.Instance;
    }
    private void Update()
    {
        if (!waveManager.isWaveInProgress)
        {
            ReleaseObject();
        }
    }
}
