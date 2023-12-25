using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VampireSurivalParticle : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("VampireEnemy"))
        {
            var mon = other.GetComponent<VampireSurivalMonster>();
            mon.SetDamage(3);
        }
    }
}
