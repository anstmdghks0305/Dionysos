using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class BossAttackCol : MonoBehaviour
    {
        public Player player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                player = other.GetComponent<Player>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                player = null;
        }
    }
}

