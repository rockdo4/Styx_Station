using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackDamagePop : MonoBehaviour, IAttackable
{
    public Color color = Color.white;
    public GameObject prefab;
    public float addY = 0.05f;

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
        //var text = Instantiate(prefab, position, Quaternion.identity);
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
