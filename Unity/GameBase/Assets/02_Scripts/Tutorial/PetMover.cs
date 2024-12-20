using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMover : MonoBehaviour
{
    private float _currentAngle = 0f;
    private float _movingSpeed = 3f;
    private float _movingRadius = 2f;

    private void Update()
    {
        // 1. 각도를 계산
        _currentAngle += _movingSpeed * Time.deltaTime;

        // 2. 각도에 따라 위치를 계산
        var currentPosition = new Vector3(_movingRadius * Mathf.Cos(_currentAngle), _movingRadius * Mathf.Sin(_currentAngle), 0);

        // 3. 위치를 설정
        transform.localPosition = currentPosition;
    }
}
