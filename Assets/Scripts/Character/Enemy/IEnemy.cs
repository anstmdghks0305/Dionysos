using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy :ICharacterData
{
    public EnemyType Type
    {
        get;set;
    }
}
