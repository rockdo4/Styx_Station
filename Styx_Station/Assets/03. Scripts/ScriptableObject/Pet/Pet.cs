using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Pets/Pet")]
public class Pet : ScriptableObject
{
    [Tooltip("�� �̸�")]
    public string Pet_Name;

    [Tooltip("�� ������Ʈ")]
    public GameObject Pet_GameObjet;
}
