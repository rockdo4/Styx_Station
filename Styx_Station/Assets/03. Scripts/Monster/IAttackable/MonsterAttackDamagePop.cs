using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackDamagePop : MonoBehaviour, IAttackable
{
    public Color color = Color.white;
    public DamageText prefab;
    public float addY = 0.05f;

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var position = transform.GetChild(2).position;
        var text = Instantiate(prefab, position, Quaternion.identity);
        var damageString = UnitConverter.OutString(attack.Damage);
        if(attack.IsCritical)
        {
            color = Color.red;
        }
        if (attacker.CompareTag("Pet"))
        {
            color = new Color(40, 255, 237);
        }
        text.Set(damageString, color);
    }
}
