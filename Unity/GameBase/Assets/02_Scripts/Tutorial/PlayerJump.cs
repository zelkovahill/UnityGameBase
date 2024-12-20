using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private const float GRAVITY = 9.8f;
    private const float JUMP_POWER = 5;

    // 점프 시작 시간
    private float _airborneStartTime;

    // 최초 점프한 높이
    private float _airborneStartHeight;

    // 최초 점프 속도
    private float _airborneStartVelocity = 0;

    // 땅에 닿았는지 확인
    private bool _isOnGround = false;

    // 점프를 했는지 확인
    private bool _isOnJump = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 땅인 경우, 점프 시작
            if (_isOnGround)
            {
                _isOnJump = true;
                _airborneStartTime = Time.time;
                _airborneStartHeight = transform.position.y;
                _airborneStartVelocity = JUMP_POWER;
            }
        }
        if (!_isOnGround || _isOnJump)
        {
            // 땅이 아닌 경우, 높이를 계산해서 새로운 위치 설정
            float t = Time.time - _airborneStartTime;
            Vector3 newPosition = transform.position;
            float heightChange = _airborneStartVelocity * t - GRAVITY * t * t / 2;

            newPosition.y = heightChange + _airborneStartHeight;
            transform.position = newPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 땅에 닿았을 때, _isOnGround를 true로 설정
            _isOnJump = false;
            _isOnGround = true;
            _airborneStartVelocity = 0;
        }
    }

    private void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            // 땅에서 벗어난 경우
            _isOnGround = false;
            _airborneStartTime = Time.time;
        }
    }
}
