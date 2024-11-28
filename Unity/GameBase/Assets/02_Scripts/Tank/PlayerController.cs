using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tank
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("이동 속도")]
        private float moveSpeed = 6f;

        [SerializeField]
        [Tooltip("플레이어 피봇")]
        private GameObject PlayerPivot;

        private Camera viewCamera;
        private Vector3 velocity;

        [SerializeField]
        [Tooltip("발사체 컨트롤러")]
        private ProjectileController projectileController;

        [SerializeField]
        [Tooltip("플레이어 HP")]
        private int player_HP = 5;

        private void Start()
        {
            viewCamera = Camera.main;   // 메인 카메라를 가져옴
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                projectileController.FireProjectile();
            }

            // 컴퓨터 화면 2D의 마우스 좌표에서 카메라를 통과한 후 3D 영역에서의 마우스 위치 값을 가져오기
            Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));

            // 플레이어가 봐야할 타겟 포인트
            Vector3 targetPosition = new Vector3(mousePos.x, transform.position.y, mousePos.z);

            Vector3 direction = targetPosition - PlayerPivot.transform.position; // 목표 방향을 계산
            direction.y = 0; // y축 회전을 방지
            PlayerPivot.transform.rotation = Quaternion.LookRotation(direction); // 회전 처리


            // 플레이어 피봇이 바라 봐야 하는 좌표를 설정
            // PlayerPivot.transform.LookAt(targetPosition, Vector3.up);

            // Input을 통해 캐릭터 이동 벡터를 생성
            velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
        }

        private void FixedUpdate()
        {
            // 계산된 벡터를 플레이어 컨트롤러에 있는 Rigidbody에 접근해서 이동 벡터를 FixedTime만큼 이동 시켜주는 라인
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + velocity * Time.fixedDeltaTime);
        }

        /// <summary>
        /// 데미지를 입었을 때 호출되는 함수
        /// </summary>
        /// <param name="Damage"></param>
        public void Damaged(int Damage)
        {
            player_HP -= Damage;

            if (player_HP <= 0)
            {
                Destroy(gameObject);
            }
        }


    }
}