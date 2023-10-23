using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Boss
{
    public class BossController : MonoBehaviour
    {
        private Player player;
        private IBossAction action;
        private NavMeshAgent navAgent;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private BossState currentState = BossState.None;

        public BossDirection Direction;
        public BossStatus Status;
        public BossState State;
        public Player Player
        {
            get
            {
                if (player == null)
                    player = GameObject.FindWithTag("Player").GetComponent<Player>();
                return player;
            }
        }
        private void Awake()
        {
            
        }
        void Start()
        {
            LoadData();
            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            action = null;
            State = BossState.Idle;
            Direction = BossDirection.Left;
        }

        void Update()
        {
            action?.Work();

            switch (State)
            {
                case BossState.Idle:
                    action = new BossIdle(this);
                    break;
                case BossState.Move:
                    action = new BossMove(this, navAgent, Status.Speed);
                    break;
            }
        }

        public void GetDamange (int damage)
        {
            Status.Hp -= damage;
            if (Status.Hp <= 0)
            {
                State = BossState.Die;
            }
        }

        void LoadData()
        {
            Status = DataManager<BossStatus>._BossStatus[gameObject.name];
        }

        void SaveData()
        {

        }
    }
}

