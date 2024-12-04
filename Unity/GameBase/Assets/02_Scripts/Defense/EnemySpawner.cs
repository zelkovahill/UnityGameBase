using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("몬스터 종류 배열")]
        private EnemyController[] enemiesToSpawn;

        [SerializeField]
        [Tooltip("스폰 포인트")]
        private Transform spawnPoint;

        [SerializeField]
        [Tooltip("스폰 사이 시간")]
        private float timeBetweenSpawns = 0.5f;

        private float spwanCounter; // 숫자를 카운팅 하는 변수

        [SerializeField]
        [Tooltip("스폰될 때 몬스터 숫자")]
        private int amountToSpawn = 15;

        private void Start()
        {
            spwanCounter = timeBetweenSpawns;
        }

        private void Update()
        {
            // 남아 있는 스폰 될 숫자가 있을 때
            if (amountToSpawn > 0)
            {
                spwanCounter -= Time.deltaTime; // 프레임마다 시간을 감소

                // spawnCount 0 이하일 때
                if (spwanCounter <= 0)
                {
                    spwanCounter = timeBetweenSpawns; // 정해진 스폰 사이 간격 시간을 다시 리셋

                    Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)],
                    spawnPoint.position, spawnPoint.rotation);

                    // Random.Range(0, enemiesToSpawn.Length) 배열안의 랜덤 값을 정해서 프리팹을 생성 , 위치랑 로테이션 값

                    amountToSpawn--; // 스폰될 때 마다 숫자를 감소
                }
            }
        }
    }
}