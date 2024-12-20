using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private const float SPEED = 1;

    private void Update()
    {
        // 입력 시스템에서 Horizontal로 주어진 값을 받아온다. (조이스틱의 좌우, 키보드의 방향키 등)
        float horizontalInput = Input.GetAxis("Horizontal");
        // 입력이 주어진다면 보통 좌는 -1, 우는 1로 주어진다.

        if (horizontalInput < 0 || horizontalInput > 0)
        {
            // Time.deltaTime : 유니티에서 제공하는 기능으로 직전의 프레임으로부터 흐른 시간(초)을 뜻한다.
            float moveDistance = horizontalInput * SPEED * Time.deltaTime;

            transform.position += new Vector3(moveDistance, 0, 0);
        }
    }
}
