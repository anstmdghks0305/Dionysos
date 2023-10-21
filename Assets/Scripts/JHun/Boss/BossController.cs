using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class BossController : MonoBehaviour
    {
        public BossStatus Status;
        private void Awake()
        {
            
        }
        void Start()
        {
            LoadData();
        }

        void Update()
        {

        }

        void LoadData()
        {
            Status = BossData.Instance.Status[gameObject.name];
        }
    }
}

