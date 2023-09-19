using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState
{
    public int AttackRange { get; set; }
    public bool CanAttack;

    public bool OperationState()
    {
        if (!CanAttack)
            return false;
        return true;
    }
}
