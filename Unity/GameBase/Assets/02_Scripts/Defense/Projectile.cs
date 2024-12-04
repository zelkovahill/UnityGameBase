using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class Projectile : MonoBehaviour
    {
        private Rigidbody rb;

        [SerializeField]
        [Tooltip("이동 속도")]
        private float moveSpeed;

        [SerializeField]
        [Tooltip("데미지")]
        private float damagedAmount;

        private bool hasDamaged;    // flag 선언

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * moveSpeed;    // rb의 앞쪽 방향으로 총알 이동 속도를 입력
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Enemy") && !hasDamaged)
            {
                col.GetComponent<EnemyHealthConroller>().TakeDanage((int)damagedAmount);    // 데미지 계산 추가
                hasDamaged = true;
            }

            Destroy(gameObject);    // Trigger 충돌이 일어나면 파괴
        }
    }
}