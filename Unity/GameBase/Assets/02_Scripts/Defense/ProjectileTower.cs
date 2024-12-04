using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class ProjectileTower : MonoBehaviour
    {
        private Tower thisTower;    // 타워 컴포넌트가 있는 오브젝트

        [SerializeField]
        [Tooltip("발사체 설정")]
        private GameObject projectilePrefab;

        [SerializeField]
        [Tooltip("발사 위치 설정")]
        private Transform firePoint;

        [SerializeField]
        [Tooltip("발사 간격")]
        private float timeBetweenShots = 1f;

        private float shotCounter;    // 시간 설정
        private Transform target;     // 목표 타겟 설정

        [SerializeField]
        [Tooltip("모델 회전")]
        private Transform launcherModel;

        private void Start()
        {
            thisTower = GetComponent<Tower>();
        }

        private void Update()
        {
            if (target != null)
            {
                launcherModel.rotation = Quaternion.Slerp(launcherModel.rotation,
                                                        Quaternion.LookRotation(target.position - transform.position),
                                                        5f * Time.deltaTime);

                launcherModel.rotation = Quaternion.Euler(0f,
                                                         launcherModel.rotation.eulerAngles.y,
                                                         0f);
            }


            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0 && target != null)
            {
                shotCounter = thisTower.fireRate;   // 오브젝트의 Tower 컴포넌트에서 설정한 FireRate에서 시간을 가져와서 입력
                firePoint.LookAt(target);           // 총알 발사는 Target를 바라본다.
                Instantiate(projectilePrefab,
                            firePoint.position,
                            firePoint.rotation);    // 총알을 생성
            }

            // 적을 배열 List에 검출 했을 때
            if (thisTower.enemiesUpdate)
            {
                if (thisTower.enemiesInRange.Count > 0)
                {
                    float minDistance = thisTower.range + 1;

                    foreach (EnemyController enemy in thisTower.enemiesInRange)
                    {
                        if (enemy != null)
                        {
                            float distance = Vector3.Distance(transform.position,
                                                                enemy.transform.position);

                            if (distance < minDistance)
                            {
                                minDistance = distance; // 가장 가까운 거리를 갱신
                                target = enemy.transform;
                            }
                        }
                    }
                }
                else
                {
                    target = null;
                }
            }
        }
    }
}
