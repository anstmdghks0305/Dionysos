using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWeapon : MonoBehaviour
{
    public int Damage = 5;
    public Transform player;
    public Player _player;
    private void Start()
    {
        _player = player.GetComponent<Player>();
        _player.Attacking = false;
    }
    private void Update()
    {
        if (_player.Attacking)
            transform.GetComponent<BoxCollider>().enabled = true;
        else if (!_player.Attacking)
        {
            init = false;
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
    bool init;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<Enemy>().Damaged(Damage);
            //other.GetComponent<Boss.BossController>().GetDamange(Damage);
            Debug.Log("check");
            if(!init && !_player.slash &&(player.GetComponent<Player>().attackPowerUp || player.GetComponent<Player>().dashPowerUp || player.GetComponent<Player>().SlashSkill.powerUp))
            {
                if(_player.Hp.ShowCurrentHp() > 90)
                {
                    int a = 100 - _player.Hp.ShowCurrentHp();
                    _player.PlusHP(a);
                }
                else
                {
                    if(_player.attack)
                        _player.PlusHP(10);
                }
                init = true;
            }
        }

        else if (other.CompareTag("Projectile") || other.name == "ExplosionArrow(Clone)")
        {
            ProjectileController.Instance.UsedProjectilePooling(other.GetComponent<Projectile>());
            other.gameObject.SetActive(false);
        }

    }
}
