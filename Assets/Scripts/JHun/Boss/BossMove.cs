using UnityEngine;
using UnityEngine.AI;

namespace Boss
{
    public class BossMove : IBossAction
    {
        private BossController controller;
        private NavMeshAgent navAgent;
        private float speed;
        private Vector3 destination;
        private float distance;

        public BossMove(BossController controller, NavMeshAgent navAgent, float speed)
        {
            this.controller = controller;
            this.navAgent = navAgent;
            this.speed = speed;
            destination = controller.Player.transform.position;
        }

        public void Work()
        {
            distance = Vector3.Distance(controller.transform.position, destination);
            Move(destination);
            Flip(destination);
            if (distance <= controller.Status.AttackRange)
            {
                controller.State = BossState.Attack;
            }
        }

        private void Move(Vector3 _destination)
        {
            navAgent.SetDestination(_destination);
        }

        private void Flip(Vector3 destination)
        {
            if(controller.transform.position.x <= destination.x)
            {
                controller.transform.GetChild(1).rotation = Quaternion.Euler(0, 180, 0);
                controller.Direction = BossDirection.Right;
            }
            else
            {
                controller.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, 0);
                controller.Direction = BossDirection.Left;
            }
        }
    }
}

