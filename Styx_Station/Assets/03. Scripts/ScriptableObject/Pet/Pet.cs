using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Pets/Pet")]
public class Pet : ScriptableObject
{
    public Tier Pet_Tier;

    public Enchant Pet_Enchant;

    public float Pet_Attack;

    public float Pet_AttackSpeed;

    public float Pet_AttackRange;

    public float Pet_Attack_Lv;

    public List<int> Pet_UpMatter;

    public RuntimeAnimatorController animation;
    
    public GameObject Pet_GameObjet;

    public Sprite petIcon;

    public Sprite PetChar;
}
