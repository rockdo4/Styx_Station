using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ExecuteHit : MonoBehaviour
{
    public AttackDefinition weapon;
    public GameObject target;
    public GameObject attacker;
    public void Hit()
    {
        if (weapon == null || target == null)
        {
            return;
        }
        if(!WaveManager.Instance.isWaveInProgress)
        {
            return;
        }
        weapon.ExecuteAttack(attacker, target);
    }
}
