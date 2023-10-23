using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Boss
{
    [System.Serializable]
    public class BossStatus
    {
        [SerializeField] public string Name;
        [SerializeField] public int Hp;
        [SerializeField] public float Speed;
        [SerializeField] public int Damage;
        [SerializeField] public float AttackSpeed;
        [SerializeField] public float AttackRange;

        public BossStatus(string _name, int _hp, float _speed, int _damage, float _attackSpeed, float _attackRange)
        {
            Name = _name;
            Hp = _hp;
            Speed = _speed;
            Damage = _damage;
            AttackSpeed = _attackSpeed;
            AttackRange = _attackRange;
        }
    }    
}

