using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterAttackDamagePop : MonoBehaviour, IAttackable
{
    public Color color = Color.white;
    public GameObject prefab;
    public float addY = 0.05f;

    private MonsterStats monsterStats;
    private Transform pos;
    private AudioSource audioSource;
    public AudioClip damagedClip;
    private void Awake()
    {
        monsterStats = gameObject.GetComponent<MonsterStats>();
        pos = transform.Find("Canvas");
        audioSource = GetComponent<AudioSource>();  
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        audioSource.PlayOneShot(damagedClip);
        //var position = transform.GetChild(2).position;
        var position = pos.position;
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
        if (attacker.CompareTag("Pet"))
        {
            color = new Color(40f/255f, 1f, 237/255f);
        }
        else
        {
            if (attack.IsCritical)
            {
                color = Color.red;
            }
            else
            {
                color = Color.white;
            }
        }
        text.Set(damageString, color);
    }
}
