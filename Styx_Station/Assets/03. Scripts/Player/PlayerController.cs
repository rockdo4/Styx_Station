using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : MonoBehaviour
{


    private StateManager playerStateManager = new StateManager();
    private List<StateBase> playerStateBases  = new List<StateBase>();

    public void Awake()
    {
        //playerStateBases.Add(new IdleState(this));
        //playerStateBases.Add(new PatrolState(this));
        //playerStateBases.Add(new TraceState(this));
    }

    public void Update()
    {

    }
}
