using UnityEngine;
using UnityEngine.AI;

namespace Boss
{
    public class BossAttack : IBossAction
    {
        private BossController controller;
        private int damage;
        private float attackRange;
        private NavMeshAgent navAgent;
        private BossAttackCol attackCol;
        private bool attacking;
        private float timer;
        public void Work()
        {
            timer += Time.deltaTime;
            if (timer >= 1f && !attacking)
            {
                Attack();
                attacking = true;
            }

            if (timer >= 1.1f)
            {
                controller.State = BossState.Idle;
                navAgent.isStopped = false;
                attacking = false;
                timer = 0;
            }
        }

        public BossAttack(BossController controller, int damage, float attackRange, BossAttackCol attackCol, NavMeshAgent navAgent)
        {
            this.controller = controller;
            this.damage = damage;
            this.attackRange = attackRange;
            this.navAgent = navAgent;
            this.attackCol = attackCol;
        }

        void Attack()
        {
            if(attackCol.player != null)
            {
                attackCol.player.Damaged(damage);
            }  
        }
    }
}


