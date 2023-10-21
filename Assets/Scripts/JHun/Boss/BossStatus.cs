using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Boss
{
    [System.Serializable]
    public class BossStatus
    {
        [SerializeField] private string name;
        [SerializeField] private int hp;
        [SerializeField] private int speed;
        [SerializeField] private int damage;

        public BossStatus(string _name, int _hp, int _speed, int _damage)
        {
            name = _name;
            hp = _hp;
            speed = _speed;
            damage = _damage;
        }
    }    
}

