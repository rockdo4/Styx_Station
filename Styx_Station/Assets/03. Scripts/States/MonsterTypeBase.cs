using UnityEngine;

[CreateAssetMenu(fileName = "MonsterTypeBase.asset", menuName = "Monster/MonsterBase")]
public class MonsterTypeBase : ScriptableObject
{
    public float maxHealth;
    public float damage;
    public AttackType attackType;
    public GameObject prefab;
    public float speed;
    public AttackDefinition weapon;
}
