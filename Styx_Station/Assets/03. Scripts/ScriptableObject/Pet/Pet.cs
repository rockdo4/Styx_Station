using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Pets/Pet")]
public class Pet : ScriptableObject
{
    [Tooltip("�� �̸�")]
    public string Pet_Name;

    public Tier Pet_Tier;

    public Enchant Pet_Enchant;

    public float Pet_Attack;

    public float Pet_AttackSpeed;

    public float Pet_AttackRange;

    public List<int> Pet_UpMatter;

    [Tooltip("�ִϸ��̼�")]
    public RuntimeAnimatorController animation;
    
    [Tooltip("�� ������Ʈ")]
    public GameObject Pet_GameObjet;
}
