using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterTypeBase.asset", menuName = "Monster/MonsterBase")]
public class MonsterTypeBase : ScriptableObject
{
    [Header("monster_index")]
    public int monster_Index;

    [Header("monster_Name")]
    public string monster_Name;

    [Header("monster_type")]
    public AttackType attackType;

    [Header("원거리=protoRangeWeapon, 근거리=protoMeleeweapon")]
    public AttackDefinition weapon;

    [Header("monster_Hp")]
    public string maxHealth;

    [Header("monster_Damage")]
    public string damage;

    [Header("monster_AtkSpeed")]
    public float monsterAtkspeed;

    [Header("monster_mvspeed")]
    public float speed;

    [Header("monster_range")]
    public float monster_range;

    [Header("monster_size")]
    public int monster_size;

    [Header("monster_coin")] 
    public int monster_coin;

    [Header("monster_pommegrande")]
    public int monster_pommegrande;

    [Header("monster_description")]
    public string monster_description;

    [Header("monster_prefab")]
    public GameObject prefab;

}
