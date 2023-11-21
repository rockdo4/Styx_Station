using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase : StateBase
{
    protected PlayerController playertController;

    public PlayerStateBase(PlayerController playertController)
    {
        this.playertController = playertController;
    }
}
