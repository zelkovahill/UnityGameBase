using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interactable
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("플레이어 이동 속도")]
        private float moveSpeed = 5.0f;

        [SerializeField]
        [Tooltip("캐릭터 컨트롤러")]
        private CharacterController controller;

        private Vector3 moveDirection;  // 이동 방향

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");     // 좌우 이동 입력
            float vertical = Input.GetAxis("Vertical");         // 앞뒤 이동 입력

            moveDirection = new Vector3(horizontal, 0, vertical).normalized;  // 이동 방향 설정

            if (moveDirection.magnitude > 0.1f)
            {
                transform.forward = moveDirection;  // 이동 방향으로 플레이어를 회전시킨다.
                controller.Move(moveDirection * moveSpeed * Time.deltaTime);  // 플레이어를 이동시킨다.
            }
        }
    }
}
