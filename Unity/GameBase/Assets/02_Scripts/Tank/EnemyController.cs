using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5.0f;  // 적 속도
    public float rotationSpeed = 1f;    // 포탑 회전 속도
    public GameObject bulletPrefab; // 총알 프리팹
    public GameObject EnemyPivot; // 적 피봇
    public Transform firePoint; // 총구 위치
    public float fireRate = 1f;
    public float nextFireTime;

    private Rigidbody rb;
    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject Temp = GameObject.FindGameObjectWithTag("Player");

        if (Temp != null)
        {
            player = Temp.transform;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // 플레이어의 위치 값 입력
            if (Vector3.Distance(player.position, transform.position) > 5.0f)
            {
                // 적 캐릭터 이동
                Vector3 direction = (player.position - transform.position).normalized;  // Vector3.Distance 거리 지원 함수F
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            }

            // 포탑 회전
            Vector3 targetDirection = (player.position - EnemyPivot.transform.position).normalized;
            targetDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            EnemyPivot.transform.rotation = Quaternion.Lerp(EnemyPivot.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 총알 발사
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                GameObject temp = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                temp.GetComponent<ProjectileMove>().launchDirection = firePoint.localRotation * Vector3.forward;
                temp.GetComponent<ProjectileMove>().projectileType = ProjectileMove.EProjectileType.Enemy;
            }
        }
    }
}
