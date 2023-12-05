using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Pets/Pet")]
public class Pet : ScriptableObject
{
    [Tooltip("펫 이름")]
    public string Pet_Name;

    public Tier Pet_Tier;

    public Enchant Pet_Enchant;

    public float Pet_Attack;

    public float Pet_AttackSpeed;

    public float Pet_AttackRange;

    public List<int> Pet_UpMatter;

    [Tooltip("애니메이션")]
    public RuntimeAnimatorController animation;
    
    [Tooltip("펫 오브젝트")]
    public GameObject Pet_GameObjet;
}
