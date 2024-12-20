using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private const float _maxMagnitude = 100f;

    // Rect를 기반으로 한 Transform, 보통 UI에서 사용
    [SerializeField] private RectTransform _handle;

    public void OnDrag(PointerEventData eventData)
    {
        // 1. 드래그 입력이 들어온 만큼 움직여준다.
        _handle.anchoredPosition += eventData.delta;


        // 2. 범위를 넘어간 경우, 최대 범위 내에서 움직여준다.
        Vector2 currentPosition = _handle.anchoredPosition;
        float currentMagnitude = Mathf.Sqrt(currentPosition.x * currentPosition.x + currentPosition.y * currentPosition.y);
        float currentAngleRadian = Mathf.Atan2(currentPosition.y, currentPosition.x);

        // 최대 길이 벡터 생성
        float clampedMagnitude = Mathf.Min(currentMagnitude, _maxMagnitude);
        var clamedPosition = new Vector2(clampedMagnitude * Mathf.Cos(currentAngleRadian), clampedMagnitude * Mathf.Sin(currentAngleRadian));

        // 위치를 조절된 벡터로 설정
        _handle.anchoredPosition = Vector3.ClampMagnitude(_handle.anchoredPosition, clamedPosition.magnitude);
    }

    // 드래그 입력이 끝나면 우치를 초기화 시킨다.
    public void OnEndDrag(PointerEventData eventData)
    {
        _handle.anchoredPosition = Vector3.zero;
    }

}
