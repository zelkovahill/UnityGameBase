using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class EnemyHealthConroller : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("체력 설정")]
        private int totalHealth;

        [SerializeField]
        [Tooltip("처치시 획득 골드")]
        private int moneyOnDeath = 50;

        public void TakeDanage(int damagedAmount)
        {
            totalHealth -= damagedAmount;

            if (totalHealth <= 0)
            {
                totalHealth = 0;
                Destroy(gameObject);

                // 죽은 이후 처리
            }
        }
    }
}