using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("공격 범위")]
        public float range = 3.0f;

        [SerializeField]
        [Tooltip("공격 속도")]
        public float fireRate = 1.0f;

        [SerializeField]
        [Tooltip("레이를 통해서 적 캐릭터 검출")]
        private LayerMask isEnemy;

        [SerializeField]
        [Tooltip("공격 범위 안에 들어오는 Collider 배열")]
        private Collider[] colliderInRange;

        [SerializeField]
        [Tooltip("Collider 배열을 통해서 받은 EnemyController List")]
        public List<EnemyController> enemiesInRange = new List<EnemyController>();

        [SerializeField]
        private float checkCounter;

        [SerializeField]
        [Tooltip("공격 범위 다시 검출하는 시간")]
        private float checkTime = 0.2f;

        public bool enemiesUpdate;

        private void Start()
        {
            checkCounter = checkTime;   // 0.2초를 Counter에 입력
        }

        private void Update()
        {
            enemiesUpdate = false;
            checkCounter -= Time.deltaTime;  // Counter에 정해진 시간을 감소 시켜서 주기적을 체크하게 만듬

            if (checkCounter <= 0)
            {
                checkCounter = checkTime;

                colliderInRange = Physics.OverlapSphere(transform.position,
                                                        range,
                                                        isEnemy); // 자신의 위치 , 범위 , 레이어

                enemiesInRange.Clear(); // List에 있는 것 초기화
            }



            // colliderInRange 배열에서
            foreach (Collider col in colliderInRange)
            {
                enemiesInRange.Add(col.GetComponent<EnemyController>()); // EnemyConroller를 받아와서 List에 넣는다.
            }

            enemiesUpdate = true;
        }

    }
}