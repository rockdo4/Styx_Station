using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSurvivalGameManager : MonoBehaviour
{
    public VamprieSurivalPlayerController player;
    public List<VamprieSurivalPlayerAttackManager> playerAttackType;

    private void Awake()
    {
        player.playerAttackType.Add(playerAttackType[0]);
    }
}
