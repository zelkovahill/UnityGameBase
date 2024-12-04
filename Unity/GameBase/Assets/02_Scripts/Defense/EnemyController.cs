using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Defense
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("적 이동속도")]
        private float moveSpeed = 1f;

        [SerializeField]
        private float speedMod = 1f;

        [SerializeField]
        private float timeSinceStart = 0f;

        private MonsterPath thePath;    // 패스 위치 정보 값
        private int currentPoint;       // 패스 위치 커서 값
        private bool reachedEnd;         // 도달 완료 검사 bool 값

        private bool modEnd = true;

        private void Start()
        {
            // 시작할 때 Path가 없으면
            if (thePath == null)
            {
                thePath = FindObjectOfType<MonsterPath>();  // Scene에서 찾기
            }
        }

        private void Update()
        {
            if (!modEnd)
            {
                timeSinceStart -= Time.deltaTime;

                if (timeSinceStart <= 0f)
                {
                    speedMod = 1f;
                    modEnd = true;
                }
            }

            // 도달 완료가 아닐 경우
            if (reachedEnd == false)
            {
                transform.LookAt(thePath.points[currentPoint]);  // 지금 위치 커서값을 향해서 본다.

                transform.position = Vector3.MoveTowards(transform.position,
                                                        thePath.points[currentPoint].position,
                                                        moveSpeed * Time.deltaTime * speedMod);

                // 나와 패스 포인트 위치의 거리를 계산해서 0.01 이하일 경우 도착
                if (Vector3.Distance(transform.position, thePath.points[currentPoint].position) < 0.01f)
                {
                    currentPoint += 1;  // 다음 위치 커서로 옮기고

                    // 다 돌았으면 정지
                    if (currentPoint >= thePath.points.Length)
                    {
                        reachedEnd = true;
                    }
                }
            }
        }

        public void SetMode(float value)
        {
            modEnd = false;
            speedMod = value;
            timeSinceStart = 2.0f;
        }
    }
}