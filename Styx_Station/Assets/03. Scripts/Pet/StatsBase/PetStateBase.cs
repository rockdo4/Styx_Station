using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetStateBase : StateBase
{
    protected PetController petController;

    public PetStateBase(PetController petController)
    {
        this.petController = petController;
    }
}
