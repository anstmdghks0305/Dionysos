using System.Numerics;
using UnityEngine;

public interface IState 
{
    public void Work(IEnemy characterData, Transform target);
}
