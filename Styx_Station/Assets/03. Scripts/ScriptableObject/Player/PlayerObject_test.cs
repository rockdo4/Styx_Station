using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerObject_test : MonoBehaviour
{
    public PlayerTier tier;
    [Range(0, 1000)]
    public int playerHp;
    [Range(0, 500)]
    public int attackPower;
    [Range(0f, 5f)]
    public float attackSpeed;
    [Range(0f, 5f)]
    public float playerAttackRange;

    public GameObject playerCharacter;
}