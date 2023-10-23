using UnityEngine;
using UnityEngine.AI;

namespace Boss
{
    public class BossAttack : IBossAction
    {
        private BossController controller;
        private int damage;
        private float attackRange;
        private Player player;
        private bool attacking;
        private float timer;
        public void Work()
        {
            timer += Time.deltaTime;
            if(timer >= 0.26f && !attacking)
            {
                Attack();
                attacking = true;
            }

            if (timer >= 0.56f)
            {
                controller.State = BossState.Idle;
                attacking = false;
                timer = 0;
            }
        }

        public BossAttack(BossController controller, int damage, float attackRange)
        {
            this.controller = controller;
            this.damage = damage;
            this.attackRange = attackRange;
            this.player = controller.Player;
        }

        void Attack()
        {
            player.Damaged(damage);
        }
    }
}


