using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MonsterAttackDamagePop : MonoBehaviour, IAttackable
{
    public Color color = Color.white;
    public GameObject prefab;
    public float addY = 0.05f;

    private MonsterStats monsterStats;

    private void Awake()
    {
        monsterStats = gameObject.GetComponent<MonsterStats>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var position = transform.GetChild(2).position;
        var textObj = ObjectPoolManager.instance.GetGo(prefab.name);
        if(textObj == null)
        {
            Debug.Log("ERR: text is null");
            return;
        }
        textObj.transform.position = position;
        var text = textObj.GetComponent<DamageText>();

        BigInteger damage = attack.Damage;
        if(damage > monsterStats.maxHp)
        {
            damage = monsterStats.maxHp;
        }
        var damageString = UnitConverter.OutString(damage);
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
