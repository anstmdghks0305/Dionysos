using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Boss
{
    public class BossController : MonoBehaviour
    {
        private Player player;
        private IBossAction action;
        private NavMeshAgent navAgent;
        private BossState currentState = BossState.None;
        private int maxHp;
        private BossAnimation animation;
        public BossAttackCol attackCol;
        [SerializeField] private Image hpBar;
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
            action = null;
            maxHp = Status.Hp;
            animation = transform.GetChild(1).GetComponent<BossAnimation>();
            hpBar = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
            Direction = BossDirection.Left;
            State = BossState.Idle;
            attackCol = transform.GetChild(1).GetChild(2).GetComponent<BossAttackCol>();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log(maxHp);
                GetDamange(10);
            }
            action?.Work();
            
            if (State == currentState)
                return;
            
            currentState = State;
            
            switch (State)
            {
                case BossState.Idle:
                    action = new BossIdle(this);
                    break;
                case BossState.Move:
                    action = new BossMove(this, navAgent, Status.Speed);
                    break;
                case BossState.Attack:
                    action = new BossAttack(this, Status.Damage, Status.AttackRange, Status.AttackSpeed, attackCol, navAgent, animation);
                    break;
            }
        }

        public void GetDamange (int damage)
        {
            Status.Hp -= damage;
            hpBar.fillAmount = Status.Hp * 1f / maxHp * 1f;
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

