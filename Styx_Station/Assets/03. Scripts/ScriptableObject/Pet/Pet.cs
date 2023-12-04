using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Pets/Pet")]
public class Pet : ScriptableObject
{
    [Tooltip("∆Í ¿Ã∏ß")]
    public string Pet_Name;

    [Tooltip("∆Í ø¿∫Í¡ß∆Æ")]
    public GameObject Pet_GameObjet;
}
