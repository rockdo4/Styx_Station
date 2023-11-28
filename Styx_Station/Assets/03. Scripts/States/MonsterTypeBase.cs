using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterTypeBase.asset", menuName = "Monster/MonsterBase")]
public class MonsterTypeBase : ScriptableObject
{
    public string maxHealth;
    public string damage;
    public AttackType attackType;
    public GameObject prefab;
    public float speed;
    public AttackDefinition weapon;

}
