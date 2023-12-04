using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Pets/Pet")]
public class Pet : ScriptableObject
{
    [Tooltip("펫 이름")]
    public string Pet_Name;

    [Tooltip("애니메이션")]
    public RuntimeAnimatorController animation;
    
    [Tooltip("펫 오브젝트")]
    public GameObject Pet_GameObjet;
}
