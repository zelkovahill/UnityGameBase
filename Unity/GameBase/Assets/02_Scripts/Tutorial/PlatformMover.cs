using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    // private float _minimumX = -2f;
    // private float _maximumX = 2f;

    // private float _movingSpeed = 3f;

    private Vector3 _StartPosition = new Vector3(-2, 3, 0);
    private Vector3 _endPosition = new Vector3(2, 3, 0);

    private float _duration = 2.0f;
    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - _startTime;
        if (elapsedTime > _duration)
        {
            // 방향을 바꿔준다.
            Vector3 temp = _StartPosition;
            _StartPosition = _endPosition;
            _endPosition = temp;

            // 지난 시간을 수정한다.
            elapsedTime = elapsedTime - _duration;
            _startTime = Time.time;
        }

        // EaseInOutElastic으로 이동
        float t = elapsedTime / _duration;
        float easedT = EaseInOutElastic(t);

        // easedT를 이용하여 위치를 설정한다.
        transform.position = Vector3.Lerp(_StartPosition, _endPosition, easedT);
    }

    private float EaseInOutElastic(float x)
    {
        var c5 = (2 * Mathf.PI) / 4.5f;

        if (x == 0)
        {
            return 0;
        }
        else if (x == 1)
        {
            return 1;
        }
        else if (x < 0.5)
        {
            return -((Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2);
        }
        else
        {
            return ((Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2) + 1;
        }
    }

    // private void Update()
    // {
    //     // 1. 현재 위치를 가져온다.
    //     Vector3 currentPosition = transform.position;

    //     // 2. x축으로 이동할 양을 계산한다.
    //     float movingAount = _movingSpeed * Time.deltaTime;

    //     // 3. 이동할 양을 현재 위치에 더한다.
    //     currentPosition.x += movingAount;

    //     // 4. 최댓값, 최솟갑을 넘어간 경우 최댓값, 최솟값으로 설정한다
    //     if (currentPosition.x < _minimumX)
    //     {
    //         currentPosition.x = _minimumX;
    //         _movingSpeed *= -1f;
    //     }
    //     else if (currentPosition.x > _maximumX)
    //     {
    //         currentPosition.x = _maximumX;
    //         _movingSpeed *= -1f;
    //     }

    //     // 5. 위치를 설정한다.
    //     transform.position = currentPosition;
    // }
}
