using UnityEngine;
using UnityEngine.AI;

namespace Boss
{
    public class BossAttack : IBossAction
    {
        private BossController controller;
        private int damage;
        private float attackSpeed;
        private float attackRange;
        private NavMeshAgent navAgent;
        private BossAttackCol attackCol;
        private BossAnimation animation;
        private bool attacking = true;
        private float timer;
        public void Work()
        {
            if (animation.isAttack)
            {
                Attack();
            }
            else
            {
                attacking = true;
            }
        }

        public BossAttack(BossController controller, int damage, float attackSpeed, float attackRange, BossAttackCol attackCol, NavMeshAgent navAgent, BossAnimation animation)
        {
            this.controller = controller;
            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.attackRange = attackRange;
            this.navAgent = navAgent;
            this.attackCol = attackCol;
            this.animation = animation;
        }

        void Attack()
        {
            if(attackCol.player != null)
            {
                if (attacking)
                {
                    navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                    attackCol.player.Damaged(damage);
                    attacking = false;
                }
            }
            else
            {
                navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                controller.State = BossState.Idle;
                navAgent.isStopped = false;
            }
        }
    }
}


